using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGTradingLib.Types;
using IGTradingLib.DataModel;

namespace IGTradingLib.TradeAnalysis
{
    public class AnalyzeSupportResistance
    {
        List<SupResLine> m_SupResList = new List<SupResLine>();
        List<SupResLine> m_LongTermSupResList = new List<SupResLine>();
        List<SupResLine> m_AllLines = new List<SupResLine>();
        decimal m_Tolerance = 0;
        DateTime m_LastLongTermCalc = DateTime.Now.AddDays(-1);
        string m_Epic = "";

        public List<PricePoint> m_TurnPoints = new List<PricePoint>();

        public AnalyzeSupportResistance(decimal tolerance, string epic)
        {
            m_Tolerance = tolerance;
            m_Epic = epic;
        }

        public void AddTurningPoints(List<PricePoint> points)
        {
            m_TurnPoints.AddRange(points);
            int startIndex = 0;

            if(m_TurnPoints.Count == points.Count)
                startIndex = 1;

            for (int i = startIndex; i < points.Count; i++)
            {
                //if this price point fall within range of existing support resistance, then add it.
                if (AdddIfExistsInSupRes(points[i]))
                {
                    //AddHorisontalSupRes(points[i]);
                    continue;
                }

                for (int j = 0; j < m_TurnPoints.Count; j++)
                {
                    if (Math.Abs(points[i].Price - m_TurnPoints[j].Price) < m_Tolerance)
                    {
                        SupResLine line = new SupResLine();
                        AddHorisontalSupRes(line, points[i]);
                        m_SupResList.Add(line);
                    }
                }
            }
            
        }

        public List<SupResLine> GetAllSupResLines()
        {
            

            //add last 2 turnpoints to support resistance
            for (int i = m_TurnPoints.Count - 1; i >= 0; i--)
            {
                AddDataToHorisontalSupRes(m_AllLines, new PricePoint() { Price = m_TurnPoints[i].Price, TimeStamp = m_TurnPoints[i].TimeStamp });

                if (m_AllLines.Count == 2)
                    break;
            }

            /*if (DateTime.Now.Date != m_LastLongTermCalc.Date)
            {
                m_LongTermSupResList.Clear();
                InstrumentHistoryData prevDay = DB.Rep.FindHistData(m_Epic, "DAY", DateTime.Now.AddDays(-7), DateTime.Now.AddDays(-1), "PriceDateTime desc", 1).FirstOrDefault();
                if (prevDay != null)
                {
                    //add prev trading day high
                    AddDataToHorisontalSupRes(m_LongTermSupResList, new PricePoint() { Price = prevDay.GetMidHighPrice(), TimeStamp = prevDay.PriceDateTime});
                    //add prev trading day Low
                    AddDataToHorisontalSupRes(m_LongTermSupResList, new PricePoint() { Price = prevDay.GetMidLowPrice(), TimeStamp = prevDay.PriceDateTime });
                }

                InstrumentHistoryData SixMonthHigh = DB.Rep.FindHistData(m_Epic, "DAY", DateTime.Now.AddMonths(-6), DateTime.Today, "HighAskPrice desc", 1).FirstOrDefault();

                if(SixMonthHigh != null)
                    AddDataToHorisontalSupRes(m_LongTermSupResList, new PricePoint() { Price = SixMonthHigh.GetMidHighPrice(), TimeStamp = SixMonthHigh.PriceDateTime });

                InstrumentHistoryData SixMonthLow = DB.Rep.FindHistData(m_Epic, "DAY", DateTime.Now.AddMonths(-6), DateTime.Today, "HighAskPrice asc", 1).FirstOrDefault();

                if (SixMonthLow != null)
                    AddDataToHorisontalSupRes(m_LongTermSupResList, new PricePoint() { Price = SixMonthLow.GetMidHighPrice(), TimeStamp = SixMonthLow.PriceDateTime });

                
                m_LastLongTermCalc = DateTime.Now;
            }*/

            m_AllLines.AddRange(m_SupResList);
            //allLines.AddRange(m_LongTermSupResList);
            
            return m_AllLines;
        }

        public bool IsPriceCloseToSupResLevel(DataStreamPrice price, decimal minDistanceToSupRes, bool bLong)
        {
            foreach (var level in m_AllLines)
            {
                decimal dist = 0;

                if (!level.HorisontalPriceLevel.HasValue)
                    continue;

                if (bLong)
                    dist = level.HorisontalPriceLevel.Value - price.AskPrice;
                else
                    dist = price.BidPrice - level.HorisontalPriceLevel.Value;

                if (dist < minDistanceToSupRes && dist > (m_Tolerance * -1))
                    return true;
            }

            return false;
        }

        private void AddHorisontalSupRes(SupResLine line, PricePoint pricePoint)
        {
           // SupResLine line = new SupResLine();
            line.PointList.Add(pricePoint);
            decimal sumPrice = 0;

            line.PointList.ForEach(p => sumPrice += p.Price);
            line.HorisontalPriceLevel = sumPrice / line.PointList.Count;
        }

        private void AddDataToHorisontalSupRes(List<SupResLine> allLines, PricePoint pricePoint)
        {
            SupResLine line = null;

            foreach (var item in allLines)
            {
                //test horisontal level support resistance
                if (item.HorisontalPriceLevel.HasValue)
                {
                    if (Math.Abs(item.HorisontalPriceLevel.Value - pricePoint.Price) < (m_Tolerance / 2))
                    {
                        line = item;
                        break;
                    }
                }
            }

            if (line == null)
            {
                line = new SupResLine();
                allLines.Add(line);
            }

            line.PointList.Add(pricePoint);
            decimal sumPrice = 0;

            line.PointList.ForEach(p => sumPrice += p.Price);
            line.HorisontalPriceLevel = sumPrice / line.PointList.Count;
        }

        private bool AdddIfExistsInSupRes(PricePoint pricePoint)
        {
            foreach (var item in m_SupResList)
            {
                //test horisontal level support resistance
                if (item.HorisontalPriceLevel.HasValue)
                {
                    if (Math.Abs(item.HorisontalPriceLevel.Value - pricePoint.Price) < (m_Tolerance / 2))
                    {
                        AddHorisontalSupRes(item, pricePoint);
                        return true;
                    }
                }
            }

            return false;
        }

       /* private DateTime GetPreviousWorkingDate()
        {
            DateTime prevDay = DateTime.Today.AddDays(-1);
            if(prevDay.DayOfWeek == 
            return prevDay;

        }*/
    }
}
