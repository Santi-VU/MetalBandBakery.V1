using MetalBandBakery.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.Infra.Repository.HTTP
{
    public class SoapNameProductService : IMarketingProducts
    {
        public string[] GetAllProducts()
        {
            NameService.WCF.IService wcfNameService = new NameService.WCF.ServiceClient();
            return wcfNameService.GetAllProducts();
        }

        public char[] GetAllProductSorts()
        {
            NameService.WCF.IService wcfNameService = new NameService.WCF.ServiceClient();
            return wcfNameService.GetAllProductSorts();
        }

        public string GetProductName(char product)
        {
            NameService.WCF.IService wcfNameService = new NameService.WCF.ServiceClient();
            return wcfNameService.GetProductName(product);
        }

        public char GetProductSort(string product)
        {
            NameService.WCF.IService wcfNameService = new NameService.WCF.ServiceClient();
            return wcfNameService.GetProductSort(product);
        }
    }
}
