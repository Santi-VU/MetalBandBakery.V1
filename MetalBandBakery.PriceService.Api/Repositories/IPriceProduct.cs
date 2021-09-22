namespace MetalBandBakery.PriceService.Api.Repositories
{
    public interface IPriceProduct
    {
        decimal GetProductPrice(char product);
        bool ItIsEnoughtMoney(decimal moneyForPay, decimal totalBuy);
        bool ModifyPrice(char product, decimal newPrice);
    }
}