using System;
using System.Linq;
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
        private readonly ICryptoService _cryptoService;
        private readonly IRepository<TraderPerson> _repository;

        public CreateTraderCommand(IUnitOfWork unitOfWork, IFileStorageService fileStorageService, ICryptoService cryptoService)
        {
            _repository = unitOfWork.GetRepository<TraderPerson>();
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
            _cryptoService = cryptoService;
        }

        protected override async Task Handle(CreateTraderRequestDto request, CancellationToken cancellationToken)
        {
            if (_repository.QueryEntities().Any(t => t.Email == request.Email))
            {
                throw new EntityAlreadyExistsException("User with this email already exists.");
            }

            var avatarFilePath = await _fileStorageService.UploadFileAsync(request.AvatarImage);
            var traderId = Guid.NewGuid();
            var now = DateTime.Now;

            var trader = new TraderPerson()
            {
                Id = traderId,
                AvatarImagePath = avatarFilePath,
                BirthDate = DateTime.Parse(request.BirthDate),
                Cryptocurrencies = request.Cryptocurrencies
                    .Select(c => new Data.States.Cryptocurrency()
                    {
                        Currency = c.Currency,
                        Symbol = c.Symbol,
                        TraderId = traderId
                    }).ToList(),
                Email = request.Email,
                FirstName = request.FirstName,
                IsActive = true,
                LastName = request.LastName,
                Password = _cryptoService.Pbkdf2Hashing(request.Password),
                CreatedOn = now,
                UpdatedOn = now
            };

            await _repository.AddAsync(trader);

            try
            {
                await _unitOfWork.SaveChangedAsync();
            }
            catch
            {
                await _fileStorageService.RemoveFileAsync(avatarFilePath);
                throw;
            }
        }
    }
}
