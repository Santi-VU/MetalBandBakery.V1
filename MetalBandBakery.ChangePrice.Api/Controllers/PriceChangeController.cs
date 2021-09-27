using MetalBandBakery.ChangePrice.Api.Repositories;
using MetalBandBakery.Core.Services;
using MetalBandBakery.Infra.Repository.HTTP;
using MetalBandBakey.Infra.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetalBandBakery.ChangePrice.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceChangeController : ControllerBase
    {
        private readonly ILogger<PriceChangeController> _logger;

        public PriceChangeController(ILogger<PriceChangeController> logger)
        {
            _logger = logger;
        }

        private bool Exist(char product)
        {
            return PriceChange._prices.ContainsKey(product);
        }

        private bool ValidateNewPrice(decimal newPrice)
        {
            return newPrice > 0;
        }

        [HttpGet]
        public void ModifyPrice(char product, decimal newPrice)
        {
            if (!Exist(product))
            {
                Console.WriteLine($"Product {product} doesn't exist");
                return;
            }

            if (!ValidateNewPrice(newPrice))
            {
                Console.WriteLine($"Can't asign new value price with value 0 or less...");
                return;
            }

            IChangePriceService _RESTpriceService = new RestfullPriceService();
            decimal lastPrice = _RESTpriceService.GetProductPrice(product);

            if (_RESTpriceService.SetPriceProduct(product, newPrice))
                Console.WriteLine($"Product {product} price changed: {lastPrice} -> {newPrice.ToString()}");
        }
    }
}
