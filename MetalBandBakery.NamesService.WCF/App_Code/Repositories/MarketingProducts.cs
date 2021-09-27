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

    public List<char> GetAllProductSorts()
    {
        List<char> list = new List<char>();
        foreach (var i in _names)
        {
            list.Add(i.Key);
        }

        return list;
    }

    public List<string> GetAllProducts()
    {
        List<string> names = new List<string>();
        foreach (var i in _names)
        {
            names.Add(i.Value);
        }
        return names;
    }
}