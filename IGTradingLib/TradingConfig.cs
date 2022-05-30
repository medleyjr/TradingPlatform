using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGTradingLib
{
    public class TradingConfig
    {
        //private static TradingConfig m_TradingConfig = null;

        public static TradingConfig Instance { get; set; }

        public TradingConfig()
        {
            TimeZoneDiff = 2; //2 hours
        }

        public int TimeZoneDiff { get; set; }
        
    }
}
