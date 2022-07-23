using System;
namespace currencyAPI.Models
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public DateTime DataDate { get; set; }
        public DateTime RecordDate { get; set; }
        public double Value { get; set; }
    }
}
