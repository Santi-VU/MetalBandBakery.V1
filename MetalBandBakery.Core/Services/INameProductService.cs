using System.Collections.Generic;

public interface IMarketingProducts
{
    List<string> GetAllProducts();
    string GetProductName(char product);
    char GetProductSort(string product);
    List<char> GetAllProductSorts();
}