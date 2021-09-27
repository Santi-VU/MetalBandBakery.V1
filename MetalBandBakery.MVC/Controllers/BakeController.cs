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
    public class BakeController : Controller
    {
        // GET: Bake
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Bake()
        {
            SoapStockService wcfStockService = new SoapStockService();
            List<int> stocks = wcfStockService.GetStocks();

            SoapNameProductService wcfNameService = new SoapNameProductService();
            List<string> names = wcfNameService.GetAllProducts();
            List<char> sorts = wcfNameService.GetAllProductSorts();

            RestFullChangerService restChangerService = new RestFullChangerService();

            var viewModel = new ListBakeViewModel();
            viewModel.bakes = new List<Bake>();
            for (int i = 0; i < names.Count; i++)
            {
                List<Material> materials = new List<Material>();
                foreach (var j in restChangerService.GetListOfProduct(sorts[i]))
                {
                    materials.Add(new Material() { Name = j.Item1, Price = j.Item2 });
                }

                viewModel.bakes.Add(new Models.Bake { Sort = sorts[i].ToString(), Name = names[i], Stock = stocks[i], Materials = materials});
            }

            return View(viewModel);
        }

        public ActionResult Edit(Bake bake)
        {
            var viewModel = new EditBakeViewModel();

            viewModel.Product = bake;

            return View(bake);
        }

        public ActionResult BakeEdited(Bake ob)
        {
            var viewModel = new EditBakeViewModel();

            //IPriceService _restPriceService = new RestfullPriceService();
            //_restPriceService.ModifyPrice(ob.Sort[0], Decimal.Parse(ob.Price.ToString()));

            if (ob.Stock > 0)
            {
                IStockService _WCFstockService = new SoapStockService();
                _WCFstockService.AddStockWithQuantity(ob.Sort[0], ob.Stock);
            } else
            {
                IStockService _WCFstockService = new SoapStockService();
                _WCFstockService.RemoveStock(ob.Sort[0], Math.Abs(ob.Stock));
            }

            return Redirect("Bake/Bake");
        }
    }
}