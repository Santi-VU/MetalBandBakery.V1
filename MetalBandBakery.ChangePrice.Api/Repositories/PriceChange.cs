using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.ChangePrice.Api.Repositories
{
    public class PriceChange : IPriceChange
    {
        public static Dictionary<char, decimal> _prices = new Dictionary<char, decimal>()
        {
            {'B', 0.65m },
            {'M', 1.00m },
            {'C', 1.35m },
            {'W', 1.50m },
        };

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

            Console.WriteLine($"Product {product} price changed: {_prices[product].ToString()} -> {newPrice.ToString()}");
            _prices[product] = newPrice;
        }
    }
}
