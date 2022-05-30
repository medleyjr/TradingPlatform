using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGTradingLib.Types;
using IGTradingLib.DataModel;
using IGTradingLib.TradeAnalysis;

namespace IGTradingLib.TradePlan.PricePatterns
{
    /// <summary>
    /// Require the following for Bull Flag. Bear Flag is reverse:
    /// 1. Major up trend
    /// 2. Three lower highs
    /// 3. Three lower lows
    /// 4. Break out from highs trend
    /// </summary>
    public class PatternFlag : IPricePattern
    {
        enum EFormationStage
        {
            none,
            major_trend,
            flag_started,
            break_out
        }

        EFormationStage m_FormationStage = EFormationStage.none;
        List<PricePoint> m_PeakList = new List<PricePoint>();
        List<PricePoint> m_TroughList = new List<PricePoint>();
        int m_TrendDir = 0;
        decimal m_AvgLegDist = 0;

        protected decimal m_MaxFirstPullbackPerc = 0.3M;
        protected decimal m_LegDistTollerancePerc = 0.2M;

        public override decimal GetStopLevel()
        {
            throw new NotImplementedException();
        }

        public override decimal GetLimitLevel()
        {
            throw new NotImplementedException();
        }

      /*  public override void Init(AnalyzePriceAction analyzePriceAction)
        {
            base.Init(analyzePriceAction);
        }*/

        public override void EndOfInterval(InstrumentHistoryData bar)
        {
            
        }

        protected override void NewTurnPointAdded(PricePoint pricePoint)
        {
            if (m_FormationStage == EFormationStage.none && m_AnalyzePriceAction.m_TurnPointlist.Count >= 3)
            {
                if (m_AnalyzePriceAction.LastTurnPoint.MovementAbs < m_AnalyzePriceAction.LastTurnPoint2.MovementAbs * m_MaxFirstPullbackPerc)
                {
                    m_FormationStage = EFormationStage.major_trend;                    
                    m_PeakList.Add(m_AnalyzePriceAction.LastTurnPoint2);                   
                    m_TroughList.Add(m_AnalyzePriceAction.LastTurnPoint);
                    m_AvgLegDist = m_AnalyzePriceAction.LastTurnPoint.MovementAbs;

                    if (m_AnalyzePriceAction.LastTurnPoint2.Movement > 0)
                        m_TrendDir = 1;
                    else
                        m_TrendDir = -1;
                }
            }
            else if (m_FormationStage == EFormationStage.major_trend)
            {
                if ((m_AnalyzePriceAction.LastTurnPoint.Price - m_PeakList[0].Price) * m_TrendDir > 0)
                {
                    Reset();
                }
                /*else if()
                {

                }*/

            }
            
        }

        protected void Reset()
        {
            m_FormationStage = EFormationStage.none;
            m_PeakList.Clear();
            m_TroughList.Clear();
            m_TrendDir = 0;
            m_AvgLegDist = 0;
        }

       
    }
}
