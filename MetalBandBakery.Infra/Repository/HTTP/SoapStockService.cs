using System;
using MetalBandBakery.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.Infra.Repository.HTTP
{
    public class SoapStockService : IStockService
    {
        public bool CheckStock(char product)
        {
            Inventory.WCF.IService wcfService = new Inventory.WCF.ServiceClient();
            return wcfService.CheckStock(product);
        }

        public int ManyStock(char product)
        {
            Inventory.WCF.IService wcfService = new Inventory.WCF.ServiceClient();
            return wcfService.ManyStock(product);
        }

        public void RemoveStock(char product, int amount)
        {
            Inventory.WCF.IService wcfService = new Inventory.WCF.ServiceClient();
            wcfService.RemoveStock(product, amount);
        }

        public bool CanBeRemoved(char product, int amount)
        {
            Inventory.WCF.IService wcfService = new Inventory.WCF.ServiceClient();
            return wcfService.CanBeRemoved(product, amount);
        }

        public void AddStock(char product)
        {
            Inventory.WCF.IService wcfService = new Inventory.WCF.ServiceClient();
            wcfService.AddStock(product);
        }

        public bool AddStockWithQuantity(char product, int quantity)
        {
            Inventory.WCF.IService wcfService = new Inventory.WCF.ServiceClient();
            return wcfService.AddStockWithQuantity(product, quantity);
        }

        public List<int> GetStocks()
        {
            Inventory.WCF.IService wcfService = new Inventory.WCF.ServiceClient();
            List<int> list = new List<int>();

            foreach (var i in wcfService.GetStocks()) {
                list.Add(i);
            }
            return list;
        }

        public bool RemoveStockUnit(char product)
        {
            Inventory.WCF.IService wcfService = new Inventory.WCF.ServiceClient();
            return wcfService.RemoveStockUnit(product);
        }

        public void AddStockUnit(char product)
        {
            Inventory.WCF.IService wcfService = new Inventory.WCF.ServiceClient();
            wcfService.AddStockUnit(product);
        }

        public List<Tuple<char, int>> ManyStockOfWarehouse(string warehouse)
        {
            Inventory.WCF.IService wcfService = new Inventory.WCF.ServiceClient();
            List<Tuple<char, int>> list = new List<Tuple<char, int>>();
            foreach (var i in wcfService.ManyStockOfWarehouse(warehouse))
            {
                list.Add(i);
            }

            return list;
        }

        public bool CanBenRemovedMaster(string warehouse, char product, int quantity)
        {
            Inventory.WCF.IService wcfService = new Inventory.WCF.ServiceClient();
            return wcfService.CanBenRemovedMaster(warehouse, product, quantity);
        }

        public bool RemoveStockMaster(string warehouse, char product, int quantity)
        {
            Inventory.WCF.IService wcfService = new Inventory.WCF.ServiceClient();
            return wcfService.RemoveStockMaster(warehouse, product, quantity);
        }

        public bool AddStockMaster(string warehouse, char product, int quantity)
        {
            Inventory.WCF.IService wcfService = new Inventory.WCF.ServiceClient();
            return wcfService.AddStockMaster(warehouse, product, quantity);
        }

        public int ManyStockOfWarehouseProduct(string warehouse, char product)
        {
            Inventory.WCF.IService wcfService = new Inventory.WCF.ServiceClient();
            return wcfService.ManyStockOfWarehouseProduct(warehouse, product);
        }
    }
}
