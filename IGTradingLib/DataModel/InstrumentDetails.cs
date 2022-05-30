using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGTradingLib.DataModel
{
    public class InstrumentDetails
    {
        public string Epic { get; set; }
        public string Name { get; set; }
        public int DataID { get; set; }
        public string InstrumentType { get; set; }
        public bool EnableLiveStream { get; set; }
        public string CurrencyCode { get; set; }

    }
}
