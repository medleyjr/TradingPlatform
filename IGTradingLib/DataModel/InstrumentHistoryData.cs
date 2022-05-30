using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGTradingLib.DataModel
{
    public class InstrumentHistoryData
    {
        public long ID { get; set; }
        public long DataID { get; set; }
        public string Resolution { get; set; }
        public DateTime PriceDateTime { get; set; }
        public decimal? OpenBidPrice { get; set; }
        public decimal? OpenAskPrice { get; set; }
        public decimal? OpenLastTraded { get; set; }

        public decimal? CloseBidPrice { get; set; }
        public decimal? CloseAskPrice { get; set; }
        public decimal? CloseLastTraded { get; set; }

        public decimal? HighBidPrice { get; set; }
        public decimal? HighAskPrice { get; set; }
        public decimal? HighLastTraded { get; set; }

        public decimal? LowBidPrice { get; set; }
        public decimal? LowAskPrice { get; set; }
        public decimal? LowLastTraded { get; set; }

        public decimal? LastTradedVolume { get; set; }

        public DateTime DateModified { get; set; }


        public decimal GetMidClosePrice()
        {
            return GetAvg(CloseBidPrice, CloseAskPrice); 
        }

        public decimal GetMidOpenPrice()
        {
            return GetAvg(OpenBidPrice, OpenAskPrice); 
        }

        public decimal GetMidHighPrice()
        {
            return GetAvg(HighBidPrice, HighAskPrice); 
        }

        public decimal GetMidLowPrice()
        {
            return GetAvg(LowBidPrice, LowAskPrice); 
        }

        //return the price that is in middle of the open and close price
        public decimal GetMidMiddlePrice()
        {
            if (IsUp())
                return GetMidOpenPrice() + (GetBarHeight() / 2);
            else
                return GetMidOpenPrice() - (GetBarHeight() / 2);
        }

        public decimal GetBarHeight()
        {
            return Math.Abs(GetMidClosePrice() - GetMidOpenPrice()); 
        }

        public bool IsUp()
        {
            return (GetMidClosePrice() - GetMidOpenPrice()) > 0;
        }

        public decimal GetMidPeak(int dir)
        {
            if (dir > 0)
                return GetMidHighPrice();
            else
                return GetMidLowPrice();
        }

        protected decimal GetAvg(decimal? val1, decimal? val2)
        {
            if (val1.HasValue && val2.HasValue)
                return (val1.Value + val2.Value) / 2;
            else if (val1.HasValue)
                return val1.Value;
            else if (val2.HasValue)
                return val2.Value;
            else
                return 0;

        }

        

       


    }
}
