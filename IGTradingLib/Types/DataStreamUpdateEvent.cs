using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGTradingLib.DataModel;

namespace IGTradingLib.Types
{
    public class DataStreamUpdateEvent
    {
        public InstrumentDataID DataID { get; set; }
        public DataStreamPrice PriceData { get; set; }        
    }
}
