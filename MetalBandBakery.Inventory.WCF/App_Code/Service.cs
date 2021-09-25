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
        if (!DBService.ExistsProductInFile(product, DBService.stocksFile))
            return false;

        List<string> lines = DBService.ReadTextFromFile(DBService.stocksFile);
        int index = DBService.GetIndexOfText(product, lines);

        return Int32.Parse(lines[index].Split('=')[1]) > 0;
    }

    public bool RemoveStock(char product, int amount)
    {
        if (!DBService.ExistsProductInFile(product, DBService.stocksFile))
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
        if (!DBService.ExistsProductInFile(product, DBService.stocksFile))
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
        if (!DBService.ExistsProductInFile(product, DBService.stocksFile))
            return -1;

        List<string> lines = DBService.ReadTextFromFile(DBService.stocksFile);
        int index = DBService.GetIndexOfText(product, lines);

        return Int32.Parse(lines[index].Split('=')[1]);
    }

    public bool CanBeRemoved(char product, int amount)
    {
        if (!DBService.ExistsProductInFile(product, DBService.stocksFile))
            return false;

        List<string> lines = DBService.ReadTextFromFile(DBService.stocksFile);
        int index = DBService.GetIndexOfText(product, lines);

        return Int32.Parse(lines[index].Split('=')[1]) >= amount;
    }

    public bool AddStock(char product)
    {
        if (!DBService.ExistsProductInFile(product, DBService.stocksFile))
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
        if (!DBService.ExistsProductInFile(product, DBService.stocksFile))
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

        if (!DBService.ExistsProductInFile(product, DBService.stocksFile))
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
}
