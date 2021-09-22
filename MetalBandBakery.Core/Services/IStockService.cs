namespace MetalBandBakery.Core.Services
{
    public interface IStockService
    {
        bool CheckStock(char product);
        void RemoveStock(char product, int amount);
        int ManyStock(char product);
        bool CanBeRemoved(char product, int amount);
        void AddStock(char product);
    }
}