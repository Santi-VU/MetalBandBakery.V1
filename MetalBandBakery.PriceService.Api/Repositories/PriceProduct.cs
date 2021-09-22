using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetalBandBakery.PriceService.Api.Repositories
{
    public class PriceProduct : IPriceProduct
    {
        public static Dictionary<char, decimal> _prices = new Dictionary<char, decimal>()
        {
            {'B', 0.65m },
            {'M', 1.00m },
            {'C', 1.35m },
            {'W', 1.50m },
        };

        public decimal GetProductPrice(char product)
        {
            return _prices[product];
        }

        public bool ItIsEnoughtMoney(decimal moneyForPay, decimal totalBuy)
        {
            return moneyForPay >= totalBuy;
        }
    }
}
