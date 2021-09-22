using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.console.Services
{
    class ChangeService
    {
        public static bool NeedsChange(decimal moneyForPay, decimal totalBuy)
        {
            return moneyForPay > totalBuy;
        }

        public static decimal GetChange(decimal moneyForPay, decimal totalBuy)
        {
            return moneyForPay - totalBuy;
        }
    }
}
