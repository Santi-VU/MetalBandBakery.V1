using MetalBandBakery.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetalBandBakery.MVC.ViewsModels
{
    public class ListCartViewModel
    {
        public List<Bake> products { get; set; }
        public Cart cart { get; set; }
    }
}