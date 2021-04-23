using System;
using System.Threading.Tasks;

using FinovationTrader.Dtos.Requests;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace FinovationTrader.API.Controllers
{
    [ApiController]
    [Route("api/traders")]
    public class TraderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TraderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateTraderRequestDto request)
        {
            await _mediator.Send(request);

            return Ok();
        }

        [HttpGet("{traderId}/avatarImage")]
        public async Task<IActionResult> GetAvatarImage(Guid traderId)
        {
            var response = await _mediator.Send(new GetAvatarImageRequestDto() { TraderId = traderId });

            return File(response.AvatarImage, "image/jpeg");
        }

        [HttpGet]
        public async Task<IActionResult> GetTraders([FromBody] GetTradersRequestDto request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrader(Guid id)
        {
            var response = await _mediator.Send(new DeleteTraderRequestDto() { Id = id });

            return Ok(response);
        }
    }
}
