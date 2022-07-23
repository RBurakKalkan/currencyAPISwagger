using System;

namespace currencyAPI.Models
{
    public class GetCurrencyOrdered
    {
        public string Currency { get; set; }
        public DateTime LastUpdated { get; set; }
        public double CurrentRate { get; set; }
    }
}
