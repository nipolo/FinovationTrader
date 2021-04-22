using System;

namespace FinovationTrader.Data.States
{
    public class Cryptocurrency
    {
        public string Symbol { get; set; }

        public string Currency { get; set; }

        public Guid TraderId { get; set; }
    }
}