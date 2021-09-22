using MetalBandBakery.Core;
using MetalBandBakery.Core.Services;
using MetalBandBakery.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetalBandBakery.console.Services;

namespace MetalBandBakery.console
{
    class Application
    {
        private IReplaceStockService _replaceService;
        private IPriceService _priceService;
        private IStockService _stockService;
        
        public Application(IPriceService priceService, IStockService stockService, IReplaceStockService replaceService)
        {
            _priceService = priceService;
            _stockService = stockService;
            _replaceService = replaceService;
        }

        public void Run()
        {
            while (true)
            {
                // Se inicia nuevo pedido, se muestran productos
                Order order = new Order();
                ShowProducts();

                //Se añaden los productos seleccionados al pedido, se asignan sus precios
                order.AddItems(UserAskForProducts());
                SetPricesToProductsOrder(order);

                //Se comprueba el Stock de los productos
                if (!CheckStockOfOrder(order))
                    return;

                //Si Stock es correcto, se comprueba que el pedido tenga asignado un precio total
                if (order.CanBePurchase())
                    //Se solicita dinero al cliente, se calcula el cambio
                    UserHaveToPay(order);

                //Se comprueba Stock de los productos comprados en OrderLines, se remplaza si es necesario.
                ReplaceItemsIfNeeded(order);

                // Evitar cierre de la aplicación al finalizar
                Console.ReadLine();
            }
        }

        private void ShowProducts()
        {
            Console.WriteLine($@"B	Brownie	    $0.65	{_stockService.ManyStock('B')}   
M	Muffin	    $1.00	{_stockService.ManyStock('M')} 
C	Cake Pop    $1.35	{_stockService.ManyStock('C')} 
W	Water	    $1.50	{_stockService.ManyStock('W')} ");
        }

        private string[] UserAskForProducts()
        {
            Console.WriteLine("\nIntroduce items to buy");
            string[] itemsToBuy = Console.ReadLine().Split(',');
            return itemsToBuy;
        }

        private void SetPricesToProductsOrder(Order order)
        {
            foreach (var i in order.OrderLines)
            {
                order.SetPrice(i.ItemId, _priceService.GetProductPrice(i.ItemId[0]));
            }
            Console.WriteLine("Total buy is: " + order.AmountToPay + "$");
        }

        private bool CheckStockOfOrder(Order order)
        {
            foreach (var i in order.OrderLines)
            {
                if (!_stockService.CanBeRemoved(i.ItemId[0], i.Amount))
                {
                    Console.WriteLine("Not enough stock of: " + i.ItemId[0]);
                    return false;
                }
                _stockService.RemoveStock(i.ItemId[0], i.Amount);
            }

            return true;
        }

        private void UserHaveToPay(Order order)
        {
            Console.WriteLine("Introduce money for pay");
            decimal moneyForPay = Convert.ToDecimal(Console.ReadLine());

            if (!_priceService.ItIsEnoughtMoney(moneyForPay, order.AmountToPay))
            {
                Console.WriteLine("Money not enought for the buy");
                return;
            }

            if (ChangeService.NeedsChange(moneyForPay, order.AmountToPay))
            {
                Console.WriteLine("Your change is: " + ChangeService.GetChange(moneyForPay, order.AmountToPay) + "$");
            }
            Console.WriteLine("Enjoy your products...!");
        }
    
        private void ReplaceItemsIfNeeded(Order order)
        {
            foreach (var i in order.OrderLines)
            {
                if (_replaceService.NeedToBeReplace(i.ItemId[0]))
                {
                    _replaceService.ReplaceProduct(i.ItemId[0]);
                    Console.WriteLine($"Stock of product {i.ItemId[0]} has been replace");
                }
            }
        }
    }
}
