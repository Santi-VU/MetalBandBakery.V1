using MetalBandBakery.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.Infra.Repository.HTTP
{
    public class SoapReplaceStockService : IReplaceStockService
    {
        public bool NeedToBeReplace(char product)
        {
            Replace.WCF.IService wcfService = new Replace.WCF.ServiceClient();
            return wcfService.NeedToBeReplace(product);
        }

        public void ReplaceProduct(char product)
        {
            Replace.WCF.IService wcfService = new Replace.WCF.ServiceClient();
            wcfService.ReplaceProduct(product);
        }
    }
}
