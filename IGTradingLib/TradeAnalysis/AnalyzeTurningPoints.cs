using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGTradingLib.Types;
using IGTradingLib.DataModel;

namespace IGTradingLib.TradeAnalysis
{
    public enum ETrendDirection
    {
        not_set,
        up,
        down,
        flag
    }

    public class AnalyzeTurningPoints
    {
        List<InstrumentHistoryData> m_HistData = null;
        int m_LastIndexChecked = 1;
        PricePoint lastTurnPoint = new PricePoint();
           
        decimal m_UpMove = 0;
        decimal m_DownMove = 0;

        List<PricePoint> m_TurnPointlist = new List<PricePoint>();
        public PricePoint newTurnPoint = new PricePoint();     

        public AnalyzeTurningPoints(List<InstrumentHistoryData> histData)
        {
            m_HistData = histData;
            lastTurnPoint.Price = m_HistData[0].GetMidClosePrice();
            lastTurnPoint.TimeStamp = m_HistData[0].PriceDateTime;
        }
        
        //index is backwords
        public PricePoint this[int index]
        {
            get
            {
                if (this.m_TurnPointlist.Count - index <= 0)
                    return null;

                return m_TurnPointlist[m_TurnPointlist.Count - index - 1];
            }
        }

        public int Count
        {
            get
            {
                return m_TurnPointlist.Count;
            }
        }

        public void Reset()
        {
            m_LastIndexChecked = 1;
            lastTurnPoint = new PricePoint();
            lastTurnPoint.Price = m_HistData[0].GetMidClosePrice();
            lastTurnPoint.TimeStamp = m_HistData[0].PriceDateTime;
            m_UpMove = 0;
            m_DownMove = 0;
            m_TurnPointlist.Clear();
            newTurnPoint = new PricePoint();
        }

        //return newly added turn points
        public List<PricePoint> CalcNextTurnPoints(decimal moveRequired = 300)
        {
            List<PricePoint> list = new List<PricePoint>();

            if (m_HistData == null || m_HistData.Count == 0)
                return list;


            for (int i = m_LastIndexChecked; i < m_HistData.Count; i++)
            {
                try
                {
                    InstrumentHistoryData dataItem = m_HistData[i];

                    if (dataItem.GetMidHighPrice() == 0 || dataItem.GetMidLowPrice() == 0)
                        continue;

                    bool evalHigh = true;
                    if (dataItem.GetMidClosePrice() > dataItem.GetMidOpenPrice())
                        evalHigh = false;

                    int count = 0;
                    while (count < 2)
                    {
                        if (evalHigh)
                        {
                            decimal tmpUp = dataItem.GetMidHighPrice() - ((m_DownMove > 0) ? newTurnPoint.Price : lastTurnPoint.Price);
                            if (tmpUp > m_UpMove && tmpUp > moveRequired)
                            {
                                m_UpMove = tmpUp;

                                //add the down move turn point to list
                                if (m_DownMove > 0)
                                {
                                    list.Add(newTurnPoint);
                                    lastTurnPoint = newTurnPoint;
                                    newTurnPoint = new PricePoint();
                                    m_DownMove = 0;
                                }

                                newTurnPoint.Movement = m_UpMove;
                                newTurnPoint.Price = dataItem.GetMidHighPrice();
                                newTurnPoint.TimeStamp = dataItem.PriceDateTime;
                            }
                            if (count == 1)
                                break;
                            else
                            {
                                evalHigh = false;
                                count++;
                            }
                        }

                        if (!evalHigh)
                        {
                            decimal tmpDown = ((m_UpMove > 0) ? newTurnPoint.Price : lastTurnPoint.Price) - dataItem.GetMidLowPrice();
                            if (tmpDown > m_DownMove && tmpDown > moveRequired)
                            {
                                m_DownMove = tmpDown;

                                if (m_UpMove > 0)
                                {
                                    list.Add(newTurnPoint);
                                    lastTurnPoint = newTurnPoint;
                                    newTurnPoint = new PricePoint();
                                    m_UpMove = 0;
                                }

                                newTurnPoint.Movement = m_DownMove;
                                newTurnPoint.Price = dataItem.GetMidLowPrice();
                                newTurnPoint.TimeStamp = dataItem.PriceDateTime;
                            }

                            if (count == 1)
                                break;
                            else
                            {
                                evalHigh = true;
                                count++;
                            }
                        }

                        
                    }

                }
                catch (Exception ex)
                {
                    LibDef.m_StatusEvent.RaiseError(ex.Message);
                }

            }

            m_LastIndexChecked = m_HistData.Count;

            
            //list.Sort(PricePoint.CompareByPrice);
            m_TurnPointlist.AddRange(list);
            
            return list;
        }

        public ETrendDirection TrendDirection()
        {
            if(m_TurnPointlist.Count < 3)
                return ETrendDirection.not_set;

            if (m_TurnPointlist[0].Price > m_TurnPointlist[2].Price && newTurnPoint.Price > m_TurnPointlist[1].Price)
                return ETrendDirection.up;
            else if (m_TurnPointlist[0].Price < m_TurnPointlist[2].Price && newTurnPoint.Price < m_TurnPointlist[1].Price)
                return ETrendDirection.down;
            else
                return ETrendDirection.flag;
        }
    }
}
