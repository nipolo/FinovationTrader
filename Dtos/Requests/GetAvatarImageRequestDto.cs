using System;

using FinovationTrader.Dtos.Responses;

using MediatR;

namespace FinovationTrader.Dtos.Requests
{
    public class GetAvatarImageRequestDto : IRequest<GetAvatarImageResponseDto>
    {
        public Guid TraderId { get; set; }
    }
}
