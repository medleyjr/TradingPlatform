using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGTradingLib.DataModel
{
    public class DataStreamPrice
    {
        public DateTime PriceDatetime { get; set; }
        public decimal BidPrice { get; set; }
        public decimal AskPrice { get; set; }

        public decimal GetMidPrice()
        {
            return (BidPrice + AskPrice) / 2;
        }

    }
}
