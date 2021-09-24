public interface IMarketingProducts
{
    string[] GetAllProducts();
    string GetProductName(char product);
    char GetProductSort(string product);
    char[] GetAllProductSorts();
}