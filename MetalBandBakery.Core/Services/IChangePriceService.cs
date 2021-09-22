namespace MetalBandBakery.Core.Services
{
    public interface IChangePriceService
    {
        bool ModifyPrice(char product, decimal newPrice);
    }
}