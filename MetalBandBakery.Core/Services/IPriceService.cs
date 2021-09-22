namespace MetalBandBakery.Core.Services
{
    public interface IPriceService
    {
        decimal GetProductPrice(char product);
        bool ItIsEnoughtMoney(decimal moneyForPay, decimal totalBuy);
        bool ModifyPrice(char product, decimal newPrice);
    }
}