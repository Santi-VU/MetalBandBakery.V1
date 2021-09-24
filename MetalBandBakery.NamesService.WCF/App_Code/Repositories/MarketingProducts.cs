using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MarketingProducts
/// </summary>
public class MarketingProducts : IMarketingProducts
{
    public static Dictionary<char, string> _names = new Dictionary<char, string>()
        {
            {'B', "Brownie" },
            {'M', "Muffin" },
            {'C', "Cake" },
            {'W', "Water" }
        };

    public string GetProductName(char product)
    {
        return _names[product];
    }

    public char GetProductSort(string product)
    {
        return _names.FirstOrDefault(p => p.Value == product).Key;
    }

    public char[] GetAllProductSorts()
    {
        char[] sorts = new char[_names.Count];
        int cont = 0;
        foreach (var i in _names)
        {
            sorts[cont] = i.Key;
            cont++;
        }

        return sorts;
    }

    public string[] GetAllProducts()
    {
        string[] names = new string[_names.Count];
        int cont = 0;
        foreach (var i in _names)
        {
            names[cont] = i.Value;
            cont++;
        }

        return names;
    }
}