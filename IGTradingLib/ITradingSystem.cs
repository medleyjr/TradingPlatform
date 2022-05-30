using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dto.endpoint.positions.create.otc.v1;
using dto.endpoint.positions.edit.v1;
using dto.endpoint.positions.close.v1;

namespace IGTradingLib
{
    public interface ITradingSystem
    {
        dto.endpoint.confirms.ConfirmsResponse CreatePosition(CreatePositionRequest req);
        EditPositionResponse EditPosition(string dealId, EditPositionRequest req);
        ClosePositionResponse ClosePosition(ClosePositionRequest req);
        dto.endpoint.positions.get.otc.v1.PositionsResponse GetOpenPositions();
        dto.endpoint.positions.get.otc.v1.OpenPosition GetPosition(string dealId);

    }
}
