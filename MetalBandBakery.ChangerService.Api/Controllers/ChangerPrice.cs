using MetalBandBakery.ChangerService.Api.Repositories;
using MetalBandBakery.Core.Services;
using MetalBandBakey.Infra.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetalBandBakery.ChangerService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChangerPrice : ControllerBase
    {
        private readonly ILogger<ChangerPrice> _logger;

        public ChangerPrice(ILogger<ChangerPrice> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Tuple<char, decimal>> Get()
        {
            List<Tuple<char, decimal>> list = new List<Tuple<char, decimal>>();
            foreach (var i in Repositories.ChangerPrice._prices)
            {
                list.Add(new Tuple<char, decimal>(i.Key, i.Value));
            }
            return list;
        }

        [HttpGet("{product}/{newPrice}")]
        public bool SetModifiyPrice(char product, decimal newPrice)
        {
            if (!Repositories.ChangerPrice._prices.ContainsKey(product))
                return false;

            if (newPrice <= 0)
                return false;

            IPriceService _RESTpriceService = new RestfullPriceService();
            decimal lastPrice = _RESTpriceService.GetProductPrice(product);
            _RESTpriceService.ModifyPrice(product, newPrice);
            return true;
        }
    }
}
