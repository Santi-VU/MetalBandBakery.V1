using System;
using System.Collections.Generic;
using System.Linq;
using MetalBandBakery.Infra.Repository;
using System.Threading.Tasks;
using MetalBandBakery.Infra.Repository.DB;
using MetalBandBakey.Infra.Repository;

namespace MetalBandBakery.CartService.API.Repositories
{
    public class CartState
    {
        public static decimal totalValue { get => valueOfCurrentCart();}
        //public static decimal totalValue { get; set; }
        public static decimal totalPayed { get; set; }

        private static decimal valueOfCurrentCart()
        {
            List<string> lines = DBService.ReadTextFromFile(DBService.cartFile);
            if (lines == null || lines.Count == 0)
                return -1;

            List<Tuple<char, int>> cart = new List<Tuple<char, int>>();
            foreach (var i in lines)
            {
                string[] split = i.Split('=');
                cart.Add(new Tuple<char, int>(split[0][0], Int32.Parse(split[1])));
            }

            decimal total = 0.00m;
            RestfullPriceService restPriceService = new RestfullPriceService();
            foreach (var i in cart)
            {
                total = total + (i.Item2 * restPriceService.GetProductPrice(i.Item1));
            }
            return total;
        }
    }
}
