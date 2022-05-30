using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medley.Common.Defs.Interfaces
{
    public interface IMedleyEncoder
    {
        string EncodeData(object obj);
        T EncodeData<T>(string data);
    }
}
