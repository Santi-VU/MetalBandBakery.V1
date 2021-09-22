namespace MetalBandBakery.Core.Services
{
    public interface IReplaceStockService
    {
        bool NeedToBeReplace(char product);
        void ReplaceProduct(char product);
    }
}