using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGTradingLib.Types
{
    public class IGStatusData
    {
        public bool LoggedOn { get; set; }
        public string AccountName { get; set; }
        public decimal FundBalance { get; set; }        
        public decimal Margin { get; set; }        
        public decimal ProfitLoss { get; set; }        
        public decimal FundsAvailable { get; set; }

    }
}
