using currencyAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace currencyAPI.Controllers
{
    public class ExchageRateDBContext : DbContext
    {
        public  DbSet<Currency> Currencies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=ExchangeRateDB;Integrated Security=True");
            
        }
    }
}
