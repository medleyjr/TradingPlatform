using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGTradingLib.DataModel
{
    public class TradePlanDetails
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public int TradePlanType { get; set; }
        public string Data { get; set; }
    }
}
