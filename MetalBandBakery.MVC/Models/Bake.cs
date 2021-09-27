using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetalBandBakery.MVC.Models
{
    public class Bake
    {
        public string Sort { get; set; }
        public string Name { get; set; }
        public decimal Price { get => GetPrice(); }
        public int Stock { get; set; }
        public List<Material> Materials { get; set; }

        public Bake()
        {
            Materials = new List<Material>();
        }

        private decimal GetPrice()
        {
            decimal total = 0.00m;
            foreach(var i in Materials)
            {
                total = total + i.Price;
            }
            return total;
        }
    }
}