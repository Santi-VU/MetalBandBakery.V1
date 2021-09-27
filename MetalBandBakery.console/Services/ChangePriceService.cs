using MetalBandBakery.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.console.Services
{
    class ChangePriceService : IChangePriceService
    {
        private static IChangePriceService _RESTpriceService;
        private static Dictionary<char, decimal> _prices = new Dictionary<char, decimal>()
        {
            {'B', 0.65m },
            {'M', 1.00m },
            {'C', 1.35m },
            {'W', 1.50m },
        };

        public ChangePriceService(IChangePriceService RESTpriceService)
        {
            _RESTpriceService = RESTpriceService;
        }

        private bool Exist(char product)
        {
            return _prices.ContainsKey(product);
        }

        private bool ValidateNewPrice(decimal newPrice)
        {
            return newPrice > 0;
        }

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

            Console.WriteLine($"Product {product} price changed: {_RESTpriceService.GetProductPrice(product).ToString()} -> {newPrice.ToString()}");
            _RESTpriceService.SetPriceProduct(product, newPrice);
        }
    }
}
