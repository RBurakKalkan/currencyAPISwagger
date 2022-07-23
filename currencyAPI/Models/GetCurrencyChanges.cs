using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace currencyAPI.Models
{
    public class GetCurrencyChanges
    {

        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public double Rate { get; set; }
        public string Changes { get; set; }

    }
}
