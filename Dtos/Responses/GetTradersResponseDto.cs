using System.Collections.Generic;

using FinovationTrader.Dtos.Models;

namespace FinovationTrader.Dtos.Responses
{
    public class GetTradersResponseDto
    {
        public List<TraderDto> Traders { get; set; }
    }
}
