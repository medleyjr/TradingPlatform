using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGTradingLib.TradePlan.Data;
using IGTradingLib.DataModel;
using IGTradingLib.Types;
using IGTradingLib.TradeAnalysis;
using IGTradingLib.TradePlan.TradeRules;

namespace IGTradingLib.TradePlan
{
    

    public class TradePlanPriceActionData : TradePlanData
    {
        public TradePlanPriceActionData()
        {
            MaxPositionsOpen = 3;
            FirstTradePerc = 0.15M;
        }

        public int MAPrice { get; set; }
        public int PositionCount { get; set; }
        public int HistoryLengthRequired { get; set; } //hours
        public int MaxPositionsOpen { get; set; }
        public decimal FirstTradePerc { get; set; }

        public string MajorResolution { get; set; }
        public int MajorHistoryLengthRequired { get; set; } //days
    }

    public class TradePlanPriceAction : TradePlanBase
    {
        InstrDataList m_HistData = new InstrDataList();
        InstrDataList m_HistMajorData = new InstrDataList();
       // int m_CurPriceDir = 0;
       // bool m_InPullback = false;
       // InstrumentHistoryData m_LastSignificantBar = null;
       // decimal m_MinBarHeight = 0;
       // decimal m_BigBarHeight = 0;
        AnalyzePriceAction m_AnalyzePriceAction = new AnalyzePriceAction();
        AnalyzePriceAction m_AnalyzeMajorPriceAction = new AnalyzePriceAction();

        EPriceActionEvent m_EPriceActionEvent = EPriceActionEvent.none;
        decimal m_MinTradingRange = 0;
        decimal m_DistBeforeSupRes = 0;
        decimal m_ResistancePrice = 0;
        decimal m_SupportPrice = 0;
        bool m_IsTrending = false;
        decimal m_TradingRangePerc = 0.3M;
        TradePosition m_TradePos = null;

        protected TradePlanPriceActionData TradePlanPAData
        {
            get
            {
                return (TradePlanPriceActionData)m_TradePlanData;
            }
        }

        protected override void Load()
        {
            LoadTradePlanData<TradePlanPriceActionData>();

            if (!string.IsNullOrEmpty(TradePlanPAData.MajorResolution))
                m_MajorResolution = TradePlanPAData.MajorResolution;
           
            var tmpList = DB.Rep.FindHistData(TradePlanPAData.Epic, TradePlanPAData.Resolution, m_TradeParam.CurrentDateTime.AddHours(TradePlanPAData.HistoryLengthRequired * -1), m_TradeParam.CurrentDateTime);
            m_HistData.AddRange(tmpList);
            m_AnalyzePriceAction.Init(m_HistData, false, true);

            var tmpMajorHist = DB.Rep.FindHistData(TradePlanPAData.Epic, TradePlanPAData.MajorResolution, m_TradeParam.CurrentDateTime.AddDays(TradePlanPAData.MajorHistoryLengthRequired * -1), m_TradeParam.CurrentDateTime);
            m_HistMajorData.AddRange(tmpMajorHist);
            m_AnalyzeMajorPriceAction.Init(m_HistMajorData, true, false);

            m_MinTradingRange = m_HistMajorData.AvgHeight * 2;
            m_DistBeforeSupRes = m_MinTradingRange / 8;

            for (int i = 1; i < m_HistData.Count - 1; i++ )
            {
                ProcessCurrentBar(i);
            }           

        }

        private void ProcessCurrentBar(int barIndex)
        {
            m_TradePos = null;
            m_EPriceActionEvent = m_AnalyzePriceAction.ProcessCurrentBar(barIndex);

            if (!m_IsTrending)
            {
                InstrumentHistoryData dataBar = m_HistData[barIndex];

                if (dataBar.IsUp() && HasPriceIncrPerc(m_ResistancePrice, dataBar.GetMidClosePrice(), m_TradingRangePerc))
                {
                    m_TradePos = new TradePosition();
                    m_TradePos.DealSize = (decimal)TradePlanPAData.PositionCount;
                    m_TradePos.PositionDirection = LibDef.BUY;
                    m_TradePos.FailureLevel = m_ResistancePrice * 0.999M;
                    m_SupportPrice = m_ResistancePrice;
                    m_ResistancePrice = 0;
                    m_IsTrending = true;
                }
                else if (!dataBar.IsUp() && HasPriceIncrPerc(dataBar.GetMidClosePrice(), m_SupportPrice, m_TradingRangePerc))
                {
                    m_TradePos = new TradePosition();
                    m_TradePos.DealSize = (decimal)TradePlanPAData.PositionCount;
                    m_TradePos.PositionDirection = LibDef.SELL;
                    m_TradePos.FailureLevel = m_SupportPrice * 1.001M;
                    m_ResistancePrice = m_SupportPrice;
                    m_SupportPrice = 0;
                    m_IsTrending = true;
                }
            }
            else if (m_EPriceActionEvent == EPriceActionEvent.trend_dir_change && m_AnalyzePriceAction.m_TurnPointlist.Count > 2)
            {
                if (m_AnalyzePriceAction.CurPriceDir > 0)
                {
                    if(m_IsTrending && m_AnalyzePriceAction.LastTurnPoint.Price < m_AnalyzePriceAction.LastTurnPoint3.Price)
                        m_SupportPrice = m_AnalyzePriceAction.LastTurnPoint.Price;
                    //do not adjust support price if we are already in norrow trading range
                    else if (!HasPriceIncrPerc(m_AnalyzePriceAction.LastTurnPoint.Price, m_AnalyzePriceAction.LastTurnPoint3.Price, m_TradingRangePerc) && !IsInTradingRange)
                        m_SupportPrice = m_AnalyzePriceAction.LastTurnPoint.Price;

                    if (!HasPriceIncrPerc(m_AnalyzePriceAction.LastTurnPoint.Price, m_AnalyzePriceAction.LastTurnPoint3.Price, m_TradingRangePerc) && IsInTradingRange)
                        m_IsTrending = false;
                }
                else
                {
                    //if we are trending and a new higher high was made, then the new high is the resistance
                    if (m_IsTrending && m_AnalyzePriceAction.LastTurnPoint.Price > m_AnalyzePriceAction.LastTurnPoint3.Price)
                        m_ResistancePrice = m_AnalyzePriceAction.LastTurnPoint.Price;       
                    //if a lower high is made and we are not in a trading range, then make lower high resistance. Else keep resistance at current level
                    else if (!HasPriceIncrPerc(m_AnalyzePriceAction.LastTurnPoint3.Price, m_AnalyzePriceAction.LastTurnPoint.Price, m_TradingRangePerc) && !IsInTradingRange)
                        m_ResistancePrice = m_AnalyzePriceAction.LastTurnPoint.Price;

                    //if the last turning point did not go past the previous peak turning point
                    if (!HasPriceIncrPerc(m_AnalyzePriceAction.LastTurnPoint3.Price, m_AnalyzePriceAction.LastTurnPoint.Price, m_TradingRangePerc) && IsInTradingRange)
                        m_IsTrending = false;
                }

            }
        }

        private bool IsInTradingRange
        {
            get
            {
                return m_SupportPrice != 0 && m_ResistancePrice != 0 && ((m_ResistancePrice - m_SupportPrice) < m_MinTradingRange);
            }
        }

        public bool HasPriceIncrPerc(decimal priceFrom, decimal priceTo, decimal perc)
        {
            if (priceTo <= priceFrom)
                return false;

            //if diff in price > than percentage of price from 
            return ((priceTo - priceFrom) > (priceFrom * (perc / 100)));
        }

        protected override void EndOfMajorInterval(InstrumentHistoryData bar)
        {

        }

        protected override void EndOfInterval(InstrumentHistoryData bar)
        {
            m_HistData.Add(bar);

            ProcessCurrentBar(m_HistData.Count - 1);

            /*if (m_EPriceActionEvent == EPriceActionEvent.open_trade)
            {
                if(m_AnalyzePriceAction.CurPriceDir > 0)
                    CreatePosition(LibDef.BUY, (decimal)TradePlanPAData.PositionCount, m_AnalyzePriceAction.m_ReversalPoint.Price, bar.GetMidClosePrice()*1.005M);
                else
                    CreatePosition(LibDef.SELL, (decimal)TradePlanPAData.PositionCount, m_AnalyzePriceAction.m_ReversalPoint.Price, bar.GetMidClosePrice()*0.995M);
            }*/

            CloseFailedPositions();

          /*  if (m_TradePos != null)
            {
                CreatePosition(m_TradePos);
            }    */

            if (!m_IsTrending)
                return;

            if (m_EPriceActionEvent == EPriceActionEvent.trend_dir_change)
                CloseAllPositions();
          
            if (m_EPriceActionEvent == EPriceActionEvent.open_trade)
            //if(TestPricePatterns())
            {
                /*if(m_EPriceActionEvent == EPriceActionEvent.trend_dir_change)
                    CloseAllPositions();*/
                
                if((m_LongPositions.Count + m_ShortPositions.Count) < TradePlanPAData.MaxPositionsOpen)
                {
                    TradePosition tradePos = new TradePosition();
                    tradePos.DealSize = (decimal)TradePlanPAData.PositionCount;

                    if (m_AnalyzePriceAction.CurPriceDir > 0)
                    {
                        tradePos.PositionDirection = LibDef.BUY;// CreatePosition(LibDef.BUY, (decimal)TradePlanPAData.PositionCount, m_AnalyzePriceAction.m_ReversalPoint.Price, bar.GetMidClosePrice() * 1.035M);
                        tradePos.FailureLevel = m_HistData.LastBar.OpenAskPrice.Value * 0.999M;
                    }
                    else
                    {
                        tradePos.PositionDirection = LibDef.SELL; //CreatePosition(LibDef.SELL, (decimal)TradePlanPAData.PositionCount, m_AnalyzePriceAction.m_ReversalPoint.Price, bar.GetMidClosePrice() * 0.965M);
                        tradePos.FailureLevel = m_HistData.LastBar.OpenBidPrice.Value * 1.001M;
                    }

                    CreatePosition(tradePos);
                }

            }
        }

        protected override void OnStreamPrice(DataStreamUpdateEvent dataPrice)
        {
            for (int i = m_LongPositions.Count - 1; i >= 0; i--)
            {
                var pos = m_LongPositions[i];
                if (pos.TradeOpenSize > 0 && pos.TradeOpenSize == pos.DealSize)
                {
                    if (HasPriceIncrPerc(pos.Price, dataPrice.PriceData.BidPrice, TradePlanPAData.FirstTradePerc))
                    {
                        int size = Math.Max((int)pos.TradeOpenSize / 2, 1);
                        ClosePosition(pos, size);
                    }
                }
            }

            for (int i = m_ShortPositions.Count - 1; i >= 0; i--)
            {
                var pos = m_ShortPositions[i];

                if (pos.TradeOpenSize > 0 && pos.TradeOpenSize == pos.DealSize)
                {
                    if (HasPriceIncrPerc(dataPrice.PriceData.AskPrice, pos.Price, TradePlanPAData.FirstTradePerc))
                    {
                        int size = Math.Max((int)pos.TradeOpenSize / 2, 1);
                        ClosePosition(pos, size);
                    }
                }                
            }
        }

        private void CloseFailedPositions()
        {
            InstrumentHistoryData dataBar = m_HistData.LastBar;

            for (int i = m_LongPositions.Count - 1; i >= 0; i--)
            {
                var pos = m_LongPositions[i];

                if(dataBar.CloseBidPrice < pos.FailureLevel)
                    ClosePosition(pos, pos.DealSize);                               
            }

            for (int i = m_ShortPositions.Count - 1; i >= 0; i--)
            {
                var pos = m_ShortPositions[i];

                if (dataBar.CloseAskPrice > pos.FailureLevel)
                    ClosePosition(pos, pos.DealSize);                 
            }
        }

        private bool TestPricePatterns()
        {
            if (m_AnalyzePriceAction.m_TurnPointlist.Count < 4)
                return false;

            decimal dist = Math.Abs(m_AnalyzePriceAction.LastTurnPoint4.Price - m_AnalyzePriceAction.LastTurnPoint3.Price);
            if (dist < (m_HistMajorData.AvgHeight / 3))
                return false;

            if (m_AnalyzePriceAction.LastTurnPoint4.Price > m_AnalyzePriceAction.LastTurnPoint3.Price &&
                 m_AnalyzePriceAction.LastTurnPoint2.Price > (m_AnalyzePriceAction.LastTurnPoint3.Price + dist * 0.33M) &&
                 m_AnalyzePriceAction.LastTurnPoint2.Price < (m_AnalyzePriceAction.LastTurnPoint4.Price - dist * 0.33M) &&
                 m_AnalyzePriceAction.LastTurnPoint.Price > m_AnalyzePriceAction.LastTurnPoint3.Price &&
                 m_HistData.LastBar.GetMidClosePrice() > m_AnalyzePriceAction.LastTurnPoint2.Price)
            {
                return true;
            }

            return false;
        }

        
    }
}
