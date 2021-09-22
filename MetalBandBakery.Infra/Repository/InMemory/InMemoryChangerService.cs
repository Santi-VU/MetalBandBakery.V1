using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.Core.Services
{
    public class InMemoryChangerService : IChangerService
    {
        private IPriceService _RESTpriceService;
        public static Dictionary<char, decimal> _prices = new Dictionary<char, decimal>()
        {
            {'B', 0.65m },
            {'M', 1.00m },
            {'C', 1.35m },
            {'W', 1.50m },
        };

        public InMemoryChangerService (IPriceService RESTpriceService)
        {
            _RESTpriceService = RESTpriceService;
        }

        public bool ModifyPrice(char product, decimal newPrice)
        {
            if (!_prices.ContainsKey(product))
                return false;

            if (newPrice <= 0)
                return false;

            _RESTpriceService.ModifyPrice(product, newPrice);
            return true;
        }
    }
}
