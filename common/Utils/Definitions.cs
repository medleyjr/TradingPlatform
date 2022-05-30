using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medley.Common.Utils
{
     [Serializable]
    public delegate void GenericEventHandler<T>(object sender, T e);
}
