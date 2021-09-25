using System;
using System.Collections.Generic;
using System.Linq;
using MetalBandBakery.Infra.Repository.DB;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    public string GetProductName(char product)
    {
        if (!DBService.ExistsProductInFile(product, DBService.namesFile))
            return "";

        List<string> lines = DBService.ReadTextFromFile(DBService.namesFile);
        int index = DBService.GetIndexOfText(product, lines);

        return lines[index].Split('=')[1];
    }

    public List<string> GetAllProducts()
    {
        List<string> names = new List<string>();
        foreach(var i in DBService.ReadTextFromFile(DBService.namesFile))
        {
            names.Add(i.Split('=')[1]);
        }
        return names;
    }

    public char GetProductSort(string product)
    {
        if (!DBService.ExistsProductInFile(product[0], DBService.namesFile))
            return ' ';

        List<string> lines = DBService.ReadTextFromFile(DBService.namesFile);
        int index = DBService.GetIndexOfText(product[0], lines);
        return lines[index].Split('=')[0][0];
    }

    public List<char> GetAllProductSorts()
    {
        List<char> sorts = new List<char>();
        foreach (var i in DBService.ReadTextFromFile(DBService.namesFile))
        {
            sorts.Add(i.Split('=')[0][0]);
        }
        return sorts;
    }
}
