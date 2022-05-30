using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGTradingLib.TradePlan.Data;
using IGTradingLib.DataModel;
using IGTradingLib.Types;
using IGTradingLib.TradeAnalysis;

namespace IGTradingLib.TradePlan
{
    public class TradePlanMAData : TradePlanData
    {
        public int MAShort { get; set; }
        public int MALong { get; set; }
        public int HistoryLengthRequired { get; set; }
        public decimal MaxMAOffset { get; set; }
        public decimal StopDistance { get; set; }
        public int PositionCount { get; set; }
        public decimal ExitLevel1 { get; set; }
        public decimal ExitLevel2 { get; set; }

    }

    public class TradePlanMA : TradePlanBase
    {
        //bool bHasOpenPos = false;
        List<InstrumentHistoryData> m_HistData = null;

        protected TradePlanMAData TradePlanMAData
        {
            get
            {
                return (TradePlanMAData)m_TradePlanData;
            }
        }

        protected override void Load()
        {
            LoadTradePlanData<TradePlanMAData>();
            m_HistData = DB.Rep.FindHistData(TradePlanMAData.Epic, TradePlanMAData.Resolution, m_TradeParam.CurrentDateTime.AddDays(-1), m_TradeParam.CurrentDateTime);
        }

        protected override void OnStreamPrice(DataStreamUpdateEvent dataPrice)
        {

        }

        protected override void EndOfInterval(InstrumentHistoryData bar)
        {
            m_HistData.Add(bar);


            for (int i = m_LongPositions.Count - 1; i >= 0; i--)
            {
                var pos = m_LongPositions[i];
                if (bar.CloseBidPrice > pos.LimitLevel || (bar.CloseBidPrice < pos.StopLevel))
                {
                    ClosePosition(pos, pos.DealSize);
                }
                else if (pos.DealSize == TradePlanMAData.PositionCount)
                {
                    if ((bar.CloseBidPrice - pos.Price) > (pos.Price - pos.StopLevel))
                        ClosePosition(pos, TradePlanMAData.PositionCount / 2);
                }
            }

            for (int i = m_ShortPositions.Count - 1; i >= 0; i--)
            {
                var pos = m_ShortPositions[i];
                if (bar.CloseAskPrice < pos.LimitLevel || (bar.CloseAskPrice > pos.StopLevel))
                {
                    ClosePosition(pos, pos.DealSize);
                }
                else if (pos.DealSize == TradePlanMAData.PositionCount)
                {
                    if ((pos.Price - bar.CloseAskPrice) > (pos.StopLevel - pos.Price))
                        ClosePosition(pos, TradePlanMAData.PositionCount/2);
                }
            }

            if (m_LongPositions.Count == 0 && m_ShortPositions.Count == 0)
            {
                decimal maShort = Indicators.GetMA(m_HistData, TradePlanMAData.MAShort);
                decimal maLong = Indicators.GetMA(m_HistData, TradePlanMAData.MALong);

                if (maShort > maLong && (bar.CloseBidPrice - maLong) > TradePlanMAData.MaxMAOffset)
                {
                    CreatePosition(LibDef.BUY, (decimal)TradePlanMAData.PositionCount, bar.CloseBidPrice.Value - TradePlanMAData.StopDistance,
                        bar.CloseBidPrice.Value + (TradePlanMAData.StopDistance * 8));
                }
                else if (maShort < maLong && (maLong - bar.CloseAskPrice) > TradePlanMAData.MaxMAOffset)
                {
                    CreatePosition(LibDef.SELL, (decimal)TradePlanMAData.PositionCount, bar.CloseAskPrice.Value - TradePlanMAData.StopDistance,
                        bar.CloseAskPrice.Value + (TradePlanMAData.StopDistance * 8));
                }

            }


        }



    }
}
