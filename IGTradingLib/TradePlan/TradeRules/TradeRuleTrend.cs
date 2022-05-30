using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGTradingLib.TradePlan.Data;
using IGTradingLib.DataModel;
using IGTradingLib.Types;
using IGTradingLib.TradeAnalysis;

namespace IGTradingLib.TradePlan.TradeRules
{
    public class TradeRuleTrend : ITradeRule
    {
        AnalyzeTurningPoints m_AnalyzeTurningPointsMinor = null;
        AnalyzeTurningPoints m_AnalyzeTurningPointsMajor = null;
        List<InstrumentHistoryData> m_HistData = null;
        decimal m_TrendMoveReq = 0;
        decimal m_Tolerance = 0;

        bool m_bLongSignal = false;
        bool m_bShortSignal = false;
        bool m_UpTrendFormed = false;
        bool m_DownTrendFormed = false;
        decimal m_PeakPriceLevel = 0;
        decimal m_StopLevel = 0;
        decimal m_LimitLevel = 0;

        public TradeRuleTrend(AnalyzeTurningPoints a, AnalyzeTurningPoints aMajor, List<InstrumentHistoryData> histData, decimal m)
        {
            m_AnalyzeTurningPointsMinor = a;
            m_AnalyzeTurningPointsMajor = aMajor;
            m_Tolerance = m;
            m_HistData = histData;
            Reset();
        }

        public bool HasLongTradeSignal()
        {
            return m_bLongSignal;
        }

        public bool HasShortTradeSignal()
        {
            return m_bShortSignal;
        }

        public decimal GetStopLevel()
        {
            return m_StopLevel;
        }

        public decimal GetLimitLevel()
        {
            return m_LimitLevel;
        }

        public void Reset()
        {
            m_UpTrendFormed = false;
            m_DownTrendFormed = false;
            m_PeakPriceLevel = 0;
            m_StopLevel = 0;
            m_LimitLevel = 0;
            m_bLongSignal = false;
            m_bShortSignal = false;
        }

        public void EndOfInterval(InstrumentHistoryData bar)
        {
            /*if (!m_bShortSignal && m_AnalyzeTurningPointsMinor.Count > 3)
            {
                TestTrend(m_AnalyzeTurningPointsMinor, bar);
            }*/

            if (!m_DownTrendFormed && !m_UpTrendFormed && m_AnalyzeTurningPointsMajor.Count > 3)
            {
                TestTrend(m_AnalyzeTurningPointsMajor, bar);
            }

            if (m_DownTrendFormed)
            {
                if (Indicators.TestPriceReversal(m_HistData, true))
                {
                    m_bShortSignal = true;
                    m_LimitLevel = bar.CloseAskPrice.Value * 0.98M;
                }
                else if (bar.GetMidClosePrice() > m_StopLevel)
                    Reset();
            }
            else if (m_UpTrendFormed)
            {
                if (Indicators.TestPriceReversal(m_HistData, false))
                {
                    m_bLongSignal = true;
                    m_LimitLevel = bar.CloseAskPrice.Value * 1.02M;
                }
                else if (bar.GetMidClosePrice() < m_StopLevel)
                    Reset();
            }

        }

        private void TestTrend(AnalyzeTurningPoints analyzeTurningPoints, InstrumentHistoryData bar)
        {
            //find pullback of down trend
            if (analyzeTurningPoints.newTurnPoint.Price > analyzeTurningPoints[0].Price && analyzeTurningPoints[1].Price < analyzeTurningPoints[3].Price)
            {
                decimal priceDiff = analyzeTurningPoints[3].Price - analyzeTurningPoints[1].Price;
                TimeSpan timeDiff = analyzeTurningPoints[1].TimeStamp - analyzeTurningPoints[3].TimeStamp;
                TimeSpan newTimeDiff = bar.PriceDateTime - analyzeTurningPoints[1].TimeStamp;
                decimal expectedPrice = analyzeTurningPoints[1].Price - (priceDiff / (decimal)timeDiff.TotalMinutes) * (decimal)newTimeDiff.TotalMinutes;

                if (bar.GetMidHighPrice() == analyzeTurningPoints.newTurnPoint.Price && Math.Abs(bar.GetMidHighPrice() - expectedPrice) < m_Tolerance)
                {
                    m_DownTrendFormed = true;
                    m_StopLevel = bar.GetMidHighPrice() + m_Tolerance;
                }
            }//find pullback of down trend
            else if (analyzeTurningPoints.newTurnPoint.Price < analyzeTurningPoints[0].Price && analyzeTurningPoints[1].Price > analyzeTurningPoints[3].Price)
                {
                    decimal priceDiff = analyzeTurningPoints[1].Price - analyzeTurningPoints[3].Price;
                    TimeSpan timeDiff = analyzeTurningPoints[1].TimeStamp - analyzeTurningPoints[3].TimeStamp;
                    TimeSpan newTimeDiff = bar.PriceDateTime - analyzeTurningPoints[1].TimeStamp;
                    decimal expectedPrice = analyzeTurningPoints[1].Price + (priceDiff / (decimal)timeDiff.TotalMinutes) * (decimal)newTimeDiff.TotalMinutes;

                    if (bar.GetMidLowPrice() == analyzeTurningPoints.newTurnPoint.Price && Math.Abs(bar.GetMidLowPrice() - expectedPrice) < m_Tolerance)
                    {
                        m_UpTrendFormed = true;
                        m_StopLevel = bar.GetMidLowPrice() - m_Tolerance;
                    }
                }
        }

        public void OnStreamPrice(DataStreamUpdateEvent dataPrice)
        {

        }

        /*public void OnStreamPrice(DataStreamUpdateEvent dataPrice)
        {
            if (m_AnalyzeTurningPointsMinor.Count < 3)
                return;

            if (m_UpTrendFormed)
            {
                if (m_StopLevel < m_AnalyzeTurningPointsMinor[1].Price && dataPrice.PriceData.AskPrice >= m_PeakPriceLevel)
                {
                    m_bLongSignal = true;
                    m_LimitLevel = dataPrice.PriceData.AskPrice + m_LimitLevel;
                }
                else if (dataPrice.PriceData.AskPrice < m_StopLevel)
                    m_StopLevel = dataPrice.PriceData.AskPrice;
                else if (dataPrice.PriceData.AskPrice > m_PeakPriceLevel)
                    m_PeakPriceLevel = dataPrice.PriceData.AskPrice;
                
            }
            else if (m_DownTrendFormed)
            {
                if (m_StopLevel > m_AnalyzeTurningPointsMinor[1].Price && dataPrice.PriceData.BidPrice <= m_PeakPriceLevel)
                {
                    m_bShortSignal = true;
                    m_LimitLevel = dataPrice.PriceData.AskPrice - m_LimitLevel;
                }
                else if (dataPrice.PriceData.BidPrice > m_StopLevel)
                    m_StopLevel = dataPrice.PriceData.BidPrice;
                else if (dataPrice.PriceData.BidPrice < m_PeakPriceLevel)
                    m_PeakPriceLevel = dataPrice.PriceData.BidPrice;
            }


            if (!m_UpTrendFormed && m_AnalyzeTurningPointsMinor[0].Price > m_AnalyzeTurningPointsMinor[2].Price)
             //   m_AnalyzeTurningPoints.newTurnPoint.Price > m_AnalyzeTurningPoints.m_TurnPointlist[1].Price)
            {
                //decimal upPeak = Math.Max(m_PeakPriceLevel, m_AnalyzeTurningPoints.newTurnPoint.Price);
                if (dataPrice.PriceData.AskPrice > (m_AnalyzeTurningPointsMinor[1].Price + m_TrendMoveReq))
                {
                    Reset();
                    m_UpTrendFormed = true;
                    m_PeakPriceLevel = dataPrice.PriceData.AskPrice;
                    m_StopLevel = dataPrice.PriceData.AskPrice;                  
                }

            }
            else if (!m_DownTrendFormed && m_AnalyzeTurningPointsMinor[0].Price < m_AnalyzeTurningPointsMinor[2].Price)
                //m_AnalyzeTurningPoints.newTurnPoint.Price < m_AnalyzeTurningPoints.m_TurnPointlist[1].Price)
            {
                if (dataPrice.PriceData.BidPrice < (m_AnalyzeTurningPointsMinor[1].Price - m_TrendMoveReq))
                {
                    Reset();
                    m_DownTrendFormed = true;
                    m_PeakPriceLevel = dataPrice.PriceData.BidPrice;
                    m_StopLevel = dataPrice.PriceData.BidPrice;                  

                }
            }
                
        }*/

    }
}
