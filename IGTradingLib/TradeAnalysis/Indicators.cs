using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGTradingLib.DataModel;
using IGTradingLib.Types;

namespace IGTradingLib.TradeAnalysis
{
    public class Indicators
    {
        public static decimal GetMA(List<InstrumentHistoryData> histData, int count)
        {
            decimal ma = 0;

            if (histData.Count < count)
                return -1;

            for (int i = histData.Count - count; i < histData.Count; i++)
                ma += histData[i].GetMidClosePrice();

            ma = ma / count;

            return ma;
        }

        public static List<PricePoint> GetMAList(List<InstrumentHistoryData> histData, int count)
        {
            List<PricePoint> maList = new List<PricePoint>();

            if (histData.Count < count)
                return maList;

            for (int i = count - 1; i < histData.Count; i++)
            {
                List<InstrumentHistoryData> tmpDataList = new List<InstrumentHistoryData>();
                
                for (int j = i - count + 1; j <= i; j++)
                {
                    tmpDataList.Add(histData[j]);
                }

                PricePoint p = new PricePoint();
                p.TimeStamp = histData[i].PriceDateTime;
                p.Price = GetMA(tmpDataList, count);

                maList.Add(p);
            }
                       

            return maList;
        }

        public static decimal GetAverageBarHeight(List<InstrumentHistoryData> histData, int count)
        {
            decimal ma = 0;

            if (histData.Count < count)
                return -1;

            for (int i = histData.Count - count; i < histData.Count; i++)
                ma += Math.Abs(histData[i].GetMidClosePrice() - histData[i].GetMidOpenPrice());

            ma = ma / count;

            return ma;
        }

        public static bool TestPriceReversal(List<InstrumentHistoryData> histData, bool bFromUpToDown)
        {
            decimal avgBar = GetAverageBarHeight(histData, 10);

            decimal reverseMove = 0;
            decimal forwardMove = 0;

            for (int i = histData.Count - 1; i >= 0; i--)
            {
                var item = histData[i];
                decimal curMove = Math.Abs(item.GetMidClosePrice() - item.GetMidOpenPrice());
                if (bFromUpToDown && item.GetMidClosePrice() <= item.GetMidOpenPrice())
                    reverseMove += curMove;
                else if (!bFromUpToDown && item.GetMidOpenPrice() <= item.GetMidClosePrice())
                    reverseMove += curMove;
                else
                {
                    if (curMove > avgBar)
                    {
                        if (reverseMove > curMove / 2)
                            return true;
                        else
                            return false;
                    }
                    else if (curMove < avgBar / 3 && forwardMove < avgBar)
                    {
                        forwardMove += curMove;
                    }
                    else
                    {
                        forwardMove += curMove;
                        if (reverseMove > forwardMove)
                            return true;
                        else
                            return false;
                    }
                }

            }

            return false;
        }
    }
}
