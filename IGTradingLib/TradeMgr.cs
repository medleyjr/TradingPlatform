using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGTradingLib.Types;
using IGTradingLib.DataModel;
using IGTradingLib.TradeAnalysis;
using IGTradingLib.TradePlan;

namespace IGTradingLib
{
    public class TradeMgr
    {
        ITradingHost m_TradingFeedback = null;
        ITradingSystem m_TradingSystem = null;
        List<InstrumentHistoryData> m_HistData = null;
        List<TradePlanBase> m_TradePlans = new List<TradePlanBase>();
       /* TradeParam m_Param = null;
        AnalyzeTurningPoints m_AnalyzeMajorTurningPoints = null;
        AnalyzeTurningPoints m_AnalyzeMinorTurningPoints = null;*/


        public void Init(ITradingHost cb, ITradingSystem tradingSystem)
        {
            m_TradingFeedback = cb;
            m_TradingSystem = tradingSystem;
        }

        public TradePlanBase AddTradePlan(TradeParam param)
        {
            TradePlanBase tradePlan = TradePlanMgr.CreateTradePlan(param.TradePlan.TradePlanType);
            tradePlan.Init(param, m_TradingFeedback, m_TradingSystem);
            m_TradePlans.Add(tradePlan);
            return tradePlan;
        }

    /*    public void StartTrade(TradeParam param, ITradingHost cb, ITradingSystem tradingSystem)
        {
            m_TradingFeedback = cb;
            m_TradingSystem = tradingSystem;
            m_Param = param;
            m_HistData = DB.Rep.FindHistData(param.Epic, param.Resolution, param.PreLoadDataDateTime, param.CurrentDateTime);
            m_AnalyzeMajorTurningPoints = new AnalyzeTurningPoints(m_HistData);
            m_AnalyzeMinorTurningPoints = new AnalyzeTurningPoints(m_HistData);
            
            DoAnalysis();
        }

        public void AddStreamPrice(DataStreamUpdateEvent dataPrice)
        {

        }

        public void AddIntervalPrice(InstrumentHistoryData data)
        {
            if (m_HistData == null)
                return;

            m_HistData.Add(data);
            DoAnalysis();
        }

        protected void DoAnalysis()
        {
            if (m_HistData == null)
                return;

            List<PricePoint> majorTurnPoints = m_AnalyzeMajorTurningPoints.CalcNextTurnPoints();
            majorTurnPoints.ForEach((p) => m_TradingFeedback.AddTurningPoint(p));           

        }*/

    }
}
