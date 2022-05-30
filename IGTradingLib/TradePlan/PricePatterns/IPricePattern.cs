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
    public abstract class IPricePattern
    {
        protected AnalyzePriceAction m_AnalyzePriceAction = null;

        public abstract decimal GetStopLevel();
        public abstract decimal GetLimitLevel();        
        public abstract void EndOfInterval(InstrumentHistoryData bar);

        protected abstract void NewTurnPointAdded(PricePoint pricePoint);
        

        public virtual void Init(AnalyzePriceAction analyzePriceAction)
        {
            m_AnalyzePriceAction = analyzePriceAction;
            m_AnalyzePriceAction.OnTurnPointAdded += m_AnalyzePriceAction_OnTurnPointAdded;
        }

        void m_AnalyzePriceAction_OnTurnPointAdded(PricePoint pricePoint)
        {
            NewTurnPointAdded(pricePoint);
        }
    }
}
