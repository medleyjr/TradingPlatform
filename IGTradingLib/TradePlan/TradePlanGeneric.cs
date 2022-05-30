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
    public class TradePlanGenericData : TradePlanData
    {
        public int MAShort { get; set; }
        public decimal MAShortMaxDist { get; set; }
        public int MALong { get; set; }
        public decimal MALongMaxDist { get; set; }
        public int HistoryLengthRequired { get; set; } //hours
        public decimal MaxMAOffset { get; set; }
        public decimal StopDistance { get; set; }
        public int PositionCount { get; set; }
        public decimal ExitLevel1 { get; set; }
        public decimal ExitLevel2 { get; set; }
        public decimal MajorTurnPointMoveRequired { get; set; }
        public decimal MinorTurnPointMoveRequired { get; set; }
        public decimal SupResTolerance { get; set; }
        public decimal TrendBreakTolerance { get; set; }
        public decimal MaxMovementAllowed { get; set; }
        public decimal MinDistanceToSupRes { get; set; }
        //public decimal MinDistanceToSupRes { get; set; }

    }

    public class TradePlanGeneric : TradePlanBase
    {
        List<InstrumentHistoryData> m_HistData = null;
        AnalyzeTurningPoints m_AnalyzeMajorTurningPoints = null;
        AnalyzeTurningPoints m_AnalyzeMinorTurningPoints = null;
        AnalyzeTurningPoints m_AnalyzeAdaptTurningPoints = null;
        AnalyzeSupportResistance m_AnalyzeMajorSupRes = null;
        List<SupResLine> m_MajorSupRes = new List<SupResLine>();
        List<ITradeRule> m_TradeRules = new List<ITradeRule>();

        protected TradePlanGenericData TradePlanGenData
        {
            get
            {
                return (TradePlanGenericData)m_TradePlanData;
            }
        }

        protected override void Load()
        {
            LoadTradePlanData<TradePlanGenericData>();

            m_HistData = DB.Rep.FindHistData(TradePlanGenData.Epic, TradePlanGenData.Resolution, m_TradeParam.CurrentDateTime.AddHours(TradePlanGenData.HistoryLengthRequired*-1), m_TradeParam.CurrentDateTime);

            m_AnalyzeMajorTurningPoints = new AnalyzeTurningPoints(m_HistData);
            m_AnalyzeMinorTurningPoints = new AnalyzeTurningPoints(m_HistData);
            m_AnalyzeAdaptTurningPoints = new AnalyzeTurningPoints(m_HistData);

            var minorTurnPoints = m_AnalyzeMinorTurningPoints.CalcNextTurnPoints(TradePlanGenData.MinorTurnPointMoveRequired);
            var majorTurnPoints = m_AnalyzeMajorTurningPoints.CalcNextTurnPoints(TradePlanGenData.MajorTurnPointMoveRequired);

            //if (minorTurnPoints.Count > 0)
            //    RebuildAdaptTurnPoints(m_TradeParam.CurrentDateTime);

            m_AnalyzeMajorSupRes = new AnalyzeSupportResistance(TradePlanGenData.SupResTolerance, TradePlanGenData.Epic);
            //m_AnalyzeMajorSupRes.AddTurningPoints(majorTurnPoints);
            //m_MajorSupRes = m_AnalyzeMajorSupRes.GetAllSupResLines();

            LoadTradeRules();
        }

      /*  protected void RebuildAdaptTurnPoints(DateTime curDT)
        {
            //get max and min values in last 2 hours
            decimal max = m_AnalyzeMinorTurningPoints[0].Price;
            decimal min = m_AnalyzeMinorTurningPoints[0].Price;
            for (int i = 1; i < m_AnalyzeMinorTurningPoints.Count; i++)
            {
                TimeSpan tDiff = curDT - m_AnalyzeMinorTurningPoints[i].TimeStamp;
                if (tDiff.TotalMinutes > 120)
                    break;

                if (m_AnalyzeMinorTurningPoints[i].Price > max)
                    max = m_AnalyzeMinorTurningPoints[i].Price;
                else if (m_AnalyzeMinorTurningPoints[i].Price < min)
                    min = m_AnalyzeMinorTurningPoints[i].Price;
            }

            if (max == min)
                return;

            m_AnalyzeAdaptTurningPoints.CalcNextTurnPoints((max - min) / 2);
        }*/

        Dictionary<DateTime, int> m_TradesDone = new Dictionary<DateTime, int>();
        protected override void EndOfInterval(InstrumentHistoryData bar)
        {
            m_HistData.Add(bar);

           
            var minorTurnPoints = m_AnalyzeMinorTurningPoints.CalcNextTurnPoints(TradePlanGenData.MinorTurnPointMoveRequired);
            var majorTurnPoints = m_AnalyzeMajorTurningPoints.CalcNextTurnPoints(TradePlanGenData.MajorTurnPointMoveRequired);

           // if (majorTurnPoints.Count > 0)
            {
                
            }

          /*  if (majorTurnPoints.Count > 0)
            {
                m_AnalyzeMajorSupRes.AddTurningPoints(majorTurnPoints);
                m_MajorSupRes = m_AnalyzeMajorSupRes.GetAllSupResLines();
            }*/

          //  if (minorTurnPoints.Count > 0)
          //      RebuildAdaptTurnPoints(bar.PriceDateTime);

            if (HasPositionsOpen())
            {
                if(minorTurnPoints.Count > 0 || majorTurnPoints.Count > 0)
                    AdjustStopLoss();
                return;
            }

         /*   if (!m_TradesDone.ContainsKey(m_AnalyzeMinorTurningPoints[0].TimeStamp) &&
                    m_AnalyzeMajorTurningPoints.newTurnPoint.Price < m_AnalyzeMajorTurningPoints[0].Price &&
                    m_AnalyzeMajorTurningPoints.newTurnPoint.Price > m_AnalyzeMajorTurningPoints[1].Price &&
                    m_AnalyzeMinorTurningPoints[0].Price > m_AnalyzeMinorTurningPoints[2].Price)
            {
                m_TradesDone[m_AnalyzeMinorTurningPoints[0].TimeStamp] = 1;
                CreatePosition(LibDef.BUY, (decimal)TradePlanGenData.PositionCount, bar.CloseAskPrice.Value - 0.0015M, bar.CloseAskPrice.Value + 0.0005M);
            }*/

            foreach (var tr in m_TradeRules)
            {
                tr.EndOfInterval(bar);
            }
            
        }

        protected override void OnStreamPrice(DataStreamUpdateEvent dataPrice)
        {
            ClosePositions(dataPrice);

            if (HasPositionsOpen())
                return;

            foreach (var tr in m_TradeRules)
            {
                tr.OnStreamPrice(dataPrice);

                if (tr.HasLongTradeSignal())
                {
                  //  if (!TestTradeExceptions(true))
                    {
                        CreatePosition(LibDef.BUY, (decimal)TradePlanGenData.PositionCount, tr.GetStopLevel(), tr.GetLimitLevel());
                        tr.Reset();
                    }
                }
                else if (tr.HasShortTradeSignal())
                {
                  //  if (!TestTradeExceptions(false))
                    {
                        CreatePosition(LibDef.SELL, (decimal)TradePlanGenData.PositionCount, tr.GetStopLevel(), tr.GetLimitLevel());
                        tr.Reset();
                    }
                }
            }

        }

        protected void LoadTradeRules()
        {
            TradeRuleTrend trTrend = new TradeRuleTrend(m_AnalyzeMinorTurningPoints, m_AnalyzeMajorTurningPoints, m_HistData, TradePlanGenData.TrendBreakTolerance);
            m_TradeRules.Add(trTrend);

           // TradeRuleSupRes trSR = new TradeRuleSupRes(m_AnalyzeMinorTurningPoints, m_AnalyzeMajorTurningPoints, m_AnalyzeAdaptTurningPoints);
          //  m_TradeRules.Add(trSR);
        }

        protected void AdjustStopLoss()
        {
            for (int i = m_ShortPositions.Count - 1; i >= 0; i--)
            {
                var pos = m_ShortPositions[i];
                if (pos.StopLevel < pos.Price)
                {
                    if (m_AnalyzeMajorTurningPoints.newTurnPoint.Price < m_AnalyzeMajorTurningPoints[0].Price &&
                        m_AnalyzeMajorTurningPoints[0].Price < pos.StopLevel)
                    {
                        pos.StopLevel = m_AnalyzeMajorTurningPoints[0].Price + 0.0001M;
                    }
                }
                else
                {
                    if (m_AnalyzeMinorTurningPoints.newTurnPoint.Price < m_AnalyzeMinorTurningPoints[0].Price &&
                        m_AnalyzeMinorTurningPoints[0].Price < pos.StopLevel)
                    {
                        pos.StopLevel = m_AnalyzeMinorTurningPoints[0].Price + 0.0001M;
                    }
                }

            }
        }

        protected void ClosePositions(DataStreamUpdateEvent dataPrice)
        {
            for (int i = m_LongPositions.Count - 1; i >= 0; i--)
            {
                var pos = m_LongPositions[i];
                if (dataPrice.PriceData.BidPrice > pos.LimitLevel || (dataPrice.PriceData.BidPrice < pos.StopLevel))
                {                    
                    ClosePosition(pos, pos.DealSize);
                }
              /*  else if (pos.DealSize == TradePlanGenData.PositionCount)
                {
                    if ((dataPrice.PriceData.BidPrice - pos.Price) > (pos.Price - pos.StopLevel))
                        ClosePosition(pos, TradePlanGenData.PositionCount / 2);
                }*/
            }

            for (int i = m_ShortPositions.Count - 1; i >= 0; i--)
            {
                var pos = m_ShortPositions[i];
                if (dataPrice.PriceData.AskPrice < pos.LimitLevel || (dataPrice.PriceData.AskPrice > pos.StopLevel))
                {
                    ClosePosition(pos, pos.DealSize);
                }
               /* else if (pos.DealSize == TradePlanGenData.PositionCount)
                {
                    if ((pos.Price - dataPrice.PriceData.AskPrice) > (pos.StopLevel - pos.Price))
                        ClosePosition(pos, TradePlanGenData.PositionCount / 2);
                }*/
            }
        }

        //return false if no exceptions
        protected bool TestTradeExceptions(bool bLongSignal)
        {
            //test distance from short MA
            if (TradePlanGenData.MAShort != 0 && TradePlanGenData.MAShortMaxDist != 0)
            {
                decimal maShort = Indicators.GetMA(m_HistData, TradePlanGenData.MAShort);
                if (bLongSignal)
                {
                    if (m_CurrentDataPrice.PriceData.AskPrice - maShort  > TradePlanGenData.MAShortMaxDist)
                        return true;
                }
                else
                {
                    if (maShort - m_CurrentDataPrice.PriceData.BidPrice  > TradePlanGenData.MAShortMaxDist)
                        return true;
                }
            }

            //test distance from long MA
            if (TradePlanGenData.MALong != 0 && TradePlanGenData.MALongMaxDist != 0)
            {
                decimal maLong = Indicators.GetMA(m_HistData, TradePlanGenData.MALong);
                if (bLongSignal)
                {
                    if (m_CurrentDataPrice.PriceData.AskPrice - maLong > TradePlanGenData.MALongMaxDist)
                        return true;
                }
                else
                {
                    if (maLong - m_CurrentDataPrice.PriceData.BidPrice > TradePlanGenData.MALongMaxDist)
                        return true;
                }
            }

            //check that we do not exeed maximum movement into one direction
            if (bLongSignal)
            {
                if (m_AnalyzeMajorTurningPoints.newTurnPoint.Price > m_AnalyzeMajorTurningPoints[0].Price &&
                    (m_CurrentDataPrice.PriceData.AskPrice - m_AnalyzeMajorTurningPoints[0].Price) > TradePlanGenData.MaxMovementAllowed)
                {
                    return true;
                }
            }
            else
            {
                if (m_AnalyzeMajorTurningPoints.newTurnPoint.Price < m_AnalyzeMajorTurningPoints[0].Price &&
                    (m_AnalyzeMajorTurningPoints[0].Price - m_CurrentDataPrice.PriceData.BidPrice) > TradePlanGenData.MaxMovementAllowed)
                {
                    return true;
                }
            }

            //check if we approach support resistance

          //  if(m_AnalyzeMajorSupRes.IsPriceCloseToSupResLevel(m_CurrentDataPrice.PriceData, TradePlanGenData.MinDistanceToSupRes, bLongSignal))
          //      return true;

            return false;
        }
    }
}
