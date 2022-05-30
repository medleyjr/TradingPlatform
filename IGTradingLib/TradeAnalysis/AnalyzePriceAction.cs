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

namespace IGTradingLib.TradeAnalysis
{
    public enum EPriceActionEvent
    {
        none,
        pullback,
        open_trade,
        trend_dir_change

    }

    public delegate void TurnPointAddedEvent(PricePoint pricePoint);

    public class AnalyzePriceAction
    {
        InstrDataList m_HistData = null;
        int m_CurPriceDir = 0;
        bool m_InPullback = false;
        bool m_bPullbackStarting = false;
        InstrumentHistoryData m_LastSignificantBar = null;
        decimal m_MinBarHeight = 0;
        decimal m_BigBarHeight = 0;
        PricePoint m_NewTurnPoint = new PricePoint();
        public PricePoint m_LastPullbackPoint = null;
        PricePoint m_CurPullbackPoint = null;
        public List<PricePoint> m_TurnPointlist = new List<PricePoint>();
        public List<PricePoint> m_PullbackPeaklist = new List<PricePoint>();
        public List<PricePoint> m_PullbackLowlist = new List<PricePoint>();
        int m_SidewaysCount = 0; //number of bars price move sideways
        bool bTrendContinue = false;
        bool m_bMajorPullbackRequired = true;
        bool m_bMonitorTrendOnly = false;
        decimal m_PullbackPercRequired = 0.382M;

        public event TurnPointAddedEvent OnTurnPointAdded; 
        

        public void Init(InstrDataList histData, bool bMonitorTrendOnly, bool bMajorPullBackRequired, decimal pullbackPercRequired = 0.3M)
        {
            m_HistData = histData;
            m_bMajorPullbackRequired = bMajorPullBackRequired;
            m_bMonitorTrendOnly = bMonitorTrendOnly;
            m_PullbackPercRequired = pullbackPercRequired;

            m_MinBarHeight = m_HistData.AvgHeight / 10;
            m_BigBarHeight = m_HistData.AvgHeight * 3;
        }

        public int CurPriceDir
        {
            get
            {
                return m_CurPriceDir;
            }
        }

        public PricePoint LastTurnPoint
        {
            get
            {
                return m_TurnPointlist[m_TurnPointlist.Count - 1];
            }
        }

        public PricePoint LastTurnPoint2
        {
            get
            {
                return m_TurnPointlist[m_TurnPointlist.Count - 2];
            }
        }

        public PricePoint LastTurnPoint3
        {
            get
            {
                return m_TurnPointlist[m_TurnPointlist.Count - 3];
            }
        }

        public PricePoint LastTurnPoint4
        {
            get
            {
                return m_TurnPointlist[m_TurnPointlist.Count - 4];
            }
        }

        protected EPriceActionEvent ProcessCurrentBarTrendOnly(int barIndex)
        {
            decimal move = m_HistData[barIndex].GetMidClosePrice() - m_HistData[barIndex - 1].GetMidClosePrice();

            if (m_CurPriceDir == 0)
            {
                if (m_HistData[barIndex].GetBarHeight() > m_MinBarHeight)
                {
                    m_CurPriceDir = (move > 0) ? 1 : -1;
                    m_LastSignificantBar = m_HistData[barIndex];

                    m_NewTurnPoint = new PricePoint();
                    m_NewTurnPoint.TimeStamp = m_HistData[barIndex].PriceDateTime;
                    m_NewTurnPoint.Price = m_HistData[barIndex].GetMidClosePrice();

                    m_LastPullbackPoint = new PricePoint();
                    m_LastPullbackPoint.TimeStamp = m_HistData[barIndex].PriceDateTime;
                    m_LastPullbackPoint.Price = m_HistData[barIndex].GetMidOpenPrice();
                }
            }
            else
            {
                if ((m_CurPriceDir > 0 && m_HistData[barIndex].GetMidClosePrice() > m_NewTurnPoint.Price) ||
                    (m_CurPriceDir < 0 && m_HistData[barIndex].GetMidClosePrice() < m_NewTurnPoint.Price))
                {
                    m_NewTurnPoint.TimeStamp = m_HistData[barIndex].PriceDateTime;
                    m_NewTurnPoint.Price = m_HistData[barIndex].GetMidClosePrice();
                    bTrendContinue = false;
                }      

                //if bar size is signaficant
                if (m_HistData[barIndex].GetBarHeight() > m_MinBarHeight)
                {
                    if (m_HistData.IsReverseMove(barIndex, m_CurPriceDir, m_LastSignificantBar))
                    {
                        m_LastSignificantBar = m_HistData[barIndex];
                        ProcessTrendChange(barIndex);                        
                        return EPriceActionEvent.trend_dir_change;
                    }
                    else if (m_HistData.IsContinueMomentum(barIndex, m_CurPriceDir, m_LastSignificantBar))
                    {
                        m_LastSignificantBar = m_HistData[barIndex];
                        m_SidewaysCount = 0;
                    }
                }
            }

            return EPriceActionEvent.none;
        }

        public EPriceActionEvent ProcessCurrentBar(int barIndex)
        {
            if (m_bMonitorTrendOnly)
                return ProcessCurrentBarTrendOnly(barIndex);

            decimal move = m_HistData[barIndex].GetMidClosePrice() - m_HistData[barIndex - 1].GetMidClosePrice();

            if (m_CurPriceDir == 0)
            {
                if (m_HistData[barIndex].GetBarHeight() > m_MinBarHeight)
                {
                    m_CurPriceDir = (move > 0) ? 1 : -1;
                    m_LastSignificantBar = m_HistData[barIndex];

                    m_NewTurnPoint = new PricePoint();
                    m_NewTurnPoint.TimeStamp = m_HistData[barIndex].PriceDateTime;
                    m_NewTurnPoint.Price = m_HistData[barIndex].GetMidClosePrice();

                    m_LastPullbackPoint = new PricePoint();
                    m_LastPullbackPoint.TimeStamp = m_HistData[barIndex].PriceDateTime;
                    m_LastPullbackPoint.Price = m_HistData[barIndex].GetMidOpenPrice();
                }                
            }
            else //if (m_CurPriceDir > 0)
            {
                decimal oldPeakPrice = m_NewTurnPoint.Price;
                if ((m_CurPriceDir > 0 && m_HistData[barIndex].GetMidPeak(m_CurPriceDir) > m_NewTurnPoint.Price) ||
                    (m_CurPriceDir < 0 && m_HistData[barIndex].GetMidPeak(m_CurPriceDir) < m_NewTurnPoint.Price))
                {
                    m_NewTurnPoint.TimeStamp = m_HistData[barIndex].PriceDateTime;
                    m_NewTurnPoint.Price = m_HistData[barIndex].GetMidPeak(m_CurPriceDir);
                    bTrendContinue = false;
                }

                if (m_InPullback && (m_HistData[barIndex].GetMidPeak(m_CurPriceDir*-1) - m_CurPullbackPoint.Price) * m_CurPriceDir < 0)
                {
                    m_CurPullbackPoint.TimeStamp = m_HistData[barIndex].PriceDateTime;
                    m_CurPullbackPoint.Price = m_HistData[barIndex].GetMidPeak(m_CurPriceDir * -1);// GetMidClosePrice();
                }

                //if bar size is signaficant
                if (m_HistData[barIndex].GetBarHeight() > m_MinBarHeight)
                {
                    if (m_InPullback)
                    {                       
                        //is the pullback reversing
                        if (m_HistData.IsReverseMove(barIndex, m_CurPriceDir * -1, m_LastSignificantBar))
                        {                            
                            m_LastSignificantBar = m_HistData[barIndex];
                            m_SidewaysCount = 0;

                            m_InPullback = false;

                            //if close above/below previous peak/low
                            if ((m_HistData[barIndex].GetMidClosePrice() - oldPeakPrice) * m_CurPriceDir > 0)
                            {
                                m_LastPullbackPoint = m_CurPullbackPoint;
                                return EPriceActionEvent.open_trade;
                            }
                            else if (IsPullbackComplete() && m_PullbackLowlist.Count == 0) // if full pullback and no failed pullback reversals
                            {

                                m_LastPullbackPoint = m_CurPullbackPoint;
                                bTrendContinue = true;
                                return EPriceActionEvent.open_trade;
                            }
                            else
                            {
                                m_PullbackLowlist.Add(m_CurPullbackPoint.CopyObj());
                                m_PullbackPeaklist.Add(m_NewTurnPoint.CopyObj());
                            }

                            
                        }
                        //Is pullback continuing
                        else if (m_HistData.IsContinueMomentum(barIndex, m_CurPriceDir * -1, m_LastSignificantBar))
                        {
                            m_LastSignificantBar = m_HistData[barIndex];
                            
                            
                            m_SidewaysCount = 0;

                            //in a uptrend. If pullback moves below the previous pullback low point. Causing lower lows.
                            //In down trend the revers apply.
                            if (m_LastPullbackPoint != null && (m_LastPullbackPoint.Price - m_CurPullbackPoint.Price) * m_CurPriceDir > 0)
                            {
                                ProcessTrendChange(barIndex);
                                m_InPullback = false;
                                return EPriceActionEvent.trend_dir_change;
                            }                            
                        }
                        else
                        {
                            m_SidewaysCount++;
                        }
                    }
                    //is it a start of pullback
                    else if (IsPullbackStarting(barIndex)) 
                    {
                        //if the pullback reverse, reverse further than the previous pullback point
                       /* if (bTrendContinue && ((m_HistData[barIndex].GetMidClosePrice() - m_NewTurnPoint.Price) * m_CurPriceDir < 0))
                        {
                            ProcessTrendChange(barIndex);
                            return EPriceActionEvent.trend_dir_change;
                        }*/

                        if (m_CurPullbackPoint == null || m_LastPullbackPoint == m_CurPullbackPoint)
                        {
                            m_CurPullbackPoint = new PricePoint();
                            m_CurPullbackPoint.TimeStamp = m_HistData[barIndex].PriceDateTime;
                            m_CurPullbackPoint.Price = m_HistData[barIndex].GetMidPeak(m_CurPriceDir * -1); //GetMidClosePrice();
                        }

                        //in a uptrend. If pullback moves below the previous pullback low point. Causing lower lows.
                        //In down trend the revers apply.
                        if (m_LastPullbackPoint != null && (m_LastPullbackPoint.Price - m_CurPullbackPoint.Price) * m_CurPriceDir > 0)
                        {
                            ProcessTrendChange(barIndex);                           
                            return EPriceActionEvent.trend_dir_change;
                        }                         

                        m_InPullback = true;

                        m_LastSignificantBar = m_HistData[barIndex];
                        m_SidewaysCount = 0;
                        return EPriceActionEvent.pullback;
                    }
                    else if (m_HistData.IsContinueMomentum(barIndex, m_CurPriceDir, m_LastSignificantBar))
                    {
                        m_LastSignificantBar = m_HistData[barIndex];
                        m_SidewaysCount = 0;

                        if(m_PullbackPeaklist.Count > 0)
                        {
                            //decimal MaxPeakPrice = 0;
                            //if closing price is above the highest peak in pullback list
                            if (((m_LastSignificantBar.GetMidClosePrice() - oldPeakPrice) * m_CurPriceDir > 0) && IsPullbackComplete())
                            {
                                m_LastPullbackPoint = m_CurPullbackPoint;
                                return EPriceActionEvent.open_trade;
                            }
                        }
                    }
                    else
                    {
                        m_SidewaysCount++;
                    }
                }
            }
           

            return EPriceActionEvent.none;

        }

        private bool IsPullbackStarting(int barIndex)
        {
            if (m_bMajorPullbackRequired)
                return m_HistData.IsReverseMove(barIndex, m_CurPriceDir, m_LastSignificantBar);
            else
            {
                if (m_CurPriceDir > 0)
                    return (m_HistData[barIndex].GetMidClosePrice() < m_LastSignificantBar.GetMidClosePrice());
                else
                    return (m_HistData[barIndex].GetMidClosePrice() > m_LastSignificantBar.GetMidClosePrice());
            }
        }

        //test if the pullback have moved more than x% from peak to previous turning point
        //pullback between 
        private bool IsPullbackComplete()
        {
            return true;
          /*  decimal priceDiff = Math.Abs(m_NewTurnPoint.Price - m_LastPullbackPoint.Price);

            if (m_CurPriceDir > 0)
                return (m_NewTurnPoint.Price - (priceDiff * m_PullbackPercRequired) > m_CurPullbackPoint.Price);
            else
                return (m_NewTurnPoint.Price + (priceDiff * m_PullbackPercRequired) < m_CurPullbackPoint.Price);*/
                        
        }

        private void ProcessTrendChange(int barIndex)
        {
            m_NewTurnPoint.AddTimeStamp = m_HistData[barIndex].PriceDateTime;
            m_NewTurnPoint.AddPrice = m_HistData[barIndex].GetMidClosePrice();
            
            if (m_TurnPointlist.Count > 0)
                m_NewTurnPoint.Movement = m_NewTurnPoint.Price - m_TurnPointlist[m_TurnPointlist.Count - 1].Price;

            m_TurnPointlist.Add(m_NewTurnPoint);
            RaiseNewTurnPoint();
            m_LastPullbackPoint = m_NewTurnPoint;
            bTrendContinue = false;
            m_CurPriceDir *= -1;
            
            m_NewTurnPoint = new PricePoint();
            m_NewTurnPoint.TimeStamp = m_HistData[barIndex].PriceDateTime;
            m_NewTurnPoint.Price = m_HistData[barIndex].GetMidClosePrice();

            m_PullbackLowlist.Clear();
            m_PullbackPeaklist.Clear();

        }

        private void RaiseNewTurnPoint()
        {
            if (m_TurnPointlist.Count == 0 || OnTurnPointAdded == null)
                return;

            OnTurnPointAdded(LastTurnPoint);
        }
    }
}
