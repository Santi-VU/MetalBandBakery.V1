using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using MetalBandBakery.Infra.Repository.DB;
using System.Linq;
using System.Threading.Tasks;

namespace MetalBandBakery.WareHouseservice.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WareHouseController : ControllerBase
    {
        private readonly ILogger<WareHouseController> _logger;

        public WareHouseController(ILogger<WareHouseController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Tuple<int, string>> Get()
        { 
            List<string> lines = DBService.ReadTextFromFile(DBService.warehousesFile);
            List<Tuple<int, string>> warehouses = new List<Tuple<int, string>>();

            string[] aux = null;
            foreach (var i in lines)
            {
                aux = i.Split('=');
                warehouses.Add(new Tuple<int, string>(Int32.Parse(aux[0]), aux[1]));
            }

            return warehouses;
        }
    }
}
