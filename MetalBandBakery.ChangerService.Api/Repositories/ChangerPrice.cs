using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.ChangerService.Api.Repositories
{
    public class ChangerPrice : IChangerPrice
    {
        public static Dictionary<char, decimal> _prices = new Dictionary<char, decimal>()
        {
            {'B', 0.65m },
            {'M', 1.00m },
            {'C', 1.35m },
            {'W', 1.50m },
        };

        public bool ModifyPrice(char product, decimal newPrice)
        {
            if (!_prices.ContainsKey(product))
                return false;

            if (newPrice <= 0)
                return false;

            _prices[product] = newPrice;
            return true;
        }
    }
}
