using MetalBandBakery.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetalBandBakery.MVC.ViewsModels
{
    public class EditProductViewModel
    {
        public List<Material> Materials { get; set; }
        public Bake Product { get; set; }
    }
}