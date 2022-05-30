using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGTradingLib;
using IGTradingLib.DataModel;

namespace IGTrading
{
    public static class Def
    {
        public static ITrading m_trading = null;
        public static InstrumentDetails m_SelectedInstr = new InstrumentDetails();

        public static IGApiConfig ApiConfig { get; set; }

        public static string[] ResolutionList = { "MINUTE", "MINUTE_2", "MINUTE_3", "MINUTE_5", "MINUTE_10", "MINUTE_15", "MINUTE_30", "HOUR", "HOUR_2", "HOUR_3", "HOUR_4", "DAY", "WEEK", "MONTH" };
    }
}
