using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGTradingLib.DataModel;
using Medley.Common.Utils;

namespace IGTradingLib
{
    public class TradingDBManage
    {
        public static void ImportToResolution(string epic, string sourceResolution, string resolution, DateTime dtStart, DateTime dtEnd)
        {
            List<InstrumentHistoryData> histData = DB.Rep.FindHistData(epic, sourceResolution, dtStart, dtEnd);

            if (histData == null || histData.Count < 2)
                return;

            List<InstrumentHistoryData> newDataList = new List<InstrumentHistoryData>();

            DateTime lastPriceDt = histData[0].PriceDateTime;
            InstrumentHistoryData newDataItem = null;
            int newBarItemCount = 0;

            for (int i = 0; i < histData.Count; i++)
            {
                var dataItem = histData[i];
                bool newBar = false;

                //check if new bar
                if (IsNewBar(resolution, lastPriceDt, dataItem.PriceDateTime))
                {                    
                    newBar = true;
                    lastPriceDt = dataItem.PriceDateTime;                    
                }

                //update values
                if (!newBar && i > 0)
                {
                    if (dataItem.HighAskPrice > newDataItem.HighAskPrice)
                        newDataItem.HighAskPrice = dataItem.HighAskPrice;

                    if (dataItem.HighBidPrice > newDataItem.HighBidPrice)
                        newDataItem.HighBidPrice = dataItem.HighBidPrice;

                    if (dataItem.LowAskPrice < newDataItem.LowAskPrice)
                        newDataItem.LowAskPrice = dataItem.LowAskPrice;

                    if (dataItem.LowBidPrice < newDataItem.LowBidPrice)
                        newDataItem.LowBidPrice = dataItem.LowBidPrice;

                    newDataItem.CloseBidPrice = dataItem.CloseBidPrice;
                    newDataItem.CloseAskPrice = dataItem.CloseAskPrice;
                    newDataItem.LastTradedVolume += dataItem.LastTradedVolume;

                    newBarItemCount++;

                }

                //save previous bar
                if (newBar || i == histData.Count - 1)
                {
                    newDataItem.DateModified = DateTime.Now;

                    //ignore midnight bar for now
                    if (!(resolution == "DAY" && newBarItemCount == 1))
                        newDataList.Add(newDataItem);
                }

                //check if new bar must be created
                if (newBar || i == 0)
                {
                    newDataItem = (InstrumentHistoryData)dataItem.CloneObject();
                    newDataItem.ID = 0;
                    newDataItem.Resolution = resolution;
                    newBarItemCount = 1;

                    if (resolution == "DAY" || resolution == "WEEK" || resolution == "MONTH")
                        newDataItem.PriceDateTime = dataItem.PriceDateTime.Date;
                }

            }

            if (newDataList.Count > 0)
                DB.Rep.AddEntityList(newDataList);

        }

        public static void ImportDataStreamToResolution(string epic, string resolution, DateTime dtStart, DateTime dtEnd)
        {
            List<InstrumentHistoryData> newDataList = ConvertDataStreamToResolution(epic, resolution, dtStart, dtEnd);
                
            if (newDataList.Count > 0)
                DB.Rep.AddEntityList(newDataList);
        }

        public static List<InstrumentHistoryData> ConvertDataStreamToResolution(string epic, string resolution, DateTime dtStart, DateTime dtEnd)
        {
            var dataStreamObj = TradingDataStreamMgr.GetObj(epic);
            long dataID = DB.Rep.GetIntrumentDataID(epic).ID;
            var histData = dataStreamObj.LoadHistory(dtStart, dtEnd);            
            
            List<InstrumentHistoryData> newDataList = new List<InstrumentHistoryData>();

            if (histData == null || histData.Count < 2)
                return newDataList;

            DateTime lastPriceDt = histData[0].PriceDatetime;
            InstrumentHistoryData newDataItem = null;
            int newBarItemCount = 0;

            for (int i = 0; i < histData.Count; i++)
            {
                var dataItem = histData[i];
                bool newBar = false;

                //check if new bar
                if (IsNewBar(resolution, lastPriceDt, dataItem.PriceDatetime))
                {
                    newBar = true;
                    lastPriceDt = dataItem.PriceDatetime;
                }                

                //update values
                if (!newBar && i > 0)
                {
                    if (dataItem.AskPrice > newDataItem.HighAskPrice)
                        newDataItem.HighAskPrice = dataItem.AskPrice;

                    if (dataItem.BidPrice > newDataItem.HighBidPrice)
                        newDataItem.HighBidPrice = dataItem.BidPrice;

                    if (dataItem.AskPrice < newDataItem.LowAskPrice)
                        newDataItem.LowAskPrice = dataItem.AskPrice;

                    if (dataItem.BidPrice < newDataItem.LowBidPrice)
                        newDataItem.LowBidPrice = dataItem.BidPrice;

                    newDataItem.CloseBidPrice = dataItem.BidPrice;
                    newDataItem.CloseAskPrice = dataItem.AskPrice;
                    //newDataItem.LastTradedVolume += dataItem.LastTradedVolume;

                    newBarItemCount++;

                }

                //save previous bar
                if (newBar || i == histData.Count - 1)
                {
                    newDataItem.DateModified = DateTime.Now;

                    //ignore midnight bar for now
                    if (!(resolution == "DAY" && newBarItemCount == 1))
                        newDataList.Add(newDataItem);
                }

                //check if new bar must be created
                if (newBar || i == 0)
                {
                    newDataItem = new InstrumentHistoryData();

                    newDataItem.CloseAskPrice = dataItem.AskPrice;
                    newDataItem.CloseBidPrice = dataItem.BidPrice;
                    newDataItem.OpenAskPrice = dataItem.AskPrice;
                    newDataItem.OpenBidPrice = dataItem.BidPrice;
                    newDataItem.HighAskPrice = dataItem.AskPrice;
                    newDataItem.HighBidPrice = dataItem.BidPrice;
                    newDataItem.LowAskPrice = dataItem.AskPrice;
                    newDataItem.LowBidPrice = dataItem.BidPrice;
                    newDataItem.PriceDateTime = dataItem.PriceDatetime;
                    newDataItem.DataID = dataID;

                    newDataItem.ID = 0;
                    newDataItem.Resolution = resolution;
                    newBarItemCount = 1;

                    if (resolution == "DAY" || resolution == "WEEK" || resolution == "MONTH")
                        newDataItem.PriceDateTime = dataItem.PriceDatetime.Date;
                }

            }

            return newDataList;

        }

        public static bool IsNewBar(string resolution, DateTime lastPriceDt, DateTime dt)
        {
            if (resolution.Contains("MINUTE"))
            {
                int minuteVal = 1;

                if (resolution == "MINUTE_2")
                    minuteVal = 2;
                else if (resolution == "MINUTE_3")
                    minuteVal = 3;
                else if (resolution == "MINUTE_5")
                    minuteVal = 5;
                else if (resolution == "MINUTE_10")
                    minuteVal = 10;
                else if (resolution == "MINUTE_15")
                    minuteVal = 15;
                else if (resolution == "MINUTE_30")
                    minuteVal = 30;


                if (dt.Day != lastPriceDt.Day || dt.Hour != lastPriceDt.Hour || (dt.Minute / minuteVal) != (lastPriceDt.Minute / minuteVal))
                    return true;
            }
            else if (resolution.Contains("HOUR"))
            {
                int hourVal = 1;

                if (resolution == "HOUR_2")
                    hourVal = 2;
                else if (resolution == "HOUR_3")
                    hourVal = 3;
                else if (resolution == "HOUR_4")
                    hourVal = 4;

                if (dt.Day != lastPriceDt.Day || (dt.Hour / hourVal) != (lastPriceDt.Hour / hourVal))
                    return true;
            }
            else if (resolution == "DAY")
            {
                if (dt.Day != lastPriceDt.Day)
                    return true;
            }
            else if (resolution == "WEEK")
            {
                //if time diff is more than 7 days or 
                //date is later and day of week is earlier
                if(((dt - lastPriceDt) > new TimeSpan(7,0,0,0,0)) || 
                    (dt > lastPriceDt && dt.DayOfWeek < lastPriceDt.DayOfWeek))                          
                    return true;
            }
            else if (resolution == "MONTH")
            {
                 if (dt.Month != lastPriceDt.Month)
                    return true;
            }
          

            return false;
        }

        public static DateTime GetRoundedBarDt(string resolution, DateTime dt)
        {
            DateTime dtNew = new DateTime();

            if (resolution.Contains("MINUTE"))
            {
                int minuteVal = 1;

                if (resolution == "MINUTE_2")
                    minuteVal = 2;
                else if (resolution == "MINUTE_3")
                    minuteVal = 3;
                else if (resolution == "MINUTE_5")
                    minuteVal = 5;
                else if (resolution == "MINUTE_10")
                    minuteVal = 10;
                else if (resolution == "MINUTE_15")
                    minuteVal = 15;
                else if (resolution == "MINUTE_30")
                    minuteVal = 30;

                dtNew = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, minuteVal * (dt.Minute / minuteVal), 0);                
            }
            else if (resolution.Contains("HOUR"))
            {
                int hourVal = 1;

                if (resolution == "HOUR_2")
                    hourVal = 2;
                else if (resolution == "HOUR_3")
                    hourVal = 3;
                else if (resolution == "HOUR_4")
                    hourVal = 4;

                dtNew = new DateTime(dt.Year, dt.Month, dt.Day, hourVal * (dt.Hour/hourVal), 0, 0);
            }
            else
            {
                dtNew = dt.Date;
            }

            return dtNew;
        }

        public static int GetMinutesFromResolution(string resolution)
        {
            int minuteVal = 0;
            
            if (resolution == "MINUTE")
                minuteVal = 1;
            else if (resolution == "MINUTE_2")
                minuteVal = 2;
            else if (resolution == "MINUTE_3")
                minuteVal = 3;
            else if (resolution == "MINUTE_5")
                minuteVal = 5;
            else if (resolution == "MINUTE_10")
                minuteVal = 10;
            else if (resolution == "MINUTE_15")
                minuteVal = 15;
            else if (resolution == "MINUTE_30")
                minuteVal = 30;

            return minuteVal;

        }
    }
}
