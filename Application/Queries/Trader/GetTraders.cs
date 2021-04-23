using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Finovation.Core.Application.Abstractions;
using Finovation.Core.Common.Enums;

using FinovationTrader.Dtos.Models;
using FinovationTrader.Dtos.Requests;
using FinovationTrader.Dtos.Responses;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TraderPerson = FinovationTrader.Data.States.Trader;

namespace FinovationTrader.Application.Queries.Trader
{
    public class GetTraders : IRequestHandler<GetTradersRequestDto, GetTradersResponseDto>
    {
        private readonly IRepository<TraderPerson> _repository;

        public GetTraders(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<TraderPerson>();
        }

        public async Task<GetTradersResponseDto> Handle(GetTradersRequestDto request, CancellationToken cancellationToken)
        {
            var traders = (await _repository
                .QueryEntities()
                .Where(t => !request.OnlyActive.HasValue
                            || !request.OnlyActive.Value
                            || t.IsActive)
                .AsNoTracking()
                .ToListAsync())
                .Select(t => new TraderDto()
                {
                    Name = t.FirstName + " " + t.LastName,
                    Email = t.Email,
                    Age = CalculateAge(t.BirthDate)
                });

            if (request.OrderBy.HasValue)
            {
                if (request.OrderBy == OrderByType.Age)
                {
                    traders = traders.OrderBy(t => t.Age);
                }
                else
                {
                    traders = traders.OrderBy(t => t.Email);
                }
            }
            if (request.PageSize.HasValue
                && request.PageSize >= 1
                && request.StartingPage.HasValue
                && request.StartingPage >= 0)
            {
                traders = traders
                    .Skip(request.PageSize.Value * (request.StartingPage.Value - 1))
                    .Take(request.PageSize.Value);
            }

            return new GetTradersResponseDto()
            {
                Traders = traders.ToList()
            };
        }

        private int CalculateAge(DateTime dateTime)
        {
            var now = DateTime.Now;
            var years = now.Year - dateTime.Year;

            if (now.Month < dateTime.Month
                || now.Day < dateTime.Day
                || now.TimeOfDay < dateTime.TimeOfDay)
            {
                --years;
            }

            return Math.Max(0, years);
        }
    }
}
