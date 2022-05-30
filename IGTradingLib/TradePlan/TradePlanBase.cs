using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Medley.Common.Utils;

using dto.endpoint.positions.create.otc.v1;
using dto.endpoint.positions.get.otc.v1;
using dto.endpoint.positions.close.v1;
using dto.endpoint.positions.edit.v1;

using IGTradingLib.Types;
using IGTradingLib.TradePlan.Data;

using Newtonsoft.Json;
using IGTradingLib.DataModel;

namespace IGTradingLib.TradePlan
{
    public enum ETradePlanType
    {
        simple_ma = 1,
        generic = 2,
        price_action = 3
    }

    public static class TradePlanMgr
    {
        public static TradePlanBase CreateTradePlan(int type)
        {
            switch ((ETradePlanType)type)
            {
                case ETradePlanType.simple_ma:
                    return new TradePlanMA();
                case ETradePlanType.generic:
                    return new TradePlanGeneric();
                case ETradePlanType.price_action:
                    return new TradePlanPriceAction();

            }

            return null;
        }
    }

    public abstract class TradePlanBase
    {
        protected TradePlanData m_TradePlanData = null;
        protected TradeParam m_TradeParam = null;
        protected ITradingSystem m_TradingSystem = null;
        protected ITradingHost m_TradingHost = null;
        protected DataStreamUpdateEvent m_CurrentDataPrice = null;
        protected InstrumentHistoryData m_CurrentBar = null;//new InstrumentHistoryData();
        protected InstrumentHistoryData m_MajorCurrentBar = null;
        protected InstrumentDataID m_DataID = null;
        protected InstrumentDetails m_InstrumentDetails = null;
        protected object m_syncBar = new object();
        protected List<TradePosition> m_LongPositions = new List<TradePosition>();
        protected List<TradePosition> m_ShortPositions = new List<TradePosition>();
        protected string m_MajorResolution = "";

        public void Init(TradeParam param, ITradingHost tradingHost, ITradingSystem tradingSystem)
        {           
            m_TradeParam = param;
            m_TradingSystem = tradingSystem;
            m_TradingHost = tradingHost;
            Load();
            m_DataID = DB.Rep.GetIntrumentDataID(m_TradePlanData.Epic);
            m_InstrumentDetails = DB.Rep.GetIntrumentDetails(m_TradePlanData.Epic);
            LibDef.OnStreamEvents[m_DataID.ID] = AddStreamPrice;            

            m_TradingHost.RegisterIntervalEventHandler(m_TradePlanData.Resolution, IntervalElapsed);
                       
        }

        public TradePlanData TradePlanData
        {
            get
            {
                return m_TradePlanData;
            }
        }

        protected void LoadTradePlanData<T>() where T : TradePlanData
        {
            if (m_TradeParam.TradePlan != null)
            {
                m_TradePlanData = JsonConvert.DeserializeObject<T>(m_TradeParam.TradePlan.Data);
            }
        }

        protected abstract void Load();
        protected abstract void EndOfInterval(InstrumentHistoryData bar);
        protected abstract void OnStreamPrice(DataStreamUpdateEvent dataPrice);

        protected virtual void EndOfMajorInterval(InstrumentHistoryData bar)
        {

        }

       // public abstract void StartTrade(TradeParam param);
        //public abstract int GetDataID();

        protected void IntervalElapsed(DateTime elapsedDt)
        {
            lock (m_syncBar)
            {
                if (m_CurrentBar != null)
                {
                    m_CurrentBar.PriceDateTime = elapsedDt;

                    if(!string.IsNullOrEmpty(m_MajorResolution))
                        ProcessMajorResolution(elapsedDt);

                    EndOfInterval(m_CurrentBar);
                    m_CurrentBar = null;
                }
            }
        }

        protected void ProcessMajorResolution(DateTime elapsedDt)
        {
            bool newBar = false;

            //check if new bar
            if (m_MajorCurrentBar != null && TradingDBManage.IsNewBar(m_MajorResolution, m_MajorCurrentBar.PriceDateTime, elapsedDt))
            {
                newBar = true;
                EndOfMajorInterval(m_MajorCurrentBar);
               // lastPriceDt = dataItem.PriceDateTime;
            }

            //update values
            if (!newBar && m_MajorCurrentBar != null)
            {
                if (m_CurrentBar.HighAskPrice > m_MajorCurrentBar.HighAskPrice)
                    m_MajorCurrentBar.HighAskPrice = m_CurrentBar.HighAskPrice;

                if (m_CurrentBar.HighBidPrice > m_MajorCurrentBar.HighBidPrice)
                    m_MajorCurrentBar.HighBidPrice = m_CurrentBar.HighBidPrice;

                if (m_CurrentBar.LowAskPrice < m_MajorCurrentBar.LowAskPrice)
                    m_MajorCurrentBar.LowAskPrice = m_CurrentBar.LowAskPrice;

                if (m_CurrentBar.LowBidPrice < m_MajorCurrentBar.LowBidPrice)
                    m_MajorCurrentBar.LowBidPrice = m_CurrentBar.LowBidPrice;

                m_MajorCurrentBar.CloseBidPrice = m_CurrentBar.CloseBidPrice;
                m_MajorCurrentBar.CloseAskPrice = m_CurrentBar.CloseAskPrice;
                m_MajorCurrentBar.LastTradedVolume += m_CurrentBar.LastTradedVolume;

               // newBarItemCount++;

            }

            //save previous bar
            if (newBar)
            {
                //m_MajorCurrentBar.DateModified = DateTime.Now;
              //  EndOfMajorInterval(m_MajorCurrentBar);
                //ignore midnight bar for now
                //if (!(m_MajorResolution == "DAY" && newBarItemCount == 1))
                 //   newDataList.Add(newDataItem);
            }

            //check if new bar must be created
            if (newBar || m_MajorCurrentBar  == null)
            {
                m_MajorCurrentBar = (InstrumentHistoryData)m_CurrentBar.CloneObject();
                m_MajorCurrentBar.ID = 0;
                m_MajorCurrentBar.Resolution = m_MajorResolution;
                //newBarItemCount = 1;

                if (m_MajorResolution == "DAY" || m_MajorResolution == "WEEK" || m_MajorResolution == "MONTH")
                    m_MajorCurrentBar.PriceDateTime = m_CurrentBar.PriceDateTime.Date;
            }
        }

        protected void AddStreamPrice(DataStreamUpdateEvent dataPrice)
        {           

            lock (m_syncBar)
            {
                m_CurrentDataPrice = dataPrice;
                OnStreamPrice(dataPrice);

                if (m_CurrentBar == null)
                {
                    m_CurrentBar = new InstrumentHistoryData();
                    m_CurrentBar.DataID = m_DataID.ID;
                    m_CurrentBar.Resolution = m_TradePlanData.Resolution;

                    m_CurrentBar.CloseAskPrice = dataPrice.PriceData.AskPrice;
                    m_CurrentBar.CloseBidPrice = dataPrice.PriceData.BidPrice;
                    m_CurrentBar.OpenAskPrice = dataPrice.PriceData.AskPrice;
                    m_CurrentBar.OpenBidPrice = dataPrice.PriceData.BidPrice;
                    m_CurrentBar.HighAskPrice = dataPrice.PriceData.AskPrice;
                    m_CurrentBar.HighBidPrice = dataPrice.PriceData.BidPrice;
                    m_CurrentBar.LowAskPrice = dataPrice.PriceData.AskPrice;
                    m_CurrentBar.LowBidPrice = dataPrice.PriceData.BidPrice;
                    //m_CurrentBar.PriceDateTime = dataPrice.PriceData.PriceDatetime;
                }
                else
                {
                    if (dataPrice.PriceData.AskPrice > m_CurrentBar.HighAskPrice)
                        m_CurrentBar.HighAskPrice = dataPrice.PriceData.AskPrice;

                    if (dataPrice.PriceData.BidPrice > m_CurrentBar.HighBidPrice)
                        m_CurrentBar.HighBidPrice = dataPrice.PriceData.BidPrice;

                    if (dataPrice.PriceData.AskPrice < m_CurrentBar.LowAskPrice)
                        m_CurrentBar.LowAskPrice = dataPrice.PriceData.AskPrice;

                    if (dataPrice.PriceData.BidPrice < m_CurrentBar.LowBidPrice)
                        m_CurrentBar.LowBidPrice = dataPrice.PriceData.BidPrice;

                    m_CurrentBar.CloseBidPrice = dataPrice.PriceData.BidPrice;
                    m_CurrentBar.CloseAskPrice = dataPrice.PriceData.AskPrice;
                }
            }
        }

        protected void CreatePosition(TradePosition tradePos)
        {
            if (!tradePos.StopLevel.HasValue && m_CurrentDataPrice != null && m_CurrentDataPrice.PriceData != null)
            {
                if (tradePos.PositionDirection == LibDef.BUY)
                    tradePos.StopLevel = m_CurrentDataPrice.PriceData.AskPrice * 0.995M;
                else
                    tradePos.StopLevel = m_CurrentDataPrice.PriceData.AskPrice * 1.005M;
            }

            if (!tradePos.LimitLevel.HasValue && m_CurrentDataPrice != null && m_CurrentDataPrice.PriceData != null)
            {
                if (tradePos.PositionDirection == LibDef.BUY)
                    tradePos.LimitLevel = m_CurrentDataPrice.PriceData.BidPrice * 1.05M;
                else
                    tradePos.LimitLevel = m_CurrentDataPrice.PriceData.BidPrice * 0.95M;
            }

            tradePos.TradeOpenSize = tradePos.DealSize;

            CreatePositionRequest req = new CreatePositionRequest();
            req.currencyCode = m_InstrumentDetails.CurrencyCode;
            req.direction = tradePos.PositionDirection;
            req.stopLevel = tradePos.StopLevel;
            req.limitLevel = tradePos.LimitLevel;
            req.size = tradePos.DealSize;
            req.epic = m_InstrumentDetails.Epic;
            req.forceOpen = false;
            req.guaranteedStop = false;
            req.orderType = "MARKET";
            req.expiry = "-";

            var rsp = m_TradingSystem.CreatePosition(req);

            if (rsp != null)
            {
                tradePos.DealID = rsp.dealId;               

                if (rsp.level.HasValue)
                    tradePos.Price = rsp.level.Value;

                if (tradePos.PositionDirection == LibDef.BUY)
                    m_LongPositions.Add(tradePos);
                else
                    m_ShortPositions.Add(tradePos);
            }         
        }

        protected void CreatePosition(string direction, decimal size, decimal stopLevel, decimal limitLevel)
        {
            TradePosition tradePos = new TradePosition();
            tradePos.DealSize = size;
            tradePos.StopLevel = stopLevel;
            tradePos.LimitLevel = limitLevel;
            tradePos.PositionDirection = direction;

            CreatePosition(tradePos);

            /*CreatePositionRequest req = new CreatePositionRequest();
            req.currencyCode = m_InstrumentDetails.CurrencyCode;
            req.direction = direction;
            req.stopLevel = stopLevel;
            req.limitLevel = limitLevel;
            req.size = size;
            req.epic = m_InstrumentDetails.Epic;
            req.forceOpen = false;
            req.guaranteedStop = false;
            req.orderType = "MARKET";
            req.expiry = "-";

            var rsp = m_TradingSystem.CreatePosition(req);

            if (rsp != null)
            {
                TradePosition pos = new TradePosition();
                pos.DealID = rsp.dealId;
                pos.DealSize = size;
                pos.StopLevel = stopLevel;
                pos.LimitLevel = limitLevel;
                pos.PositionDirection = direction;
                
                if(rsp.level.HasValue)
                    pos.Price = rsp.level.Value;

                if (direction == LibDef.BUY)
                    m_LongPositions.Add(pos);
                else
                    m_ShortPositions.Add(pos);
            }  */                      
        }

        protected void ClosePosition(TradePosition pos, decimal size)
        {
            ClosePositionRequest req = new ClosePositionRequest();
            req.dealId = pos.DealID;            
            req.orderType = "MARKET";
            req.size = size;

            if (pos.PositionDirection == LibDef.BUY)
                req.direction = LibDef.SELL;
            else
                req.direction = LibDef.BUY;

            var rsp = m_TradingSystem.ClosePosition(req);

            if (rsp != null)
            {
                pos.DealSize -= size;

                if (pos.DealSize == 0)
                {
                    if (pos.PositionDirection == LibDef.BUY)
                        m_LongPositions.Remove(pos);
                    else
                        m_ShortPositions.Remove(pos);
                }
            }
        }

        protected bool HasPositionsOpen()
        {
            return (m_LongPositions.Count > 0 || m_ShortPositions.Count > 0);            
        }

        protected void CloseAllPositions()
        {
            for (int i = m_LongPositions.Count - 1; i >= 0; i--)
            {
                var pos = m_LongPositions[i];
                ClosePosition(pos, pos.DealSize);                
            }

            for (int i = m_ShortPositions.Count - 1; i >= 0; i--)
            {
                var pos = m_ShortPositions[i];
                ClosePosition(pos, pos.DealSize);                
            }
        }
    }
}
