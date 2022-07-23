using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace currencyAPI.Controllers
{
    public class CurrencyChangesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
