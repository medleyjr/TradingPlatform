using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGTradingLib.Types
{
    public class PricePoint
    {
        public decimal Price { get; set; }
        public decimal Movement { get; set; }
        public DateTime TimeStamp { get; set; }

        public decimal AddPrice { get; set; }
        public DateTime AddTimeStamp { get; set; }

        public PricePoint CopyObj()
        {
            PricePoint p = new PricePoint();
            p.Price = Price;
            p.Movement = Movement;
            p.TimeStamp = TimeStamp;
            p.AddPrice = AddPrice;
            p.AddTimeStamp = AddTimeStamp;

            return p;
        }

        public decimal MovementAbs 
        {
            get
            {
                return Math.Abs(Movement);
            }
        }

        public static int CompareByPrice(PricePoint l, PricePoint r)
        {
            return decimal.Compare(l.Price, r.Price);
        }
    }
}
