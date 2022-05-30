using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Medley.Common.ADODatabase
{
    public class ADODatabaseSetting
    {
        public bool IsValidSetting()
        {
            if (string.IsNullOrEmpty(DBConnectionString) || string.IsNullOrEmpty(DBProviderName))
                return false;
            else
                return true;
        }

        [XmlAttributeAttribute()]
        public string DBProviderName { get; set; }
        [XmlAttributeAttribute()]
        public string DBConnectionString { get; set; }        
    }
}
