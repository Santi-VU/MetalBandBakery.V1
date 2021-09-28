using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using MetalBandBakery.Infra.Repository.DB;
using System.Linq;
using System.Threading.Tasks;

namespace MetalBandBakery.PriceService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PricesController : ControllerBase
    {
        private readonly ILogger<PricesController> _logger;

        public PricesController(ILogger<PricesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Tuple<char, decimal>> Get()
        {
            List<Tuple<char, decimal>> list = new List<Tuple<char, decimal>>();

            List<string> lines = DBService.ReadTextFromFile(DBService.pricesFile);
            foreach(var i in lines)
            {
                string[] aux = i.Split('=');
                list.Add(new Tuple<char, decimal>(aux[0][0], Decimal.Parse(aux[1])));
            }

            return list;
        }

        [HttpGet("getPrice/{product}")]
        public decimal GetPrice(char product)
        {
            if (!DBService.ExistsIdInFile(product, DBService.pricesFile))
                return -1;

            List<string> lines = DBService.ReadTextFromFile(DBService.pricesFile);
            int index = DBService.GetIndexOfText(product, lines);

            return Decimal.Parse(lines[index].Split('=')[1]);
        }

        [HttpGet("isEnough/{moneyForPay}/{totalBuy}")]
        public bool GetEnough(decimal moneyForPay, decimal totalBuy)
        {
            return moneyForPay >= totalBuy;
        }

        [HttpGet("setPrice/{product}/{newPrice}")]
        public bool GetNewPrice(char product, decimal newPrice)
        {
            if (!DBService.ExistsIdInFile(product, DBService.pricesFile))
                return false;

            if (newPrice <= 0)
                return false;

            List<string> lines = DBService.ReadTextFromFile(DBService.pricesFile);
            int index = DBService.GetIndexOfText(product, lines);

            lines[index] = product.ToString() + "=" + newPrice;
            DBService.ReWriteFile(DBService.pricesFile, lines);

            //Repositories.PriceProduct._prices[product] = newPrice;
            return true;
        }

        [HttpGet("getAllPrices")]
        public List<decimal> GetAllPriceS()
        {
            List<decimal> prices = new List<decimal>();
            foreach (var i in DBService.ReadTextFromFile(DBService.pricesFile))
            {
                prices.Add(Decimal.Parse(i.Split('=')[1]));
            }
            return prices;
        }
    }
}
