using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGTradingLib.Types
{
    

    public class TradePosition
    {
        public static string POS_REQ_TYPE_OPEN  = "OPEN";
        public static string POS_REQ_TYPE_CLOSE = "CLOSE";

        public static string POS_REQ_DIR_BUY    = "BUY";
        public static string POS_REQ_DIR_SELL   = "SELL";

        public string DealID               { get; set; }
        public DateTime PositionRequestDt   { get; set; }
        public string PositionReqType       { get; set; }
        public string PositionDirection     { get; set; }
        public decimal? StopLevel           { get; set; }
        public decimal? LimitLevel          { get; set; }
        public decimal Price                { get; set; }
        public decimal DealSize { get; set; }
        public decimal TradeOpenSize { get; set; }


        public decimal FailureLevel { get; set; }

    }
}
