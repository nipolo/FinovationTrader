using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Finovation.Core.Application.Abstractions;

using FinovationTrader.Application.Services;
using FinovationTrader.Dtos.Requests;
using TraderPerson = FinovationTrader.Data.States.Trader;

using MediatR;
using Finovation.Core.Common.Exceptions;

namespace FinovationTrader.Application.Commands.Trader
{
    public class CreateTraderCommand : AsyncRequestHandler<CreateTraderRequestDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;
        private readonly IRepository<TraderPerson> _repository;

        public CreateTraderCommand(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
        {
            _repository = unitOfWork.GetRepository<TraderPerson>();
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        protected override async Task Handle(CreateTraderRequestDto request, CancellationToken cancellationToken)
        {
            if (_repository.QueryEntities().Any(t => t.Email == request.Email))
            {
                throw new EntityAlreadyExistsException("User with this email already exists");
            }

            var avatarFilePath = await _fileStorageService.UploadFileAsync(request.AvatarImage);

            var trader = new TraderPerson()
            {
                Id = Guid.NewGuid(),
                AvatarImagePath = avatarFilePath,
                BirthDate = DateTime.Parse(request.BirthDate),
                Cryptocurrencies = request.Cryptocurrencies
                    .Select(c => new Data.States.Cryptocurrency()
                    {
                        Currency = c.Currency,
                        Symbol = c.Symbol
                    }),
                Email = request.Email,
                FirstName = request.Email,
                IsActive = true,
                LastName = request.LastName,
                Password = request.Password
            };

            await _repository.AddAsync(trader);

            try
            {
                _unitOfWork.SaveChanged();
            }
            catch
            {
                await _fileStorageService.RemoveFileAsync(avatarFilePath);
                throw;
            }
        }
    }
}
