using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using dto.colibri.endpoint.auth.v2;
using dto.endpoint.accountswitch;
//using dto.endpoint.auth.session;
using IGPublicPcl;
using Lightstreamer.DotNet.Client;
//using Newtonsoft.Json;
using dto.endpoint.search;
using IGTradingLib.DataModel;
using IGTradingLib.Types;
using Medley.Common.Utils;
using Medley.Common.Logging;

namespace IGTradingLib
{
    public class TradingImpl : ITrading
    {
        private IgRestApiClient m_IGRestApiClient;
        AuthenticationResponse m_AuthRsp = null;
        string m_currentAccountID = "";
        IIGStatusEvent m_StatusEvent = null;
        private IGStreamingApiClient m_IgStreamApiClient = null;
        PriceDataSubscription m_PriceDataSubscription = null;
        AccountSubscription[] m_AccountSubscriptions = new AccountSubscription[2];
        PositionSubscription[] m_PositionSubscriptions = new PositionSubscription[2];
        //string m_AccountTimeZone = "";

        string m_ApiKey = "";
        string m_UserID = "";
        string m_Pwd = "";
        bool m_bLive = false;

        public TradingImpl()
        {
            TradingConfig.Instance = new TradingConfig();

            m_IGRestApiClient = new IgRestApiClient();
            DB.Rep = RepositoryFactory.GetRepository("Database.ADO", "TradingDB");
                        
            //DB.Rep.Open("TradingDB");
        }

        public void SetIGEvent(IIGStatusEvent status)
        {
            m_StatusEvent = status;
            LibDef.m_StatusEvent = status;
        }

       

        public bool Login(string apiKey, string userID, string pwd, bool bLive)
        {
            m_ApiKey = apiKey;
            m_UserID = userID;
            m_Pwd = pwd;
            m_bLive = bLive;

            if (bLive)
                IgRestService.SetLiveMode();
            else
                IgRestService.SetDemoMode();

            var ar = new AuthenticationRequest();
            ar.identifier = userID;
            ar.password = pwd;

            var authenticationResponse = m_IGRestApiClient.SecureAuthenticate(ar, apiKey);
            if (CheckIGRsp(authenticationResponse))
            {
                m_AuthRsp = authenticationResponse.Result.Response;
                m_currentAccountID = m_AuthRsp.currentAccountId;
                RaiseIGEvent(true, m_AuthRsp.accountInfo);
                SetAccountTimeZone();
                return true;
            }
            else
                return false;
        }

        public void Logout()
        {            
            m_AuthRsp = null;
            StopDataStreaming();
            m_IGRestApiClient.logout();
            RaiseIGEvent(false);
        }

        public void SwapAccount()
        {
            if (m_AuthRsp == null)
                return;

            AccountSwitchRequest  req = new AccountSwitchRequest();
            if (m_AuthRsp.accounts[0].accountId == m_currentAccountID)
                req.accountId = m_AuthRsp.accounts[1].accountId;
            else
                req.accountId = m_AuthRsp.accounts[0].accountId;

            var rsp = m_IGRestApiClient.accountSwitch(req);
            //m_IGRestApiClient.accountBalance
            if (CheckIGRsp(rsp))
            {
                m_currentAccountID = req.accountId;
                SetAccountTimeZone();
                //var rspBal = m_IGRestApiClient.accountBalance();
                // if (rspBal.Result && (rspBal.Result.Response != null) && rspBal.Result.StatusCode == System.Net.HttpStatusCode.OK)
                RaiseIGEvent(true);
            }

        }

        public List<Market> SearchMarket(string str)
        {
            var ret = m_IGRestApiClient.searchMarket(str);

            if (ret != null && ret.Result != null && ret.Result.Response != null)
                return ret.Result.Response.markets;
            else 
                return null;
        }       

        public int DownloadPriceHistory(string epic, string resolution, DateTime dtFrom, DateTime dtTo)
        {
            InstrumentDataID dataID = DB.Rep.GetIntrumentDataID(epic);
           
            TimeZoneInfo timeZoneData = TimeZoneInfo.Local;

            if (!string.IsNullOrEmpty(dataID.DataTimeZone))
                timeZoneData = TimeZoneInfo.FindSystemTimeZoneById(dataID.DataTimeZone);           

            //convert from data timezone to account timezone
            if (dataID.DataTimeZone != LibDef.m_AccountTimeZone)
            {
                dtFrom = TimeZoneInfo.ConvertTime(dtFrom, timeZoneData, LibDef.m_TimeZoneAcc);
                dtTo = TimeZoneInfo.ConvertTime(dtTo, timeZoneData, LibDef.m_TimeZoneAcc);
            }

            //var response = m_IGRestApiClient.priceSearchByNum(epic, resolution, count.ToString());
            var response = m_IGRestApiClient.priceSearchByDate(epic, resolution, dtFrom.ToString(@"yyyy:MM:dd-HH:mm:ss"), dtTo.ToString(@"yyyy:MM:dd-HH:mm:ss"));
            if (CheckIGRsp(response) && (response.Result.Response.prices != null))
            {
                List<InstrumentHistoryData> dataList = new List<InstrumentHistoryData>();
                foreach (var price in response.Result.Response.prices)
                {
                    InstrumentHistoryData data = new InstrumentHistoryData();
                    data.DataID = dataID.ID;
                    data.Resolution = resolution;
                    //2015:12:17-19:45:00
                    data.PriceDateTime = DateTime.ParseExact(price.snapshotTime, @"yyyy:MM:dd-HH:mm:ss", CultureInfo.InvariantCulture);

                    //convert from account timezone to data timezone
                    if (dataID.DataTimeZone != LibDef.m_AccountTimeZone)
                    {
                        data.PriceDateTime = TimeZoneInfo.ConvertTime(data.PriceDateTime, LibDef.m_TimeZoneAcc, timeZoneData);
                    }

                    data.OpenAskPrice = price.openPrice.ask;
                    data.OpenBidPrice = price.openPrice.bid;
                    data.OpenLastTraded = price.openPrice.lastTraded;

                    data.CloseAskPrice = price.closePrice.ask;
                    data.CloseBidPrice = price.closePrice.bid;
                    data.CloseLastTraded = price.closePrice.lastTraded;

                    data.LowAskPrice = price.lowPrice.ask;
                    data.LowBidPrice = price.lowPrice.bid;
                    data.LowLastTraded = price.lowPrice.lastTraded;

                    data.HighAskPrice = price.highPrice.ask;
                    data.HighBidPrice = price.highPrice.bid;
                    data.HighLastTraded = price.highPrice.lastTraded;

                    data.LastTradedVolume = price.lastTradedVolume;
                    data.DateModified = DateTime.Now;

                    if (!(data.GetMidClosePrice() == 0 || data.GetMidOpenPrice() == 0 || data.GetMidHighPrice() == 0 || data.GetMidLowPrice() == 0))
                    {
                        dataList.Add(data);
                    }
                }

                DB.Rep.AddEntityList(dataList);


                RaiseErrorMessage("Allowance expire " + DateTime.Now.AddSeconds(response.Result.Response.allowance.allowanceExpiry).ToString());
                RaiseErrorMessage("Remaining Allowance " + response.Result.Response.allowance.remainingAllowance.ToString());
                return response.Result.Response.allowance.remainingAllowance;
            }
            else
            {
                return -1;
            }
        }
                

        public void StartDataStreaming()
        {
            DataStreamParam param = new DataStreamParam();
            param.PriceData = true;
            var epicList = from row in DB.Rep.GetInstruments() where row.EnableLiveStream == true select row.Epic;
            param.EpicList = epicList.ToArray();

            if (m_AuthRsp == null || m_AuthRsp.lightstreamerEndpoint == null)
            {
                RaiseErrorMessage("Auth rsp not set");
                return;
            }

            m_IgStreamApiClient = new IGStreamingApiClient();
            ConversationContext context = m_IGRestApiClient.GetConversationContext();

            if ((context != null) && (context.apiKey != null) && (context.xSecurityToken != null) && (context.cst != null))
            {
                bool connectionEstablished = m_IgStreamApiClient.Connect(m_AuthRsp.currentAccountId, context.cst,
                                                            context.xSecurityToken, context.apiKey, m_AuthRsp.lightstreamerEndpoint);

                if (connectionEstablished)
                    SubscribeToDataStream(param);
                else
                    RaiseErrorMessage("Failed to connect to streaming data");
            }
            else
            {
                RaiseErrorMessage("Invalid context returned");
            }
        }

        public void StopDataStreaming()
        {
            if (m_IgStreamApiClient == null)
                return;

            UnsubscribeFromDataStream();
            m_IgStreamApiClient.disconnect();
            m_IgStreamApiClient = null;
        }

        public void PingIGServer()
        {
            if (m_IGRestApiClient == null || m_IgStreamApiClient == null || m_PriceDataSubscription == null)
                return;

            TimeSpan timeDiff = DateTime.Now - m_PriceDataSubscription.LastPriceUpdate;
            if (timeDiff.TotalSeconds < 15 || DateTime.Today.DayOfWeek == DayOfWeek.Saturday || DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
                return;

            try
            {
                var accBal = m_IGRestApiClient.accountBalance();

                RaiseErrorMessage("No data received from Price server in a while.");
                MedleyLogger.Instance.Info("No data received from Price server in a while.");

                if (accBal.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //we are still logged in. Only restart streaming.
                    StopDataStreaming();
                    StartDataStreaming();
                }
                else
                {
                    //account is probably logged off
                    Logout();
                    if (Login(m_ApiKey, m_UserID, m_Pwd, m_bLive))
                        StartDataStreaming();
                }
            }
            catch (Exception ex)
            {
                MedleyLogger.Instance.Error("Failed to check account balance during server ping", ex);                
                
                try
                {
                    //Logout();
                    m_AuthRsp = null;
                    //m_IgStreamApiClient = null;                   
                    StopDataStreaming();
                    RaiseIGEvent(false);
                }
                catch
                {
                    
                }
            }
           
            

        }

        public dto.endpoint.confirms.ConfirmsResponse CreatePosition(dto.endpoint.positions.create.otc.v1.CreatePositionRequest req)
        {
            var rsp = m_IGRestApiClient.createPositionV1(req);

            if (CheckIGRsp(rsp))
            {
                //verify that position actually exist
                var getPosRsp = m_IGRestApiClient.retrieveConfirm(rsp.Result.Response.dealReference);

                if (getPosRsp == null || getPosRsp.Result.Response.status != "OPEN")
                    return null;
                else
                    return getPosRsp.Result.Response;
            }
            else
                return null;
        }

        public dto.endpoint.positions.edit.v1.EditPositionResponse EditPosition(string dealId, dto.endpoint.positions.edit.v1.EditPositionRequest req)
        {
            var rsp = m_IGRestApiClient.editPositionV1(dealId, req);

            if (CheckIGRsp(rsp))
            {
                //verify that position actually changed
                var getPosRsp = m_IGRestApiClient.retrieveConfirm(rsp.Result.Response.dealReference);

                if (getPosRsp == null || getPosRsp.Result.Response.status != "AMENDED")
                    return null;
                else
                    return rsp.Result.Response;
            }
            else
                return null;
        }

        public dto.endpoint.positions.close.v1.ClosePositionResponse ClosePosition(dto.endpoint.positions.close.v1.ClosePositionRequest req)
        {
            var rsp = m_IGRestApiClient.closePosition(req);

            if (CheckIGRsp(rsp))
            {
                //verify that position closed
                var getPosRsp = m_IGRestApiClient.retrieveConfirm(rsp.Result.Response.dealReference);

                if (getPosRsp == null || (getPosRsp.Result.Response.status != "CLOSED" && getPosRsp.Result.Response.status != "PARTIALLY_CLOSED" && getPosRsp.Result.Response.status != "DELETED"))
                    return null;
                else
                    return rsp.Result.Response;
            }
            else
                return null;
        }

        public dto.endpoint.positions.get.otc.v1.PositionsResponse GetOpenPositions()
        {
            var rsp = m_IGRestApiClient.getOTCOpenPositionsV1();
            if (CheckIGRsp(rsp))
                return rsp.Result.Response;
            else
                return null;
        }

        public dto.endpoint.positions.get.otc.v1.OpenPosition GetPosition(string dealId)
        {
            var rsp = m_IGRestApiClient.getOTCOpenPositionByDealIdV1(dealId);

            if (CheckIGRsp(rsp))
                return rsp.Result.Response;
            else
                return null;
        }

        #region Helper functions

        protected void SubscribeToDataStream(DataStreamParam p)
        {
            if (m_PriceDataSubscription == null)
                m_PriceDataSubscription = new PriceDataSubscription(m_StatusEvent);

            if (m_AccountSubscriptions[0] == null)
                m_AccountSubscriptions[0] = new AccountSubscription(m_StatusEvent, m_AuthRsp.accounts[0]);

            if (m_AccountSubscriptions[1] == null)
                m_AccountSubscriptions[1] = new AccountSubscription(m_StatusEvent, m_AuthRsp.accounts[1]);

            if (m_PositionSubscriptions[0] == null)
                m_PositionSubscriptions[0] = new PositionSubscription(m_StatusEvent, m_AuthRsp.accounts[0]);

            if (m_PositionSubscriptions[1] == null)
                m_PositionSubscriptions[1] = new PositionSubscription(m_StatusEvent, m_AuthRsp.accounts[1]);

            m_PriceDataSubscription.m_SubscriptionKey = m_IgStreamApiClient.subscribeToMarketDetails(p.EpicList, m_PriceDataSubscription); 
                   // new string[] { "UPDATE_TIME", "MARKET_DELAY", "MARKET_STATE", "BID", "OFFER"});

            m_AccountSubscriptions[0].m_SubscriptionKey = m_IgStreamApiClient.subscribeToAccountDetailsKey(m_AuthRsp.accounts[0].accountId, m_AccountSubscriptions[0]);
            m_AccountSubscriptions[1].m_SubscriptionKey = m_IgStreamApiClient.subscribeToAccountDetailsKey(m_AuthRsp.accounts[1].accountId, m_AccountSubscriptions[1]);

            m_PositionSubscriptions[0].m_SubscriptionKey = m_IgStreamApiClient.subscribeToTradeSubscription(m_AuthRsp.accounts[0].accountId, m_PositionSubscriptions[0]);
            m_PositionSubscriptions[1].m_SubscriptionKey = m_IgStreamApiClient.subscribeToTradeSubscription(m_AuthRsp.accounts[1].accountId, m_PositionSubscriptions[1]); 

        }

        protected void UnsubscribeFromDataStream()
        {
            if (m_PriceDataSubscription == null || m_IgStreamApiClient == null || m_PriceDataSubscription.m_SubscriptionKey == null)
                return;

            m_IgStreamApiClient.UnsubscribeTableKey(m_PriceDataSubscription.m_SubscriptionKey);
            m_PriceDataSubscription.m_SubscriptionKey = null;

            m_IgStreamApiClient.UnsubscribeTableKey(m_AccountSubscriptions[0].m_SubscriptionKey);
            m_AccountSubscriptions[0].m_SubscriptionKey = null;

            m_IgStreamApiClient.UnsubscribeTableKey(m_AccountSubscriptions[1].m_SubscriptionKey);
            m_AccountSubscriptions[1].m_SubscriptionKey = null;

            m_IgStreamApiClient.UnsubscribeTableKey(m_PositionSubscriptions[0].m_SubscriptionKey);
            m_PositionSubscriptions[0].m_SubscriptionKey = null;

            m_IgStreamApiClient.UnsubscribeTableKey(m_PositionSubscriptions[1].m_SubscriptionKey);
            m_PositionSubscriptions[1].m_SubscriptionKey = null;
        }


        protected bool CheckIGRsp<T>(Task<IgPublicPcl.IgResponse<T>> response)
        {
            try
            {
                if (response.Result == null)
                {
                    RaiseErrorMessage("No Response received");
                    return false;
                }

                if (response.Result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    RaiseErrorMessage("Response error " + response.Result.StatusCode);
                    return false;
                }

                if (response.Result.Response == null)
                {
                    RaiseErrorMessage("No Response data received");
                    return false;
                }            
            }
            catch (Exception ex)
            {
                MedleyLogger.Instance.Error("CheckIGRsp reponse failed", ex);               

                return false;
            }

            return true;
        }

        protected void SetAccountTimeZone()
        {
            LibDef.m_AccountTimeZone = "";
            LibDef.m_TimeZoneAcc = TimeZoneInfo.Local;


            if (string.IsNullOrEmpty(m_currentAccountID))
                return;

            if (m_currentAccountID == "XJASJ" || m_currentAccountID == "RZTNV")
                LibDef.m_AccountTimeZone = "GMT Standard Time";

            if (!string.IsNullOrEmpty(LibDef.m_AccountTimeZone))
                LibDef.m_TimeZoneAcc = TimeZoneInfo.FindSystemTimeZoneById(LibDef.m_AccountTimeZone);
        }

        protected void RaiseIGEvent(bool LoggedOn, dto.endpoint.auth.session.AccountInfo accountInfo = null)
        {
            if (m_StatusEvent == null)
                return;

            IGStatusData statusData = new IGStatusData();
            statusData.LoggedOn = LoggedOn;

            if (LoggedOn)
            {
                var account = m_AuthRsp.accounts.First(a => a.accountId == m_currentAccountID);
                if (account != null)
                    statusData.AccountName = account.accountName;

                if (accountInfo != null)
                {
                    if (accountInfo.balance.HasValue)
                        statusData.FundBalance = accountInfo.balance.Value;
                }
            }

            m_StatusEvent.UpdateIGStatus(statusData);
        }

        protected void RaiseErrorMessage(string msg)
        {
            if (m_StatusEvent == null)
                return;

            m_StatusEvent.RaiseError(msg);
        }


        #endregion

        

       
    }
}
