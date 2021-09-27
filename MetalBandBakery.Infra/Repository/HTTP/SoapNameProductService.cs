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
        public List<string> GetAllProducts()
        {
            NameService.WCF.IService wcfNameService = new NameService.WCF.ServiceClient();
            List<string> names = new List<string>();
            foreach (var i in wcfNameService.GetAllProducts())
            {
                names.Add(i);
            }
            return names;
        }

        public List<char> GetAllProductSorts()
        {
            NameService.WCF.IService wcfNameService = new NameService.WCF.ServiceClient();
            List<char> sorts = new List<char>();
            foreach (var i in wcfNameService.GetAllProductSorts())
            {
                sorts.Add(i);
            }
            return sorts;
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
