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
        /*
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
        */
        public ActionResult Cart(Warehouse wareHouse)
        {
            SoapStockService wcfStockService = new SoapStockService();
            List<int> stocks = wcfStockService.GetStocks();

            SoapNameProductService wcfNameService = new SoapNameProductService();
            List<string> names = wcfNameService.GetAllProducts();
            List<char> sorts = wcfNameService.GetAllProductSorts();

            RestFullChangerService restChangerService = new RestFullChangerService();

            var viewModel = new ListCartViewModel();
            viewModel.products = new List<Bake>();

            foreach (var i in wcfStockService.ManyStockOfWarehouse(wareHouse.Name))
            {
                Bake b = new Bake();
                b.Sort = i.Item1.ToString();
                b.Stock = i.Item2;
                b.Name = wcfNameService.GetProductName(b.Sort[0]);

                List<Material> materials = new List<Material>();
                foreach (var j in restChangerService.GetListOfProduct(b.Sort[0]))
                {
                    materials.Add(new Material() { Name = j.Item1, Price = j.Item2 });
                }
                b.Materials = materials;
                viewModel.products.Add(b);
            }

            RestFullCartService restCartService = new RestFullCartService();
            Cart c = new Cart();
            c.productList = restCartService.Get();
            c.totalPayed = restCartService.GetCurrentPayed();
            viewModel.cart = c;
            viewModel.Warehouse = wareHouse.Name;

            return View(viewModel);
        }

        public ActionResult Pay(ListCartViewModel listCartModel)
        {
            RestFullCartService restCartService = new RestFullCartService();
            restCartService.Pay(listCartModel.cart.totalPayed);

            Warehouse warehouse = new Warehouse() { Name = listCartModel.Warehouse };
            return RedirectToAction("Cart", warehouse);
        }

        public ActionResult Add(GestCartViewModel model)
        {
            SoapStockService wcfStockService = new SoapStockService(); 
            if (wcfStockService.CanBenRemovedMaster(model.Warehouse, model.SortProduct, model.Quantity))
            {
                wcfStockService.RemoveStockMaster(model.Warehouse, model.SortProduct, 1);
                RestFullCartService restCartService = new RestFullCartService();
                restCartService.AddProduct(model.SortProduct);
            }

            return RedirectToAction("Cart", new Warehouse() { Name = model.Warehouse });
        }

        public ActionResult Del(GestCartViewModel model)
        {
            RestFullCartService restCartService = new RestFullCartService();
            int unitsInCart = restCartService.GetUnitsOfProduct(model.SortProduct);
            if (unitsInCart > 0)
            {
                restCartService.RemoveProduct(model.SortProduct);
                SoapStockService wcfStockService = new SoapStockService();
                wcfStockService.AddStockMaster(model.Warehouse, model.SortProduct, 1);
            }

            return RedirectToAction("Cart", new Warehouse() { Name = model.Warehouse });
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
            return Redirect("https://localhost:44337/");
        }

        public ActionResult Stock()
        {
            RestFullWarehouseService restWarehouseService = new RestFullWarehouseService();
            List<Tuple<int, string>> namesWarehouses = restWarehouseService.Get();

            SoapStockService soapStockService = new SoapStockService();
            RestFullChangerService changerService = new RestFullChangerService();
            SoapNameProductService soapNameProductService = new SoapNameProductService();

            List<Warehouse> warehouses = new List<Warehouse>();
            List<Bake> stockBakes = new List<Bake>();
            List<Material> materialBake = null;
            foreach (var i in namesWarehouses)
            {
                Warehouse warehouse = new Warehouse() { Id = i.Item1, Name = i.Item2 };

                foreach (var j in soapStockService.ManyStockOfWarehouse(i.Item2))
                {

                    Bake b = new Bake();
                    b.Sort = j.Item1.ToString();
                    b.Stock = j.Item2;
                    b.Name = soapNameProductService.GetProductName(b.Sort[0]);

                    materialBake = new List<Material>();
                    foreach (var y in changerService.GetListOfProduct(b.Sort[0]))
                    {
                        materialBake.Add(new Material { Name = y.Item1, Price = y.Item2 });
                    }
                    b.Materials = materialBake;

                }
                warehouse.Stock = stockBakes;
                warehouses.Add(warehouse);
            }

            var viewModel = new ViewsModels.ListWarehouseViewModel();
            viewModel.Warehouses = warehouses;

            return View(viewModel);
        }
    }
}