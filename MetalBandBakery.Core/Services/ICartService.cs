using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.Core.Services
{
    public interface ICartService
    {
        List<Tuple<char, int>> Get();
        bool AddProduct(char product);
        bool RemoveProduct(char product);
        bool Pay(decimal payValue);
        decimal GetCurrentPayed();
        void RestartCart();
        int GetUnitsOfProduct(char product);
    }
}
