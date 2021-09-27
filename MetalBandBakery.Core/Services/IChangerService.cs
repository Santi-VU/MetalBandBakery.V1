using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.Core.Services
{
    public interface IChangerService
    {
        decimal GetMatsPriceOf(char product);
        bool RemoveMatOf(char product, string mat);
        bool AddMatOf(char product, string mat);
        List<Tuple<string, decimal>> GetListOfProduct(char product);
        List<Tuple<string, decimal>> GetAllMaterials();
        void ModifiyMatPrice(string material, decimal newPrice);
    }
}
