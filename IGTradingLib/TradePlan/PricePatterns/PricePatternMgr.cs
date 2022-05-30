using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGTradingLib.TradePlan.PricePatterns
{
    public static class PricePatternMgr
    {
        public static List<IPricePattern> GetNewList()
        {   
            List<IPricePattern> list = new List<IPricePattern>();

            list.Add(new PatternFlag());
            list.Add(new PatternTriangle());

            return list;
        }
    }
}
