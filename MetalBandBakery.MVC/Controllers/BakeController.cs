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
            Inventory.WCF.IService wcfStockService = new Inventory.WCF.ServiceClient();
            int[] stocks = wcfStockService.GetStocks();
            string[] shorts = wcfStockService.GetShorts();

            decimal[] prices;
            string apiUrl = "https://localhost:44383/prices";
            using (WebClient client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;

                string json = client.DownloadString($"{apiUrl}/getAllPrices");

                prices = JsonConvert.DeserializeObject<decimal[]>(json);
            }

            string[] names = new string[shorts.Length];
            for (int i = 0; i < shorts.Length; i++)
            {
                if (shorts[i] == "B")
                    names[i] = "Brownie";
                else if (shorts[i] == "C")
                    names[i] = "Cake";
                else if (shorts[i] == "M")
                    names[i] = "Muffin";
                else if (shorts[i] == "W")
                    names[i] = "Water";
            }

            var viewModel = new ListBakeViewModel();
            viewModel.bakes = new List<Models.Bake>();
            for (int i = 0; i < names.Length; i++)
            {
                viewModel.bakes.Add(new Models.Bake { Sort = shorts[i], Name = names[i], Price = prices[i], Stock = stocks[i]});
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

            MetalBandBakery.Core.Services.IChangerService _RESTchangerService = new RestFullChangerService();
            _RESTchangerService.ModifyPrice(ob.Sort[0], Convert.ToDecimal(ob.Price));

            IStockService _WCFstockService = new SoapStockService();
            _WCFstockService.AddStockWithQuantity(ob.Sort[0], ob.Stock);

            return Redirect("Bake/Bake");
        }
    }
}