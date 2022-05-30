using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using System.Threading;

using IGTradingLib.DataModel;
using IGTradingLib.Types;
using Medley.Common.Logging;

namespace IGTradingLib
{
    public static class TradingDataStreamMgr
    {
        static Dictionary<long, TradingDataStream> m_objMap = new Dictionary<long, TradingDataStream>();

        public static void AddDataPrice(string epic, DataStreamPrice dataPrice)
        {
            TradingDataStream obj = GetObj(epic);
            obj.AddDataPrice(dataPrice);
        }

        public static TradingDataStream GetObj(string epic)
        {
            long id = DB.Rep.GetIntrumentDataID(epic).ID;

            if (m_objMap.ContainsKey(id))
            {
                return m_objMap[id];
            }
            else
            {
                TradingDataStream obj = new TradingDataStream();
                obj.Connect(id);
                m_objMap[id] = obj;
                return obj;
            }
        }
    }

    public class TradingDataStream
    {
        

        List<DataStreamPrice> m_CachedList = new List<DataStreamPrice>();
        List<DataStreamPrice> m_DirtyList = new List<DataStreamPrice>();

        DbProviderFactory m_Factory = null;
        string m_ConnectionStr = "";
        object dbSyncLock = new object();

        TimeZoneInfo m_TimeZoneData = TimeZoneInfo.Local;
        InstrumentDataID m_DataID = null;
        Timer m_DbTimer = null;

        public bool Connect(long dataId)
        {
            try
            {
                m_Factory = DbProviderFactories.GetFactory("System.Data.SQLite");
                string dbFilePath = string.Format(@"Data Source=Database\TradingDB_StreamData{0}.db3", dataId);
                m_ConnectionStr = string.Format(@"{0};datetimeformat=CurrentCulture", dbFilePath);

                using (DbConnection dbConnection = GetConnection())
                {
                    if (dbConnection.State == ConnectionState.Closed)
                        dbConnection.Open();

                    string query = "SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = 'DataStreamPrice'";

                    using (DbCommand cmd = GetDbCommand(query, dbConnection))
                    {
                        object obj = cmd.ExecuteScalar();

                        if (obj == null || Convert.ToInt32(obj) <= 0)
                        {
                            using (DbCommand cmd2 = GetDbCommand(GetTableCreateSql(), dbConnection))
                            {
                                cmd2.ExecuteNonQuery();
                            }
                        }
                    }

                    m_DataID = DB.Rep.GetIntrumentDataID(dataId);
                    if (!string.IsNullOrEmpty(m_DataID.DataTimeZone))
                        m_TimeZoneData = TimeZoneInfo.FindSystemTimeZoneById(m_DataID.DataTimeZone);

                    m_DbTimer = new Timer(new TimerCallback(WriteDBTimerFn), null, 0, 30000); //call timer every 30 seconds

                    MedleyLogger.Instance.Info("TradingDataStream. New connection for " + m_DataID.Name);

                }
            }
            catch (Exception ex)
            {
                LibDef.RaiseErrorMessage(ex.Message);
                return false;
            }

            return true;
        }

        public void AddDataPrice(DataStreamPrice dataPrice)
        {
            //MedleyLogger.Instance.Info("TradingDataStream.AddDataPrice start for " + m_DataID.Name);
            if (!string.IsNullOrEmpty(m_DataID.DataTimeZone))
            {
                dataPrice.PriceDatetime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, m_TimeZoneData);
            }
            else
            {
                dataPrice.PriceDatetime = DateTime.Now;
            }

            DataStreamUpdateEvent pEvent = new DataStreamUpdateEvent();
            pEvent.DataID = m_DataID;
            pEvent.PriceData = dataPrice;

            try
            {
                LibDef.NotifyOnStreamPrice(pEvent);

                if (LibDef.OnStreamEvent != null)
                    LibDef.OnStreamEvent(pEvent);

                LibDef.m_StatusEvent.DataStreamEvent(pEvent);
            }
            catch (Exception ex)
            {
                MedleyLogger.Instance.Error("Failed to raise datastream event. ", ex);
                LibDef.RaiseErrorMessage("Failed to raise datastream event. " + ex.Message);
            }

            if (m_CachedList.Count == 100000)
                m_CachedList.RemoveAt(m_CachedList.Count - 1);

            m_CachedList.Add(dataPrice);           

            lock (dbSyncLock)
            {
                m_DirtyList.Add(dataPrice);
            }

            //MedleyLogger.Instance.Info("TradingDataStream.AddDataPrice end for " + m_DataID.Name);
                       
        }

        public List<DataStreamPrice> LoadHistory(DateTime startDate, DateTime endDate)
        {
            List<DataStreamPrice> dataList = new List<DataStreamPrice>();
            try
            {
                lock (dbSyncLock)
                {
                    using (DbConnection dbConnection = GetConnection())
                    {
                        if (dbConnection.State == ConnectionState.Closed)
                            dbConnection.Open();

                        string sql = "select * from DataStreamPrice where PriceDateTime >= @StartDate and PriceDateTime <= @EndDate order by PriceDateTime";

                        using (DbCommand cmd = GetDbCommand(sql, dbConnection))
                        {
                            cmd.CommandText = sql;

                            DbParameter paramStartDate = m_Factory.CreateParameter();
                            paramStartDate.DbType = DbType.DateTime;
                            paramStartDate.Value = startDate;
                            paramStartDate.ParameterName = "StartDate";
                            cmd.Parameters.Add(paramStartDate);

                            DbParameter paramEndDate = m_Factory.CreateParameter();
                            paramEndDate.DbType = DbType.DateTime;
                            paramEndDate.Value = endDate;
                            paramEndDate.ParameterName = "EndDate";
                            cmd.Parameters.Add(paramEndDate);


                            using (DbDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                            {

                                while (dr.Read())
                                {
                                    DataStreamPrice data = new DataStreamPrice();

                                    data.AskPrice = (decimal)dr[dr.GetOrdinal("AskPrice")];
                                    data.BidPrice = (decimal)dr[dr.GetOrdinal("BidPrice")];
                                    data.PriceDatetime = (DateTime)dr[dr.GetOrdinal("PriceDateTime")];

                                    dataList.Add(data);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LibDef.RaiseErrorMessage("Failed to load stream data." + ex.Message);
                MedleyLogger.Instance.Error("Failed to load stream data." + ex.Message);
            }

            return dataList;
        }

        private void WriteDBTimerFn(Object param)
        {
            int count = 0;
            lock (dbSyncLock)
            {
                count = m_DirtyList.Count;
            }

            if (count > 0)
            {
                MedleyLogger.Instance.Info("TradingDataStream.WriteDBTimerFn write records " + count.ToString() + " " + m_DataID.Name);
                UpdateDatabase();
            }
        }

        protected void UpdateDatabase()
        {
            int lastStep = 0;
            try
            {
                
                lock (dbSyncLock)
                {
                    using (DbConnection dbConnection = GetConnection())
                    {
                        lastStep = 1;
                        if (dbConnection.State == ConnectionState.Closed)
                            dbConnection.Open();

                        lastStep = 2;

                        using(DbTransaction dbTrans = dbConnection.BeginTransaction())
                        {
                            lastStep = 3;
                            try
                            {
                                foreach (var i in m_DirtyList)
                                {
                                    string sql = string.Format("insert into DataStreamPrice(PriceDateTime, BidPrice, AskPrice) values(@PriceDateTime, {0}, {1})", i.BidPrice, i.AskPrice);

                                    using (DbCommand cmd = GetDbCommand(sql, dbConnection))
                                    {

                                        DbParameter param = m_Factory.CreateParameter();
                                        param.DbType = DbType.DateTime;
                                        param.Value = i.PriceDatetime;
                                        param.ParameterName = "PriceDateTime";
                                        cmd.Parameters.Add(param);

                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                lastStep = 4;
                                dbTrans.Commit();
                                m_DirtyList.Clear();
                                lastStep = 5;
                            }
                            catch (Exception ex)
                            {
                                LibDef.RaiseErrorMessage(m_DataID.Name + " Datastream db insert error. " + ex.Message);
                                MedleyLogger.Instance.Error(m_DataID.Name + " Datastream db insert error. " + ex.Message);
                                dbTrans.Rollback();
                            }

                        } //end of using trans

                    }
                }
            }
            catch (Exception ex)
            {
                LibDef.RaiseErrorMessage(m_DataID.Name + lastStep.ToString() + " Datastream db insert error2. " + ex.Message);
                MedleyLogger.Instance.Error(m_DataID.Name + lastStep.ToString() + " Datastream db insert error2. " + ex.Message);  
            }

        }

        protected virtual DbConnection GetConnection()
        {
            DbConnection dbConnection = m_Factory.CreateConnection();
            dbConnection.ConnectionString = m_ConnectionStr;
            return dbConnection;
        }

        protected DbCommand GetDbCommand(string sqlQuery, DbConnection dbConnection)
        {
            DbCommand command = m_Factory.CreateCommand();
            command.CommandText = sqlQuery;
            command.Connection = dbConnection;
            return command;
        }

        protected string GetTableCreateSql()
        {
            return "CREATE TABLE DataStreamPrice (" +
                   "ID            INTEGER PRIMARY KEY AUTOINCREMENT, " + 
                   "PriceDateTime    DATETIME, " +
                   "BidPrice     NUMERIC( 10, 5 ), " +
                   "AskPrice     NUMERIC( 10, 5 ) " +
                   ");";
        }
       
    }
}
