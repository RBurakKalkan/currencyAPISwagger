using currencyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace currencyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CurrencySelectionController : Controller
    {
        ExchageRateDBContext ctx = new ExchageRateDBContext();
        [HttpGet("{Date}/{State}")]
        public async Task<IEnumerable<GetCurrencyOrdered>> GetCurrenciesForToday(DateTime Date, int State)
        {
            var result = await (from c in ctx.Currencies
                                where c.DataDate == Convert.ToDateTime(Date.ToShortDateString())
                                select new GetCurrencyOrdered()
                                {
                                    Currency = c.Name,
                                    LastUpdated = c.DataDate,
                                    CurrentRate = c.Value
                                }).ToListAsync();
     
            return
                State == 1 ? result.OrderBy(x => x.CurrentRate) :               //Order by value asc
                State == 2 ? result.OrderByDescending(x => x.CurrentRate) :     //Order by value desc
                State == 3 ? result.OrderBy(x => x.Currency) :                  // Order by Name asc
                State == 4 ? result.OrderByDescending(x => x.Currency) :        // Order by Name desc
                result;                                                         // No order by at all

        }
    }
}
