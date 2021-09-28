using MetalBandBakery.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetalBandBakery.MVC.ViewsModels
{
    public class GestCartViewModel
    {
        public string Warehouse { get; set; }
        public char SortProduct { get; set; }
        public int Quantity { get; set; }
    }
}