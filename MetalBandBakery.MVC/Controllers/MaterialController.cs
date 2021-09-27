using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetalBandBakery.Infra.Repository.HTTP;
using MetalBandBakery.Infra.Repository.DB;
using MetalBandBakery.Infra.Repository;
using System.Web.Mvc;
using MetalBandBakey.Infra.Repository;
using MetalBandBakery.MVC.Models;
using MetalBandBakery.MVC.ViewsModels;

namespace MetalBandBakery.MVC.Controllers
{
    public class MaterialController : Controller
    {
        // GET: Material
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Material()
        {
            RestFullChangerService restChangerService = new RestFullChangerService();
            List<Material> list = new List<Material>();
            foreach (var i in restChangerService.GetAllMaterials())
            {
                list.Add(new Models.Material { Name = i.Item1, Price = i.Item2 });
            }

            ListMaterialViewModel viewModel = new ListMaterialViewModel();
            viewModel.Materials = list;

            return View(viewModel);
        }

        public ActionResult Edit(Material material)
        {
            var viewModel = material;

            return View(viewModel);
        }

        public ActionResult MaterialEdited(Material mat)
        {
            if (mat.Price <= 0)
                return null;

            RestFullChangerService restChangerPrice = new RestFullChangerService();
            restChangerPrice.ModifiyMatPrice(mat.Name, mat.Price);

            SoapNameProductService soapNameService = new SoapNameProductService();
            RestfullPriceService restPriceService = new RestfullPriceService();

            foreach(var i in soapNameService.GetAllProductSorts())
            {
                restPriceService.ModifyPrice(i, restChangerPrice.GetMatsPriceOf(i));
            }

            return Redirect("Material/Material");
        }
    }
}