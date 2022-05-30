using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGTrading
{
    public class IGApiConfig
    {
        public string LiveAPIKey { get; set; }
        public string LiveUsername{ get; set; }
        public string LivePwd{ get; set; }

        public string DemoAPIKey { get; set; }
        public string DemoUsername { get; set; }
        public string DemoPwd { get; set; }
    }
}
