using System;
using System.Collections.Generic;
using MetalBandBakery.Core.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.Infra.Repository.InMemory
{
    class InMemoryStockService : IStockService
    {
        public static Dictionary<char, int> _stock = new Dictionary<char, int>()
        {
            {'B', 5 },
            {'M', 5 },
            {'C', 5 },
            {'W', 5 },
        };

        public void AddStock(char product)
        {
            throw new NotImplementedException();
        }

        public bool CanBeRemoved(char product, int amount)
        {
            throw new NotImplementedException();
        }

        public bool CheckStock(char product)
        {
            throw new NotImplementedException();
        }

        public int ManyStock(char product)
        {
            throw new NotImplementedException();
        }

        public void RemoveStock(char product, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
