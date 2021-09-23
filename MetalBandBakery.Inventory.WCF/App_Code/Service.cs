using MetalBandBakery.InventoryWCF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    public bool CheckStock(char product)
    {
        if (!InventoryProduct._stock.ContainsKey(product))
            return false;

        return InventoryProduct._stock[product] > 0;
    }

    public bool RemoveStock(char product, int amount)
    {
        if (!InventoryProduct._stock.ContainsKey(product))
            return false;

        if (InventoryProduct._stock[product] <= 0)
            return false;

        InventoryProduct._stock[product] -= amount;
        return true;
    }

    public int ManyStock(char product)
    {
        if (!InventoryProduct._stock.ContainsKey(product))
            return -1;

        return InventoryProduct._stock[product];
    }

    public bool CanBeRemoved(char product, int amount)
    {
        return InventoryProduct._stock[product] >= amount;
    }

    public bool AddStock(char product)
    {
        if (!InventoryProduct._stock.ContainsKey(product))
            return false;

        if (InventoryProduct._stock[product] == 0)
            InventoryProduct._stock[product] = 5;
        else
            InventoryProduct._stock[product] = ((5 - InventoryProduct._stock[product]) + InventoryProduct._stock[product]);
        return true;
    }

    public int[] GetStocks()
    {
        int[] stocks = new int[InventoryProduct._stock.Count];
        int cont = 0;
        foreach (var i in InventoryProduct._stock)
        {
            stocks[cont] = i.Value;
            cont++;
        }
        return stocks;
    }

    public string[] GetShorts()
    {
        string[] shorts = new string[InventoryProduct._stock.Count];
        int cont = 0;
        foreach (var i in InventoryProduct._stock)
        {
            shorts[cont] = i.Key.ToString();
            cont++;
        }
        return shorts;
    }

    public bool AddStockWithQuantity(char product, int quantity)
    {
        if (!InventoryProduct._stock.ContainsKey(product))
            return false;

        if (quantity <= 0)
            return false;

        InventoryProduct._stock[product] = quantity;
        return true;
    }
}
