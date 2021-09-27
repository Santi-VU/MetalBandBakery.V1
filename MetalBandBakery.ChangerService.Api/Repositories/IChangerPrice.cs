namespace MetalBandBakery.ChangerService.Api.Repositories
{
    public interface IChangerPrice
    {
        bool ModifyPrice(char product, decimal newPrice);
    }
}