using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;

using Medley.Common.Defs.Interfaces;
using Medley.Common.Utils;
using Medley.Common.ADODatabase;
using IGTradingLib.DataModel;

namespace IGTradingLib
{
    public class TradingRepository : ADORepository
    {
        Dictionary<string, InstrumentDataID> m_IntrumentDataIDs = new Dictionary<string, InstrumentDataID>();
       // long m_NextHistDataID = -1;
        DbConnection m_DbConnection = null;
        List<InstrumentDetails> m_InstrumentDetailsList = new List<InstrumentDetails>();
        List<InstrumentDataID> m_InstrumentDataList = new List<InstrumentDataID>();

        public bool OpenConnection()
        {
            return true;
        }

        public void CloseConnection()
        {
            if (m_DbConnection != null && m_DbConnection.State == ConnectionState.Open)
                m_DbConnection.Close();
        }

       /* protected DbConnection GetTradingConnection()
        {
            if (m_DbConnection == null)
            {
                m_DbConnection = m_Factory.CreateConnection();
                m_DbConnection.ConnectionString = m_dbSetting.DBConnectionString;
                m_DbConnection.Open();
            }

            return m_DbConnection;
        }*/

        public List<InstrumentDetails> GetInstruments()
        {
            if (m_InstrumentDetailsList.Count == 0)
            {
                var res = FindAllEnities<InstrumentDetails>();

                if (res != null)
                    m_InstrumentDetailsList = res;
            }

            return m_InstrumentDetailsList;
        }

        public List<InstrumentDataID> GetInstrumentDataIDs()
        {
            if (m_InstrumentDataList.Count == 0)
            {
                var res = FindAllEnities<InstrumentDataID>();

                if (res != null)
                    m_InstrumentDataList = res;
            }

            return m_InstrumentDataList;
        }

        public InstrumentDataID GetIntrumentDataID(long dataID)
        {
            GetInstrumentDataIDs();

            InstrumentDataID data = m_InstrumentDataList.FirstOrDefault(d => d.ID == dataID);
            if (data != null)
                return data;
            else 
                return new InstrumentDataID();

        }

        public InstrumentDetails GetIntrumentDetails(string epic)
        {
            GetInstruments();

            InstrumentDetails data = m_InstrumentDetailsList.FirstOrDefault(d => d.Epic == epic);
            if (data != null)
                return data;
            else
                return new InstrumentDetails();

        }

        public InstrumentDataID GetIntrumentDataID(string epic)
        {
            GetInstruments();
            GetInstrumentDataIDs();

            if (m_InstrumentDetailsList == null || m_InstrumentDataList == null)
                return new InstrumentDataID();

            InstrumentDetails detail = m_InstrumentDetailsList.FirstOrDefault(d => d.Epic == epic);

            if (detail != null)
            {
                InstrumentDataID data = m_InstrumentDataList.FirstOrDefault(d => d.ID == detail.DataID);
                if (data != null)
                    return data;
            }

            return new InstrumentDataID();          
        }

        public List<InstrumentHistoryData> FindHistData(string epic, string resolution, DateTime dtStart, DateTime dtEnd, string orderBy = "PriceDateTime asc", int limit = -1) 
        {
            DbCommand cmd = m_Factory.CreateCommand();
            string strLimit = "";
            if (limit != -1)
                strLimit = "LIMIT " + limit.ToString();

            string sql = string.Format("select * from InstrumentHistoryData where DataID = {0} and Resolution = '{1}' and PriceDateTime >= @StartDate and PriceDateTime <= @EndDate order by {2} {3}",
                GetIntrumentDataID(epic).ID, resolution, orderBy, strLimit);
            cmd.CommandText = sql;

            DbParameter paramStartDate = m_Factory.CreateParameter();
            paramStartDate.DbType = DbType.DateTime;
            paramStartDate.Value = dtStart;
            paramStartDate.ParameterName = "StartDate";
            cmd.Parameters.Add(paramStartDate);

            DbParameter paramEndDate = m_Factory.CreateParameter();
            paramEndDate.DbType = DbType.DateTime;
            paramEndDate.Value = dtEnd;
            paramEndDate.ParameterName = "EndDate";
            cmd.Parameters.Add(paramEndDate);

            return FindEnities<InstrumentHistoryData>(cmd);
        }

        public InstrumentHistoryData GetLastInstrumentHistoryItem(string epic, string resolution)
        {
            DbCommand cmd = m_Factory.CreateCommand();
            string sql = string.Format("select * from InstrumentHistoryData where DataID = {0} and Resolution = '{1}' order by PriceDateTime desc LIMIT 1", GetIntrumentDataID(epic).ID, resolution);
            cmd.CommandText = sql;

            return FindEnities<InstrumentHistoryData>(cmd).FirstOrDefault();
        }

        public InstrumentHistoryData GetFirstInstrumentHistoryItem(string epic, string resolution)
        {
            DbCommand cmd = m_Factory.CreateCommand();
            string sql = string.Format("select * from InstrumentHistoryData where DataID = {0} and Resolution = '{1}' order by PriceDateTime asc LIMIT 1", GetIntrumentDataID(epic).ID, resolution);
            cmd.CommandText = sql;

            return FindEnities<InstrumentHistoryData>(cmd).FirstOrDefault();
        }

    /*    public void AddPriceHistory(List<InstrumentHistoryData> dataItems)
        {
            DbConnection dbConnection = GetTradingConnection();
            DbTransaction dbTrans = dbConnection.BeginTransaction();

            try
            {

                foreach (InstrumentHistoryData dataItem in dataItems)
                {

                    StringBuilder sqlQuery = new StringBuilder();
                    DbCommand cmd = GetDbCommand(dbConnection);

                    if (m_NextHistDataID == -1)
                        m_NextHistDataID = GetNextId(dbConnection, "InstrumentHistoryData");
                    else
                        m_NextHistDataID++;

                    dataItem.ID = m_NextHistDataID;

                    sqlQuery.Append("Insert into InstrumentHistoryData(ID, DataID, Resolution, PriceDateTime, OpenBidPrice, CloseBidPrice)");
                    sqlQuery.AppendFormat("values({0}, {1}, '{2}', @PriceDateTime, {3}, {4})", dataItem.ID, dataItem.DataID, dataItem.Resolution, dataItem.OpenBidPrice, dataItem.CloseBidPrice);

                    DbParameter param = m_Factory.CreateParameter();
                    param.DbType = DbType.DateTime;
                    param.Value = dataItem.PriceDateTime;
                    param.ParameterName = "PriceDateTime";
                    cmd.Parameters.Add(param);

                    //if (dbConnection.State == ConnectionState.Closed)
                   //     dbConnection.Open();

                    cmd.CommandText = sqlQuery.ToString();
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                dbTrans.Commit();
            }
            
        }*/
    }
}
