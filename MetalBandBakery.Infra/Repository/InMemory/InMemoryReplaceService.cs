using MetalBandBakery.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.Core.Services
{
    public class ReplaceStockService : IReplaceStockService
    {
        private IStockService _WCFstockService;

        public ReplaceStockService(IStockService WCFstockService)
        {
            _WCFstockService = WCFstockService;
        }

        public bool NeedToBeReplace(char product)
        {
            if (_WCFstockService.ManyStock(product) > 1)
                return false;
            return true;
        }

        public void ReplaceProduct(char product)
        {
            _WCFstockService.AddStock(product);
        }
    }
}
