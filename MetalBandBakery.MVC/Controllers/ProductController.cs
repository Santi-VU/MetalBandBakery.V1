using MetalBandBakery.Infra.Repository.HTTP;
using MetalBandBakery.MVC.Models;
using MetalBandBakery.MVC.ViewsModels;
using MetalBandBakey.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetalBandBakery.MVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Product()
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

                viewModel.bakes.Add(new Models.Bake { Sort = sorts[i].ToString(), Name = names[i], Stock = stocks[i], Materials = materials });
            }

            return View(viewModel);
        }

        public ActionResult Edit(Bake bake)
        {
            RestFullChangerService restChangerService = new RestFullChangerService();
            List<Material> list = new List<Material>();
            List<Material> current = new List<Material>();

            foreach (var i in restChangerService.GetAllMaterials())
            {
                list.Add(new Material { Name = i.Item1, Price = i.Item2 });
            }
            list.Remove(list[list.Count-1]);

            foreach (var i in restChangerService.GetListOfProduct(bake.Sort[0]))
            {
                current.Add(new Material { Name = i.Item1, Price = i.Item2 });
            }
            bake.Materials = current;

            var viewModel = new EditProductViewModel();
            viewModel.Product = bake;
            viewModel.Materials = list;

            return View(viewModel);
        }

        public ActionResult Add(GestMaterialViewModel data)
        {
            if (data == null)
            {
                return null;
            }

            RestFullChangerService restChargetService = new RestFullChangerService();
            restChargetService.AddMatOf(data.Sort, data.Material);
            decimal newPrice = restChargetService.GetMatsPriceOf(data.Sort);

            RestfullPriceService restPriceService = new RestfullPriceService();
            restPriceService.ModifyPrice(data.Sort, newPrice);

            Bake bake = new Bake();
            bake.Sort = data.Sort.ToString();

            SoapNameProductService soapNameService = new SoapNameProductService();
            bake.Name = soapNameService.GetProductName(bake.Sort[0]);

            SoapStockService soapStockService = new SoapStockService();
            bake.Stock = soapStockService.ManyStock(bake.Sort[0]);

            RestFullChangerService restChangerService = new RestFullChangerService();
            List<Material> materials = new List<Material>();
            foreach(var i in restChangerService.GetListOfProduct(bake.Sort[0]))
            {
                materials.Add(new Material { Name = i.Item1, Price = i.Item2 });
            }
            bake.Materials = materials;

            return RedirectToAction("Edit/Product", bake);
        }

        public ActionResult Del(GestMaterialViewModel data)
        {
            if (data == null)
                return null;

            Bake bake = new Bake();
            bake.Sort = data.Sort.ToString();

            RestFullChangerService restChangerService = new RestFullChangerService();
            List<Tuple<string, decimal>> tuple = restChangerService.GetListOfProduct(bake.Sort[0]);
            bool contaisMat = false;

            foreach (var i in tuple)
            {
                if (i.Item1 == data.Material)
                {
                    contaisMat = true;
                    break;
                }
            }

            RestfullPriceService restPriceService = new RestfullPriceService();
            SoapNameProductService soapNameService = new SoapNameProductService();
            SoapStockService soapStockService = new SoapStockService();

            if (contaisMat != false)
            {
                restChangerService.RemoveMatOf(bake.Sort[0], data.Material);
                decimal newPrice = restChangerService.GetMatsPriceOf(data.Sort);
                restPriceService.ModifyPrice(data.Sort, newPrice);
            }

            bake.Name = soapNameService.GetProductName(bake.Sort[0]);
            bake.Stock = soapStockService.ManyStock(bake.Sort[0]);

            List<Material> materials = new List<Material>();
            foreach (var i in restChangerService.GetListOfProduct(bake.Sort[0]))
            {
                materials.Add(new Material { Name = i.Item1, Price = i.Item2 });
            }
            bake.Materials = materials;

            return RedirectToAction("Edit/Product", bake);
        }
    }
}