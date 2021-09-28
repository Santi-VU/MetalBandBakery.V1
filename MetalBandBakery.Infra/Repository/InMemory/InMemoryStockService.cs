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

        public bool AddStockMaster(string warehouse, char product, int quantity)
        {
            throw new NotImplementedException();
        }

        public void AddStockUnit(char product)
        {
            throw new NotImplementedException();
        }

        public bool AddStockWithQuantity(char product, int quantity)
        {
            throw new NotImplementedException();
        }

        public bool CanBenRemovedMaster(string warehouse, char product, int quantity)
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

        public List<int> GetStocks()
        {
            throw new NotImplementedException();
        }

        public int ManyStock(char product)
        {
            throw new NotImplementedException();
        }

        public List<Tuple<char, int>> ManyStockOfWarehouse(string warehouse)
        {
            throw new NotImplementedException();
        }

        public int ManyStockOfWarehouseProduct(string warehouse, char product)
        {
            throw new NotImplementedException();
        }

        public void RemoveStock(char product, int amount)
        {
            throw new NotImplementedException();
        }

        public bool RemoveStockMaster(string warehouse, char product, int quantity)
        {
            throw new NotImplementedException();
        }

        public bool RemoveStockUnit(char product)
        {
            throw new NotImplementedException();
        }
    }
}
