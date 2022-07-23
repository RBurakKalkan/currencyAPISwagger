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
    public class CurrencyChangesController : Controller
    {
        ExchageRateDBContext ctx = new ExchageRateDBContext();
        [HttpGet("{Currency}")]

        public async Task<IEnumerable<GetCurrencyChanges>> GetCurrenciesForToday(string Currency)
        {
            var query = await (from c in ctx.Currencies
                               where c.Name == Currency + "-TRY" orderby c.DataDate
                               select new GetCurrencyChanges() 
                               {
                                   Currency = c.Name,
                                   Date = c.DataDate,
                                   Rate = c.Value
                               }).ToListAsync();


            for (int i = 0; i < query.Count; i++)
            {
                double Change = 0;
                string strChange;
                if (i >= 1)
                    Change = ((query[i].Rate / query[i - 1].Rate)*100)-100;
                else
                    Change = 0;

                if (Change < 0)
                {
                    strChange = String.Format("{0:0.00}", Change) + "%";
                }
                else if (Change > 0)
                {
                    strChange = "+" + String.Format("{0:0.00}", Change) + "%";
                }
                else
                {
                    strChange = "-";
                }
                query[i].Changes = strChange;
            }
            return query;
        }
    }
}
