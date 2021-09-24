using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
            foreach (var i in Repositories.PriceProduct._prices)
            {
                list.Add(new Tuple<char, decimal>(i.Key, i.Value));
            }
            return list;
        }

        [HttpGet("getPrice/{product}")]
        public decimal GetPrice(char product)
        {
            return Repositories.PriceProduct._prices[product];
        }

        [HttpGet("isEnough/{moneyForPay}/{totalBuy}")]
        public bool GetEnough(decimal moneyForPay, decimal totalBuy)
        {
            return moneyForPay >= totalBuy;
        }

        [HttpGet("setPrice/{product}/{newPrice}")]
        public bool GetNewPrice(char product, decimal newPrice)
        {
                if (!Repositories.PriceProduct._prices.ContainsKey(product))
                return false;

            if (newPrice <= 0)
                return false;

            Repositories.PriceProduct._prices[product] = newPrice;
            return true;
        }

        [HttpGet("getAllPrices")]
        public decimal[] GetAllPriceS()
        {
            decimal[] prices = new decimal[Repositories.PriceProduct._prices.Count];
            int cont = 0;
            foreach (var i in Repositories.PriceProduct._prices) {
                prices[cont] = i.Value;
                cont++;
            }
            return prices;
        }
    }
}
