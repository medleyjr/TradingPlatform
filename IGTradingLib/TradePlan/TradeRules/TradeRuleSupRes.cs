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
    public class TradeRuleSupRes : ITradeRule
    {
        AnalyzeTurningPoints m_AnalyzeTurningPointsMinor = null;
        AnalyzeTurningPoints m_AnalyzeTurningPointsMajor = null;
       // AnalyzeTurningPoints m_AnalyzeAdaptTurningPoints = null;

        int m_MaxHistPeakHours = 24;
        decimal m_SupResTolerance = 0.0002M;

        bool m_bLongSignal = false;
        bool m_bShortSignal = false;
        decimal m_StopLevel = 0;
        decimal m_LimitLevel = 0;
        decimal m_Resistance = 0;
        decimal m_Support = 0;
        decimal m_BuyLevel = 0;
        decimal m_SellLevel = 0;
        decimal m_PrevTrendMove = 0;
        DateTime m_breakOutDt = new DateTime();

        public TradeRuleSupRes(AnalyzeTurningPoints aMinor, AnalyzeTurningPoints aMajor, AnalyzeTurningPoints aAdapt)
        {
            m_AnalyzeTurningPointsMinor = aMinor;
            m_AnalyzeTurningPointsMajor = aMajor;
           // m_AnalyzeAdaptTurningPoints = aAdapt;
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
            m_StopLevel = 0;
            m_LimitLevel = 0.002M;
            m_bLongSignal = false;
            m_bShortSignal = false;
            m_Resistance = 0;
            m_Support = 0;
            m_BuyLevel = 0;
            m_SellLevel = 0;
            m_PrevTrendMove = 0;
        }

        public void EndOfInterval(InstrumentHistoryData bar)
        {
            
           /* DateTime startDt = DateTime.Now;

            decimal high = bar.GetMidClosePrice();
            decimal low = bar.GetMidClosePrice();

            for (int i = 0; i < m_AnalyzeTurningPoints.Count; i++)
            {
                TimeSpan tDiff = startDt - m_AnalyzeTurningPoints[i].TimeStamp;
                if (tDiff.TotalHours > m_MaxHistPeakHours)
                    break;
            }*/

            /*if (m_AnalyzeTurningPointsMajor.Count < 2)
                return;

            if (m_Support != 0 && m_Resistance != 0)
            {
                if (bar.GetMidClosePrice() < Math.Min(m_Resistance, m_Support) || bar.GetMidClosePrice() > Math.Max(m_Resistance, m_Support))
                {
                    Reset();
                }
            }

            //if its an up trend and resistance were broken
            if (m_AnalyzeTurningPointsMajor.newTurnPoint.Price < m_AnalyzeTurningPointsMajor[0].Price &&
                m_AnalyzeTurningPointsMajor[0].Price > m_AnalyzeTurningPointsMajor[1].Price &&
                m_AnalyzeTurningPointsMajor[0].Price > m_AnalyzeTurningPointsMajor[2].Price
                && bar.GetMidClosePrice() < bar.GetMidOpenPrice() &&
                Math.Abs((bar.GetMidClosePrice() - m_AnalyzeTurningPointsMajor[2].Price)) < m_SupResTolerance)
            {
                m_Support = m_AnalyzeTurningPointsMajor[2].Price;
                m_Resistance = m_AnalyzeTurningPointsMajor[0].Price;
                m_BuyLevel = bar.GetMidOpenPrice();
            }*/

            if (m_AnalyzeTurningPointsMajor.Count < 2)
                return;

            if (m_Support != 0 && m_Resistance != 0)
            {
                //clear trade signal if price moved to much or time have elapsed
                if (Math.Abs(m_Resistance - m_Support) > m_PrevTrendMove || (m_breakOutDt < bar.PriceDateTime.AddHours(-1)))
                    Reset();

                if (m_Resistance > m_Support) //up trend
                {
                    if (bar.GetMidClosePrice() < m_Support)
                        Reset();
                    else if (m_BuyLevel == 0)
                    {
                        if (bar.GetMidHighPrice() > m_Resistance)
                            m_Resistance = bar.GetMidHighPrice();
                        else if (Math.Abs((bar.GetMidLowPrice() - m_Support)) < m_SupResTolerance)
                        {
                            m_BuyLevel = (m_Resistance - m_Support) / 2 + m_Support;
                        }
                    }
                }
                else
                {
                    if (bar.GetMidClosePrice() > m_Support)
                        Reset();
                    else if (m_SellLevel == 0)
                    {
                        if (bar.GetMidLowPrice() < m_Resistance)
                            m_Resistance = bar.GetMidLowPrice();
                        else if (Math.Abs((m_Support - bar.GetMidHighPrice())) < m_SupResTolerance)
                        {
                            m_SellLevel = m_Support - (m_Support - m_Resistance) / 2 ;
                        }
                    }
                }                
            }
            else
            {
                //test for up trend break out
                if (TestBreakOut(bar, true))
                {
                    m_Support = m_AnalyzeTurningPointsMajor[1].Price;
                    m_Resistance = bar.GetMidHighPrice();
                }
                else if (TestBreakOut(bar, false))
                {
                    m_Support = m_AnalyzeTurningPointsMajor[1].Price;
                    m_Resistance = bar.GetMidLowPrice();
                }
            }

          /*  if (m_Support != 0 && m_Resistance != 0)
            {
                if (bar.GetMidClosePrice() < Math.Min(m_Resistance, m_Support) || bar.GetMidClosePrice() > Math.Max(m_Resistance, m_Support))
                {
                    Reset();
                }
            }

            //if its an up trend and resistance were broken
            if (m_AnalyzeAdaptTurningPoints.newTurnPoint.Price < m_AnalyzeAdaptTurningPoints[0].Price &&
                m_AnalyzeAdaptTurningPoints[0].Price > m_AnalyzeAdaptTurningPoints[1].Price &&
                m_AnalyzeAdaptTurningPoints[0].Price > m_AnalyzeAdaptTurningPoints[2].Price
                && bar.GetMidClosePrice() < bar.GetMidOpenPrice() &&
                Math.Abs((bar.GetMidClosePrice() - m_AnalyzeAdaptTurningPoints[2].Price)) < m_SupResTolerance)
            {
                m_Support = m_AnalyzeAdaptTurningPoints[2].Price;
                m_Resistance = m_AnalyzeAdaptTurningPoints[0].Price;
                m_BuyLevel = bar.GetMidOpenPrice();
            }*/
        }

        public void OnStreamPrice(DataStreamUpdateEvent dataPrice)
        {
            if (m_BuyLevel != 0 && dataPrice.PriceData.AskPrice > m_BuyLevel)
            {
                m_bLongSignal = true;
                m_StopLevel = m_Support - m_SupResTolerance;
                m_LimitLevel = dataPrice.PriceData.AskPrice + m_LimitLevel;
            }
            else if (m_SellLevel != 0 && dataPrice.PriceData.BidPrice < m_SellLevel)
            {
                m_bShortSignal = true;
                m_StopLevel = m_Support + m_SupResTolerance;
                m_LimitLevel = dataPrice.PriceData.BidPrice - m_LimitLevel;
            }
        }

        public bool TestBreakOut(InstrumentHistoryData bar, bool bUpTrend)
        {
            decimal tm = -1; //define trend multiplier
            decimal peak = bar.GetMidLowPrice();
            if (bUpTrend)
            {
                tm = 1;
                peak = bar.GetMidHighPrice();
            }

            decimal prevTrendMove = (m_AnalyzeTurningPointsMajor[1].Price - m_AnalyzeTurningPointsMajor[0].Price)*tm;
            if (prevTrendMove > 0 &&
                (peak - m_AnalyzeTurningPointsMajor[0].Price)*tm > (prevTrendMove*1.4M)) //is the breakout more than 40% than the previous trend move                
            {
                bool minorTPBelow = false;
                //This explanation is for uptrend. If we have a minor turnpoint below break line and then above break line reading from right
                for (int i = 0; i < m_AnalyzeTurningPointsMinor.Count; i++)
                {
                    if (m_AnalyzeTurningPointsMinor[i].TimeStamp <= m_AnalyzeTurningPointsMajor[0].TimeStamp)
                        break;

                    if ((m_AnalyzeTurningPointsMajor[1].Price - m_AnalyzeTurningPointsMinor[i].Price) * tm > 0)
                    {
                        minorTPBelow = true;
                        continue;
                    }

                    if (minorTPBelow && (m_AnalyzeTurningPointsMinor[i].Price - m_AnalyzeTurningPointsMajor[1].Price) * tm > 0)
                        return false;

                }
                m_PrevTrendMove = prevTrendMove;
                m_breakOutDt = bar.PriceDateTime;
                return true;
            }

            return false;
        }
    }
}
