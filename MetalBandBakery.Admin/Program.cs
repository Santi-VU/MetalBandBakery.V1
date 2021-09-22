using MetalBandBakery.Core.Services;
using MetalBandBakery.Infra.Repository.HTTP;
using MetalBandBakey.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.Admin
{
    class Program
    {
        private static IChangerService _RESTchangerService = new RestFullChangerService();
        private static IPriceService _RESTpriceService = new RestfullPriceService();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Introduzca producto a cambiar");
                string productToChange = Console.ReadLine();

                Console.WriteLine("Introduzca nuevo precio para el producto");
                decimal newPrice = Convert.ToDecimal(Console.ReadLine());

                decimal lastPrice = _RESTpriceService.GetProductPrice(productToChange[0]);
                if (!_RESTchangerService.ModifyPrice(productToChange[0], newPrice))
                {
                    Console.WriteLine($"No pudo modificarse el precio del producto {productToChange}");
                }
                else
                {
                    Console.WriteLine($"Precio del producto {productToChange} modificado: {lastPrice} -> {newPrice}");
                }
            }
        }
    }
}
