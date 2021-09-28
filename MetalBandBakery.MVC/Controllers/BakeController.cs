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

        public ActionResult Bake(Warehouse warehouse)
        {
            SoapStockService wcfStockService = new SoapStockService();
            SoapNameProductService wcfNameService = new SoapNameProductService();
            RestFullChangerService restChangerService = new RestFullChangerService();
            RestfullPriceService restPriceService = new RestfullPriceService();

            List<Bake> bakes = new List<Bake>();
            foreach (var i in wcfStockService.ManyStockOfWarehouse(warehouse.Name))
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
                bakes.Add(b);
            }

            var viewModel = new ListBakeViewModel();
            viewModel.bakes = bakes;
            viewModel.Warehouse = warehouse.Name;

            return View(viewModel);
        }

        public ActionResult Edit(EditBakeViewModel editedModel)
        {
            SoapStockService soapStockService = new SoapStockService();
            RestFullChangerService changerService = new RestFullChangerService();
            SoapNameProductService soapNameProductService = new SoapNameProductService();
            RestFullWarehouseService restWarehouseService = new RestFullWarehouseService();

            Bake b = new Bake();
            b.Sort = editedModel.BakeSort.ToString();
            b.Name = soapNameProductService.GetProductName(b.Sort[0]);
            b.Stock = soapStockService.ManyStockOfWarehouseProduct(editedModel.Warehouse ,b.Sort[0]);

            List<Material> materials = new List<Material>();
            foreach (var i in changerService.GetListOfProduct(b.Sort[0]))
            {
                materials.Add(new Material() { Name = i.Item1, Price = i.Item2 });
            }

            b.Materials = materials;

            var viewModel = new EditedBakeViewModel();
            viewModel.Warehouse = editedModel.Warehouse;
            viewModel.bake = b;

            return View(viewModel);
        }

        public ActionResult BakeEdited(EditedBakeViewModel editedModel)
        {
            SoapStockService soapStockService = new SoapStockService();
            RestFullChangerService changerService = new RestFullChangerService();
            SoapNameProductService soapNameProductService = new SoapNameProductService();
            RestFullWarehouseService restWarehouseService = new RestFullWarehouseService();

            if (editedModel.bake.Stock == 0)
                return null;

            if (editedModel.bake.Stock > 0)
            {
                soapStockService.AddStockMaster(editedModel.Warehouse, editedModel.bake.Sort[0], editedModel.bake.Stock);
            } else
            {
                soapStockService.RemoveStockMaster(editedModel.Warehouse, editedModel.bake.Sort[0], editedModel.bake.Stock);
            }

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