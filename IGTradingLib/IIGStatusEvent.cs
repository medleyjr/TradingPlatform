using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGPublicPcl;
using IGTradingLib.Types;
using dto.endpoint.auth.session;

namespace IGTradingLib
{
    public interface IIGStatusEvent
    {
        void UpdateIGStatus(IGStatusData status);
        void RaiseError(string errorStr);
        void DataStreamEvent(DataStreamUpdateEvent dataStreamEvent);
        void PositionChangeEvent(AccountDetails accountID, LsTradeSubscriptionData eventData);
        void AccountChangeEvent(AccountDetails accountID, StreamingAccountData eventData);
    }
}
