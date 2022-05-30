using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGTradingLib.Types;
using IGTradingLib.DataModel;

namespace IGTradingLib.TradePlan.TradeRules
{
    public interface ITradeRule
    {
        bool HasLongTradeSignal();
        bool HasShortTradeSignal();
        decimal GetStopLevel();
        decimal GetLimitLevel();
        void OnStreamPrice(DataStreamUpdateEvent dataPrice);
        void EndOfInterval(InstrumentHistoryData bar);
        void Reset();
    }
}
