using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using System.Reflection;

using Medley.Common.Defs.Interfaces;
using Medley.Common.Defs.Types;
using Medley.Common.Utils;

namespace Medley.Common.ADODatabase
{
    public class TableReadInfo
    {
        public int OrdinalID;
        public string FieldName;
        public PropertyInfo PropInfo;
    }

    public class ADORepository : IRepository
    {
        //protected DbConnection m_DbConnection = null;
        protected DbProviderFactory m_Factory = null;
        protected ADODatabaseSetting m_dbSetting = null;
        protected Dictionary<string, TableReadInfo> m_dicTableInfo = new Dictionary<string, TableReadInfo>();

        public void Open(string config)
        {
            

            if (!string.IsNullOrEmpty(config))
            {
                m_dbSetting = XmlSerializeUtil.DeserializeFromXML<ADODatabaseSetting>(config);
            }

            if (m_dbSetting == null)
            {
                m_dbSetting = new ADODatabaseSetting();
                m_dbSetting.DBProviderName = "System.Data.SqlClient";
                m_dbSetting.DBConnectionString = string.Format("Data Source=.;Initial Catalog={0};Integrated Security=true", config);
            }

            m_Factory = DbProviderFactories.GetFactory(m_dbSetting.DBProviderName);
            //m_DbConnection = m_Factory.CreateConnection();
            //m_DbConnection.ConnectionString = dbSetting.DBConnectionString;
            //m_DbConnection.Open(); 
        }

        public void Close()
        {
          /*  if (m_DbConnection != null && m_DbConnection.State == ConnectionState.Open)
                m_DbConnection.Close();*/
        }

        public void AddEntity<T>(T model) where T : class
        {
            using (DbConnection dbConnection = GetConnection())
            {
                AddEntityItem(model, dbConnection);
            }

        }

        public void AddEntityItem<T>(T model, DbConnection dbConnection) where T : class
        {            
            string sqlQuery = "select * from " + model.GetType().Name;
            DbDataAdapter dbDataAdapter = GetAdapter(sqlQuery, dbConnection);

            DataTable dt = new DataTable();
            DataRow row = dt.NewRow();
            ADODatabaseHelper.CopyFromClassToRow<T>(dt, model, row);

        //    if (row["DateModified"] == DBNull.Value )
            //       row["DateModified"] = DateTime.Now;

            if (row["ID"] == DBNull.Value || Convert.ToInt64(row["ID"]) == 0)
                row["ID"] = GetNextId(dbConnection, model.GetType().Name);

            dt.Rows.Add(row);
            //Do Not Explicitly Open a Connection if You Use Fill or Update for a Single Operation
            dbDataAdapter.Update(dt);            

        }

        public void AddEntityList<T>(List<T> modelList) where T : class
        {
            if (modelList == null || modelList.Count == 0)
                return;

            using (DbConnection dbConnection = GetConnection())
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                DbTransaction dbTrans = dbConnection.BeginTransaction();

                try
                {

                    string sqlQuery = "select * from " + modelList[0].GetType().Name;
                    DbDataAdapter dbDataAdapter = GetAdapter(sqlQuery, dbConnection);

                    DataTable dt = new DataTable();
                    long nextID = GetNextId(dbConnection, modelList[0].GetType().Name);

                    foreach (var model in modelList)
                    {
                        //AddEntityItem(model, dbConnection);
                        DataRow row = dt.NewRow();
                        ADODatabaseHelper.CopyFromClassToRow<T>(dt, model, row);
                        if (row["ID"] == DBNull.Value || Convert.ToInt64(row["ID"]) == 0)
                        {
                            row["ID"] = nextID;
                            nextID++;
                        }

                        dt.Rows.Add(row);
                    }

                    dbDataAdapter.Update(dt);
                    dbTrans.Commit();
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    throw ex;
                }

            }
        }

        public void UpdateEntity<T>(T model) where T : class
        {           
            using (DbConnection dbConnection = GetConnection())
            {                
                StringBuilder updateSection = new StringBuilder();
                DbCommand cmd = GetDbCommand(dbConnection);

                int Id = 0;
                foreach (PropertyInfo propInfo in typeof(T).GetProperties())
                {
                    object obj = propInfo.GetValue(model);
                    if (obj != null)
                    {
                        if (propInfo.Name == "ID")
                        {
                            Id = Convert.ToInt32(obj);
                            continue;
                        }

                        DbParameter param = m_Factory.CreateParameter();
                        param.DbType = ADODatabaseHelper.GetDbType(obj);
                        param.Value = obj;
                        param.ParameterName = propInfo.Name;
                        cmd.Parameters.Add(param);

                        if (updateSection.Length > 0)
                            updateSection.Append(", ");                        

                        updateSection.AppendFormat("{0} = @{0}", propInfo.Name); 
                    }
                }

                if (updateSection.Length > 0)
                {
                    cmd.CommandText = string.Format("Update {0} set {1} where ID = {2}", typeof(T).Name, updateSection.ToString(), Id);
                    dbConnection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEntity<T>(T model) where T : class
        {
            using (DbConnection dbConnection = GetConnection())
            {               
                DbCommand cmd = GetDbCommand(dbConnection);

                int Id = 0;
                foreach (PropertyInfo propInfo in typeof(T).GetProperties())
                {
                    object obj = propInfo.GetValue(model);
                    if (obj != null)
                    {
                        if (propInfo.Name == "ID")
                        {
                            Id = Convert.ToInt32(obj);
                            break;
                        }                       
                    }
                }    
            
                cmd.CommandText = string.Format("Delete from {0} where ID = {1}", typeof(T).Name, Id);
                dbConnection.Open();
                cmd.ExecuteNonQuery();
                
            }
        }

        public T GetEntity<T>(long Id) where T : class, new()
        {
            List<T> list = FindEnitiesByField<T>("ID", Id);
            if (list != null && list.Count > 0)
                return list[0];
            else
                return default(T);
        }

        public List<T> FindAllEnities<T>() where T : class, new()
        {
            DbCommand cmd = m_Factory.CreateCommand();
            cmd.CommandText = string.Format("select * from {0} ", typeof(T).Name);
            return FindEnities<T>(cmd);
        }

        public List<T> FindEnitiesByField<T>(string fieldName, object fieldValue) where T : class, new()
        {
            DbCommand cmd = m_Factory.CreateCommand();
            DbParameter param = m_Factory.CreateParameter();
            param.DbType = ADODatabaseHelper.GetDbType(fieldValue);
            param.Value = fieldValue;
            param.ParameterName = fieldName;           
            cmd.Parameters.Add(param);
            cmd.CommandText = string.Format("select * from {0} where {1} = @{1}", typeof(T).Name, fieldName);

            return FindEnities<T>(cmd);
        }

        public List<T> FindEnities<T>(ISearchParam searchParam) where T : class, new()
        {
            DbCommand cmd = m_Factory.CreateCommand();

            if (searchParam is SqlSearchParam)
            {
                cmd.CommandText = string.Format("select * from {0} {1}", typeof(T).Name, ((SqlSearchParam)searchParam).SqlString);
            }
            else
                return new List<T>();

            return FindEnities<T>(cmd);
        }

        public List<T> FindEnities<T>(string whereClause) where T : class, new()
        {
            SqlSearchParam p = new SqlSearchParam(whereClause);
            return FindEnities<T>(p);
        }

        protected List<T> FindEnities<T>(DbCommand cmd) where T : class, new()
        {
            List<T> entityList = new List<T>();

            using (DbConnection dbConnection = GetConnection())
            {
                cmd.Connection = dbConnection;
                
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                DbDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                m_dicTableInfo.Clear();
                List<TableReadInfo> listTblRead = new List<TableReadInfo>();

                foreach (PropertyInfo propInfo in typeof(T).GetProperties())
                {
                    TableReadInfo tblRead = new TableReadInfo();
                    tblRead.PropInfo = propInfo;
                    tblRead.FieldName = propInfo.Name;
                    tblRead.OrdinalID = dr.GetOrdinal(tblRead.FieldName);
                    listTblRead.Add(tblRead);
                }

                while (dr.Read())
                {
                    T entity = new T();

                    foreach (TableReadInfo tblRead in listTblRead)
                    {
                        if (dr[tblRead.OrdinalID] != DBNull.Value)
                            tblRead.PropInfo.SetValue(entity, dr[tblRead.OrdinalID]);
                    }

                    entityList.Add(entity);
                }

                dr.Close();
            }

            return entityList;
        }

        private DbDataAdapter GetAdapter(string sqlQuery, DbConnection dbConnection)
        {
            DbCommand m_Command = null;
            DbDataAdapter m_Adadaptor = null;
            DbCommandBuilder m_cmdBuilder = null;
            
            m_Command = m_Factory.CreateCommand();
            m_Command.CommandText = sqlQuery;
            m_Command.Connection = dbConnection;

            m_Adadaptor = m_Factory.CreateDataAdapter();
            m_Adadaptor.SelectCommand = m_Command;
            m_cmdBuilder = m_Factory.CreateCommandBuilder();
            m_cmdBuilder.DataAdapter = m_Adadaptor;
            
            return m_Adadaptor;
        }

        protected DbCommand GetDbCommand(string sqlQuery, DbConnection dbConnection)
        {
            DbCommand command = m_Factory.CreateCommand();
            command.CommandText = sqlQuery;
            command.Connection = dbConnection;
            return command;
        }

        protected DbCommand GetDbCommand(DbConnection dbConnection)
        {
            DbCommand command = m_Factory.CreateCommand();
            command.Connection = dbConnection;
            return command;
        }

        protected virtual DbConnection GetConnection()
        {
            DbConnection dbConnection = m_Factory.CreateConnection();
            dbConnection.ConnectionString = m_dbSetting.DBConnectionString;
            return dbConnection;
        }

        protected long GetNextId(DbConnection dbConnection, string tableName)
        {
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            DbCommand cmd = GetDbCommand("select MAX(ID) from " + tableName, dbConnection);
            object obj = cmd.ExecuteScalar();

            if (obj == DBNull.Value)
                return 1;
            else
                return ((long)obj) + 1;            
        }
    }
}
