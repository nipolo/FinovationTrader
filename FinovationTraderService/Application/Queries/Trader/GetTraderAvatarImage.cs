using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Finovation.Core.Application.Abstractions;
using Finovation.Core.Common.Exceptions;

using FinovationTrader.Application.Services;
using FinovationTrader.Dtos.Requests;
using FinovationTrader.Dtos.Responses;

using MediatR;

using TraderPerson = FinovationTrader.Data.States.Trader;

namespace FinovationTrader.Application.Queries.Trader
{
    public class GetTraderAvatarImage : IRequestHandler<GetAvatarImageRequestDto, GetAvatarImageResponseDto>
    {
        private readonly IRepository<TraderPerson> _repository;

        public GetTraderAvatarImage(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
        {
            _repository = unitOfWork.GetRepository<TraderPerson>();
        }

        public async Task<GetAvatarImageResponseDto> Handle(GetAvatarImageRequestDto request, CancellationToken cancellationToken)
        {
            var trader = await _repository.GetByIdAsync(request.TraderId);

            if (trader == null)
            {
                throw new AggregateNotFoundException("Cannot find trader with this id.");
            }

            var fileName = trader.AvatarImagePath.Split('/').Last();

            return new GetAvatarImageResponseDto()
            {
                AvatarImage = new FileStream(trader.AvatarImagePath, FileMode.Open),
                FileDownloadName = fileName
            };
        }
    }
}
