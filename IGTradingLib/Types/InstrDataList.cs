using IGTradingLib.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGTradingLib.Types
{
    public class InstrDataList : List<InstrumentHistoryData>
    {
        protected decimal m_AvgHeight = 0;
        protected decimal m_BigBarFactor = 3;

        //if the closing price current bar is less than opening price of previous bar for uptrend or half of a big bar
        public bool IsReverseMove(int barIndex, int trendDir, InstrumentHistoryData dataBar = null)
        {
            InstrumentHistoryData dataBarCmp = this[barIndex - 1];
            if (dataBar != null)
                dataBarCmp = dataBar;

            decimal cmpPrice;
            if (dataBarCmp.GetBarHeight() > BigBarHieght)
                cmpPrice = dataBarCmp.GetMidMiddlePrice();
            else
                cmpPrice = dataBarCmp.GetMidOpenPrice();

            if(trendDir > 0)
                return (this[barIndex].GetMidClosePrice() < cmpPrice);
            else
                return (this[barIndex].GetMidClosePrice() > cmpPrice);

        }

        public bool IsContinueMomentum(int barIndex, int trendDir, InstrumentHistoryData dataBar = null)
        {
            InstrumentHistoryData dataBarCmp = this[barIndex - 1];
            if (dataBar != null)
                dataBarCmp = dataBar;

            if (trendDir > 0)
                return (this[barIndex].GetMidClosePrice() > dataBarCmp.GetMidClosePrice());
            else
                return (this[barIndex].GetMidClosePrice() < dataBarCmp.GetMidClosePrice());

        }

        public void CalcBarStats()
        {
            if (this.Count == 0)
                return;

            decimal totalBarHeight = 0;
            foreach (var bar in this)
            {
                totalBarHeight += bar.GetBarHeight();
            }

            m_AvgHeight = totalBarHeight / this.Count;
        }

        public decimal AvgHeight
        {
            get
            {
                if (m_AvgHeight == 0M)
                    CalcBarStats();

                return m_AvgHeight;
            }
        }

        public decimal BigBarHieght
        {
            get
            {
                return AvgHeight * m_BigBarFactor;
            }
        }

        public InstrumentHistoryData LastBar
        {
            get
            {
                if (this.Count == 0)
                    return null;

                return this[this.Count - 1];
            }
        }
    }
}
