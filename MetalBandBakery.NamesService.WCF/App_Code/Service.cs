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
    public string GetProductName(char product)
    {
        return MarketingProducts._names[product];
    }

    public string[] GetAllProducts()
    {
        string[] names = new string[MarketingProducts._names.Count];
        int cont = 0;
        foreach (var i in MarketingProducts._names)
        {
            names[cont] = i.Value;
            cont++;
        }

        return names;
    }

    public char GetProductSort(string product)
    {
        return MarketingProducts._names.FirstOrDefault(p => p.Value == product).Key;
    }

    public char[] GetAllProductSorts()
    {
        char[] sorts = new char[MarketingProducts._names.Count];
        int cont = 0;
        foreach (var i in MarketingProducts._names)
        {
            sorts[cont] = i.Key;
            cont++;
        }

        return sorts;
    }
}
