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
    
    public class PatternTriangle : IPricePattern
    {
        public override decimal GetStopLevel()
        {
            throw new NotImplementedException();
        }

        public override decimal GetLimitLevel()
        {
            throw new NotImplementedException();
        }

       /* public override void Init(AnalyzePriceAction analyzePriceAction)
        {
            throw new NotImplementedException();
        }*/

        public override void EndOfInterval(InstrumentHistoryData bar)
        {
            throw new NotImplementedException();
        }

        protected override void NewTurnPointAdded(PricePoint pricePoint)
        {

        }
    }
}
