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
            if (State == 1)         //Order by value asc
            {
                return result.OrderBy(x => x.CurrentRate);
            }
            else if (State == 2)    //Order by value desc
            {
                return result.OrderByDescending(x => x.CurrentRate);
            }
            else if (State == 3)    //Order by Name asc
            {
                return result.OrderBy(x => x.Currency);
            }
            else if (State == 4)    //Order by Name desc
            {
                return result.OrderByDescending(x => x.Currency);
            }
            else
            {
                return result;      // no order by at all
            }
        }
    }
}
