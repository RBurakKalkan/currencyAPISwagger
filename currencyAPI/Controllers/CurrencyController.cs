using currencyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace currencyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {

        DateTime dataDate;
        [HttpGet]
        public async Task<IEnumerable<Currency>> GetFromXML()
        {
            string xmlStr;
            Uri uri = new Uri("https://www.tcmb.gov.tr/kurlar/today.xml");
            using (var wc = new WebClient())
            {
                xmlStr = await wc.DownloadStringTaskAsync(uri);
            }
            XmlDocument data = new XmlDocument();
            data.LoadXml(xmlStr);
            DataSet ds = new DataSet();
            ds.ReadXml(new XmlNodeReader(data));

            List<Currency> currencyList = new List<Currency>();
            string[] selectedCurrencies = { "USD", "EUR", "GBP", "CHF", "KWD", "SAR", "RUB" };  //The currencies that defined in the .NET Case document.
            dataDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["Tarih"].ToString());

            foreach (DataRow nodes in ds.Tables[1].Rows)
            {
                if (!selectedCurrencies.Contains(nodes["Kod"])) continue;
                currencyList.Add(new Currency
                {
                    Name = nodes["Kod"].ToString(),
                    DataDate = dataDate,
                    RecordDate = DateTime.Now,
                    Value = Convert.ToDouble(nodes["ForexBuying"].ToString().Replace('.', ',')) // I didn't know which price to choose, so i've decided to get "ForexBuying" because it has no NULL value.
                });
            }
            return currencyList;
        }
        [HttpGet("AddDb")]
        public async Task AddDB()
        {
            var XML = await GetFromXML();
            DateTime today = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            if (dataDate >= today)                                                              // If data is updated then add it to our db
            {
                foreach (var item in XML)
                {
                    using (var _context = new ExchageRateDBContext())
                    {
                        Currency currency = new Currency();
                        currency.DataDate = dataDate;
                        currency.Name = item.Name + "-TRY";
                        currency.RecordDate = DateTime.Now;
                        currency.Value = item.Value;
                        await _context.Currencies.AddAsync(currency);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}

