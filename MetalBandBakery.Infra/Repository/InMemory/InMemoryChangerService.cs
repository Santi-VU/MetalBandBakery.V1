using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.Core.Services
{
    public class InMemoryChangerService : IChangerService
    {
        public bool AddMatOf(char product, string mat)
        {
            throw new NotImplementedException();
        }

        public List<Tuple<string, decimal>> GetAllMaterials()
        {
            throw new NotImplementedException();
        }

        public List<Tuple<string, decimal>> GetListOfProduct(char product)
        {
            throw new NotImplementedException();
        }

        public decimal GetMatsPriceOf(char product)
        {
            throw new NotImplementedException();
        }

        public void ModifiyMatPrice(string material, decimal newPrice)
        {
            throw new NotImplementedException();
        }

        public bool RemoveMatOf(char product, string mat)
        {
            throw new NotImplementedException();
        }
    }
}
