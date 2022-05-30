using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Medley.Common.Defs.Interfaces;

namespace Medley.Common.Defs.Types
{
    public class SqlSearchParam : ISearchParam
    {
        public SqlSearchParam() { }

        public SqlSearchParam(string sqlString)
        {
            SqlString = sqlString;
        }

        public string SqlString { get; set; }
    }
}
