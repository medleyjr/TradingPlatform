using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGTradingLib.Types;

namespace IGTradingLib
{
    public interface ITradingHost
    {
        void AddTurningPoint(PricePoint turnPoint);
        void RegisterIntervalEventHandler(string resolution, Action<DateTime> fn);
    }
}
