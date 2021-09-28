using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetalBandBakery.MVC.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Bake> Stock { get; set; }

        public Warehouse()
        {
            Stock = new List<Bake>();
        }
    }
}