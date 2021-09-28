using MetalBandBakery.InventoryWCF.Repositories;
using MetalBandBakery.Infra.Repository.DB;
using System;
using System.Collections.Generic;
using System.IO;
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
        if (!DBService.ExistsIdInFile(product, DBService.stocksFile))
            return false;

        List<string> lines = DBService.ReadTextFromFile(DBService.stocksFile);
        int index = DBService.GetIndexOfText(product, lines);

        return Int32.Parse(lines[index].Split('=')[1]) > 0;
    }

    public bool RemoveStock(char product, int amount)
    {
        if (!DBService.ExistsIdInFile(product, DBService.stocksFile))
            return false;

        List<string> lines = DBService.ReadTextFromFile(DBService.stocksFile);
        int index = DBService.GetIndexOfText(product, lines);

        int currentStock = Int32.Parse(lines[index].Split('=')[1]);

        if (currentStock <= 0)
            return false;

        lines[index] = product.ToString() + "=" + Int32.Parse((currentStock - amount).ToString());
        DBService.ReWriteFile(DBService.stocksFile, lines);
        return true;
    }

    public bool RemoveStockUnit(char product)
    {
        if (!DBService.ExistsIdInFile(product, DBService.stocksFile))
            return false;

        List<string> lines = DBService.ReadTextFromFile(DBService.stocksFile);
        int index = DBService.GetIndexOfText(product, lines);

        int currentStock = Int32.Parse(lines[index].Split('=')[1]);

        if (currentStock <= 0)
            return false;

        lines[index] = product.ToString() + "=" + Int32.Parse((currentStock - 1).ToString());
        DBService.ReWriteFile(DBService.stocksFile, lines);
        return true;
    }

    public int ManyStock(char product)
    {
        if (!DBService.ExistsIdInFile(product, DBService.stocksFile))
            return -1;

        List<string> lines = DBService.ReadTextFromFile(DBService.stocksFile);
        int index = DBService.GetIndexOfText(product, lines);

        return Int32.Parse(lines[index].Split('=')[1]);
    }

    public bool CanBeRemoved(char product, int amount)
    {
        if (!DBService.ExistsIdInFile(product, DBService.stocksFile))
            return false;

        List<string> lines = DBService.ReadTextFromFile(DBService.stocksFile);
        int index = DBService.GetIndexOfText(product, lines);

        return Int32.Parse(lines[index].Split('=')[1]) >= amount;
    }

    public bool AddStock(char product)
    {
        if (!DBService.ExistsIdInFile(product, DBService.stocksFile))
            return false;

        List<string> lines = DBService.ReadTextFromFile(DBService.stocksFile);
        int index = DBService.GetIndexOfText(product, lines);

        int currentStock = Int32.Parse(lines[index].Split('=')[1]);
        int aux = 0;

        if (currentStock == 0)
            lines[index] = product.ToString() + "=" + 5;
        else
            aux = 5 - currentStock;
            InventoryProduct._stock[product] = ((5 - InventoryProduct._stock[product]) + InventoryProduct._stock[product]);
        lines[index] = product.ToString() + "=" + Int32.Parse((aux + currentStock).ToString());

        DBService.ReWriteFile(DBService.stocksFile, lines);
        return true;
    }

    public List<int> GetStocks()
    {
        List<int> stocks = new List<int>();
        foreach(var i in DBService.ReadTextFromFile(DBService.stocksFile))
        {
            stocks.Add(Int32.Parse(i.Split('=')[1]));
        }
        return stocks;
    }

    public bool AddStockWithQuantity(char product, int quantity)
    {
        if (!DBService.ExistsIdInFile(product, DBService.stocksFile))
            return false;

        if (quantity <= 0)
            return false;

        List<string> lines = DBService.ReadTextFromFile(DBService.stocksFile);
        int index = DBService.GetIndexOfText(product, lines);

        int auxStock = Int32.Parse(lines[index].Split('=')[1]);
        lines[index] = product.ToString() + "=" + Int32.Parse((auxStock + quantity).ToString());

        DBService.ReWriteFile(DBService.stocksFile, lines);
        return true;
    }

    public void AddStockUnit(char product)
    {
        List<string> lines = DBService.ReadTextFromFile(DBService.stocksFile);
        int index = -1;
        int currentStock = 0;

        if (!DBService.ExistsIdInFile(product, DBService.stocksFile))
        {
            lines.Add(product.ToString() + "=1");
        } else {
            index = DBService.GetIndexOfText(product, lines);
            if (index == -1)
                return;
            currentStock = Int32.Parse(lines[index].Split('=')[1]);
            currentStock += 1;
            lines[index] = product.ToString() + "=" + currentStock;
        }
        DBService.ReWriteFile(DBService.stocksFile, lines);
    }
    //
    //
    public List<Tuple<char, int>> ManyStockOfWarehouse(string warehouse)
    {
        if (!DBService.ExistsValueInFile(warehouse, DBService.warehousesFile))
            return null;

        List<string> lines = DBService.ReadTextFromFile(DBService.stocksMasterFile);
        if (lines == null || lines.Count == 0)
            return null;

        List<Tuple<char, int>> stock = new List<Tuple<char, int>>();
        string[] aux = null;
        foreach (var i in lines)
        {
            aux = i.Split('=');
            if (aux[0] != warehouse)
                continue;
            stock.Add(new Tuple<char, int>(aux[1][0], Int32.Parse(aux[2])));
        }

        return stock;
    }

    public bool CanBenRemovedMaster(string warehouse, char product, int quantity)
    {
        if (!DBService.ExistsValueInFile(warehouse, DBService.warehousesFile))
            return false;

        if (!DBService.ExistsValueInFile(product.ToString(), DBService.stocksMasterFile))
            return false;

        if (quantity <= 0)
            return false;

        int auxStock = 0;
        string[] aux = null;
        List<string> lines = DBService.ReadTextFromFile(DBService.stocksMasterFile);

        foreach(var i in lines)
        {
            aux = i.Split('=');
            if (aux[0] != warehouse)
                continue;
            if (aux[1] != product.ToString())
                continue;
            auxStock = Int32.Parse(aux[2]);
        }

        if (auxStock <= 0)
            return false;

        return true;
    }

    public bool RemoveStockMaster(string warehouse, char product, int quantity)
    {
        if (!DBService.ExistsValueInFile(warehouse, DBService.warehousesFile))
            return false;

        if (!DBService.ExistsValueInFile(product.ToString(), DBService.stocksMasterFile))
            return false;

        if (quantity <= 0)
            return false;

        List<string> lines = DBService.ReadTextFromFile(DBService.stocksMasterFile);
        string[] aux = null;
        int auxStock = 0;
        int index = 0;

        foreach (var i in lines)
        {
            aux = i.Split('=');
            if (aux[0] != warehouse)
            {
                index++;
                continue;
            }
            if (aux[1] != product.ToString())
            {
                index++;
                continue;
            }
            auxStock = Int32.Parse(aux[2]);
            break;
        }

        if (auxStock == 0)
            return false;

        auxStock -= quantity;
        lines[index] = warehouse + "=" + product.ToString() + "=" + auxStock.ToString();
        DBService.ReWriteFile(DBService.stocksMasterFile, lines);

        return true;
    }

    public bool AddStockMaster(string warehouse, char product, int quantity)
    {
        if (!DBService.ExistsValueInFile(warehouse, DBService.warehousesFile))
            return false;

        if (!DBService.ExistsValueInFile(product.ToString(), DBService.stocksMasterFile))
            return false;

        if (quantity <= 0)
            return false;

        List<string> lines = DBService.ReadTextFromFile(DBService.stocksMasterFile);
        string[] aux = null;
        int auxStock = 0;
        int index = 0;

        foreach (var i in lines)
        {
            aux = i.Split('=');
            if (aux[0] != warehouse)
            {
                index++;
                continue;
            }
            if (aux[1] != product.ToString())
            {
                index++;
                continue;
            }
            auxStock = Int32.Parse(aux[2]);
            break;
        }

        auxStock += quantity;
        lines[index] = warehouse + "=" + product.ToString() + "=" + auxStock.ToString();
        DBService.ReWriteFile(DBService.stocksMasterFile, lines);

        return true;
    }

    public int ManyStockOfWarehouseProduct(string warehouse, char product)
    {
        if (!DBService.ExistsValueInFile(warehouse, DBService.warehousesFile))
            return -1;

        List<string> lines = DBService.ReadTextFromFile(DBService.stocksMasterFile);
        if (lines == null || lines.Count == 0)
            return -1;

        int stock = 0;
        string[] aux = null;
        foreach (var i in lines)
        {
            aux = i.Split('=');
            if (aux[0] != warehouse)
                continue;
            if (aux[1] != product.ToString())
                continue;
            stock = Int32.Parse(aux[2]);
        }

        return stock;
    }
}
