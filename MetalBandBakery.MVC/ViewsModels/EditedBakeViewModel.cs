using MetalBandBakery.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetalBandBakery.MVC.ViewsModels
{
    public class EditedBakeViewModel
    {
        public string Warehouse { get; set; }
        public Bake bake { get; set; }
    }
}