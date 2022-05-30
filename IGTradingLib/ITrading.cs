using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dto.endpoint.search;
using IGTradingLib.Types;


namespace IGTradingLib
{
    public interface ITrading : ITradingSystem
    {
        void SetIGEvent(IIGStatusEvent status);
        bool Login(string apiKey, string userID, string pwd, bool bLive);
        void Logout();
        void SwapAccount();

        List<Market> SearchMarket(string str);
        int DownloadPriceHistory(string epic, string resolution, DateTime dtFrom, DateTime dtTo);        
        void StartDataStreaming();
        void StopDataStreaming();
        void PingIGServer();

    }
}
