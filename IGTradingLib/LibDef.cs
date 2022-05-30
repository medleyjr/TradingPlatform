using IGTradingLib.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGTradingLib
{
    public static class LibDef
    {
        public static IIGStatusEvent m_StatusEvent = null;
        public static ITrading m_trading = null;
        public static string m_AccountTimeZone = "";
        public static TimeZoneInfo m_TimeZoneAcc = TimeZoneInfo.Local;

        public static Action<DataStreamUpdateEvent> OnStreamEvent;
        public static Dictionary<long, Action<DataStreamUpdateEvent>> OnStreamEvents = new Dictionary<long,Action<DataStreamUpdateEvent>>();

        public static void NotifyOnStreamPrice(DataStreamUpdateEvent dataEvent)
        {
            if (LibDef.OnStreamEvents.ContainsKey(dataEvent.DataID.ID))
            {
                var eNotify = LibDef.OnStreamEvents[dataEvent.DataID.ID];
                if (eNotify != null)
                    eNotify(dataEvent);
            }
        }
        
        public static string BUY = "BUY";
        public static string SELL = "SELL";

        public static string IT_INDICES = "INDICES";
        public static string IT_CURRENCY = "CURRENCIES";
        public static string IT_COMMODITIES = "COMMODITIES";

        public static void RaiseErrorMessage(string msg)
        {
            if (m_StatusEvent == null)
                return;

            m_StatusEvent.RaiseError(msg);
        }


    }
}
