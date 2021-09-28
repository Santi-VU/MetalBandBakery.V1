using System;
using System.Collections.Generic;

namespace MetalBandBakery.Core.Services
{
    public interface IStockService
    {
        bool CheckStock(char product);
        void RemoveStock(char product, int amount);
        int ManyStock(char product);
        bool CanBeRemoved(char product, int amount);
        void AddStock(char product);
        bool AddStockWithQuantity(char product, int quantity);
        List<int> GetStocks();
        bool RemoveStockUnit(char product);
        void AddStockUnit(char product);
        List<Tuple<char, int>> ManyStockOfWarehouse(string warehouse);
        bool CanBenRemovedMaster(string warehouse, char product, int quantity);
        bool RemoveStockMaster(string warehouse, char product, int quantity);
        bool AddStockMaster(string warehouse, char product, int quantity);
        int ManyStockOfWarehouseProduct(string warehouse, char product);
    }
}