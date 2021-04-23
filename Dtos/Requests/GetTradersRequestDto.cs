using Finovation.Core.Common.Enums;

using FinovationTrader.Dtos.Responses;

using MediatR;

namespace FinovationTrader.Dtos.Requests
{
    public class GetTradersRequestDto : IRequest<GetTradersResponseDto>
    {
        public bool? OnlyActive { get; set; }
        
        public OrderByType? OrderBy { get; set; }

        public int? PageSize { get; set; }

        public int? StartingPage { get; set; }  
    }
}
