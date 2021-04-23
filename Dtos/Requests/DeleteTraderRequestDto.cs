using System;

using MediatR;

namespace FinovationTrader.Dtos.Requests
{
    public class DeleteTraderRequestDto : IRequest
    {
        public Guid Id { get; set; }
    }
}
