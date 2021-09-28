using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetalBandBakery.MVC.ViewsModels
{
    public class PayCartViewModel
    {
        public string Warehouse { get; set; }
        public decimal totalPayed { get; set; }
    }
}