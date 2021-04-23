using System.Threading;
using System.Threading.Tasks;

using Finovation.Core.Application.Abstractions;
using Finovation.Core.Common.Exceptions;
using FinovationTrader.Dtos.Requests;
using TraderPerson = FinovationTrader.Data.States.Trader;

using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace FinovationTrader.Application.Commands.Trader
{
    public class DeleteTraderCommand : AsyncRequestHandler<DeleteTraderRequestDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TraderPerson> _repository;

        public DeleteTraderCommand(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<TraderPerson>();
            _unitOfWork = unitOfWork;
        }

        protected override async Task Handle(DeleteTraderRequestDto request, CancellationToken cancellationToken)
        {
            var trader = await _repository
                .QueryEntities()
                .Where(t => t.Id == request.Id && t.IsActive == true)
                .FirstOrDefaultAsync();
            var now = DateTime.Now;

            if (trader == null)
            {
                throw new EntityAlreadyExistsException($"Active user with id {request.Id} not found.");
            }

            trader.IsActive = false;
            trader.UpdatedOn = now;
            trader.DeletedOn = now;

            _repository.Update(trader);

            await _unitOfWork.SaveChangedAsync();
        }
    }
}
