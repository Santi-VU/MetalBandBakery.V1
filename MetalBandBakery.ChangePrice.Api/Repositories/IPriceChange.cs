namespace MetalBandBakery.ChangePrice.Api.Repositories
{
    public interface IPriceChange
    {
        void ModifyPrice(char product, decimal newPrice);
    }
}