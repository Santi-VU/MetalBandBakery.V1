using MetalBandBakery.Core;
using MetalBandBakery.Core.Services;
using MetalBandBakery.Infra.Repository;
using MetalBandBakery.Infra.Repository.HTTP;
using MetalBandBakery.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetalBandBakery.console.Services;
using MetalBandBakey.Infra.Repository;

namespace MetalBandBakery.console
{
    class Program
    {
        private static IStockService _WCFstockService = new SoapStockService();
        private static IReplaceStockService WCFreplaceService = new SoapReplaceStockService();
        private static IPriceService _RESTpriceService = new RestfullPriceService();

        static void Main(string[] args)
        {
            //Application application = new Application(_priceService, _stockService);
            Application application = new Application(_RESTpriceService, _WCFstockService, WCFreplaceService);
            application.Run();
        }
    }
}
