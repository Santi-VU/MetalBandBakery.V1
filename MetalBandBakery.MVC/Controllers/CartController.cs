using MetalBandBakery.Core.Services;
using MetalBandBakery.Infra.Repository.HTTP;
using MetalBandBakery.MVC.Models;
using MetalBandBakery.MVC.ViewsModels;
using MetalBandBakey.Infra.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MetalBandBakery.MVC.Controllers
{
    public class CartController : Controller
    {
        // GET: Bake
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cart()
        {
            SoapStockService wcfStockService = new SoapStockService();
            List<int> stocks = wcfStockService.GetStocks();

            SoapNameProductService wcfNameService = new SoapNameProductService();
            List<string> names = wcfNameService.GetAllProducts();
            List<char> sorts = wcfNameService.GetAllProductSorts();

            RestFullChangerService restChangerService = new RestFullChangerService();      

            var viewModel = new ListCartViewModel();
            viewModel.products = new List<Bake>();
            for (int i = 0; i < names.Count; i++)
            {
                List<Material> materials = new List<Material>();
                foreach (var j in restChangerService.GetListOfProduct(sorts[i]))
                {
                    materials.Add(new Material() { Name = j.Item1, Price = j.Item2 });
                }

                viewModel.products.Add(new Models.Bake 
                    { Sort = sorts[i].ToString(), Name = names[i], Stock = stocks[i], Materials = materials}
                );
            }

            RestFullCartService restCartService = new RestFullCartService();
            Cart c = new Cart();
            c.productList = restCartService.Get();
            c.totalPayed = restCartService.GetCurrentPayed();
            viewModel.cart = c;

            return View(viewModel);
        }

        public ActionResult Pay(ListCartViewModel listCartModel)
        {
            RestFullCartService restCartService = new RestFullCartService();
            restCartService.Pay(listCartModel.cart.totalPayed);
            return Redirect("Cart/Cart");
        }

        public ActionResult Add(Bake bake)
        {
            SoapStockService wcfStockService = new SoapStockService(); 
            if (wcfStockService.CanBeRemoved(bake.Sort[0], 1))
            {
                wcfStockService.RemoveStockUnit(bake.Sort[0]);
                RestFullCartService restCartService = new RestFullCartService();
                restCartService.AddProduct(bake.Sort[0]);
            }

            return Redirect("Cart/Cart");
        }

        public ActionResult Del(Bake bake)
        {
            RestFullCartService restCartService = new RestFullCartService();
            int unitsInCart = restCartService.GetUnitsOfProduct(bake.Sort[0]);
            if (unitsInCart > 0)
            {
                restCartService.RemoveProduct(bake.Sort[0]);
                SoapStockService wcfStockService = new SoapStockService();
                wcfStockService.AddStockUnit(bake.Sort[0]);
            }

            return Redirect("Cart/Cart");
        }

        public ActionResult Resume(Cart cart)
        {
            RestFullCartService restCartService = new RestFullCartService();
            cart.productList = restCartService.Get();
            return View(cart);
        }

        public ActionResult ResetCart()
        {
            RestFullCartService restCartService = new RestFullCartService();
            restCartService.RestartCart();
            return Redirect("Cart/Cart");
        }
    }
}