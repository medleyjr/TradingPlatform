using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGTradingLib.TradePlan;
using IGTradingLib.DataModel;

namespace IGTradingLib.Types
{
    public class TradeParam
    {
       // public string Epic { get; set; }
       // public string Resolution { get; set; }
        public DateTime CurrentDateTime { get; set; }
        public DateTime PreLoadDataDateTime { get; set; }
       // public decimal MajorTurnPointMove { get; set; }
       // public decimal MinorTurnPointMove { get; set; }

        public TradePlanDetails TradePlan { get; set; }
    }
}
