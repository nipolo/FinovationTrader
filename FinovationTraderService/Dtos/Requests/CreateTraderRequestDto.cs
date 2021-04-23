using System;
using System.Collections.Generic;

using FinovationTrader.Dtos.Models;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace FinovationTrader.Dtos.Requests
{
    public class CreateTraderRequestDto : IRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BirthDate { get; set; }

        public List<CryptocurrencyDto> Cryptocurrencies { get; set; }

        public IFormFile AvatarImage { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
