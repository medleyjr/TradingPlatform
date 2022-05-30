using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGTradingLib.Types
{
    public class SupResLine
    {
        public List<PricePoint> PointList = new List<PricePoint>();
        public decimal? HorisontalPriceLevel;
        public PricePoint Point1 = null;
        public PricePoint Point2 = null;
    }
}
