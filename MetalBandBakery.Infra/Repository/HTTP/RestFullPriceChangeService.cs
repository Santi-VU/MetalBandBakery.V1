using MetalBandBakery.Core.Services;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MetalBandBakey.Infra.Repository
{
    public class RestFullPriceChangeService : IChangePriceService
    {
        public void ModifyPrice(char product, decimal newPrice)
        {
			string apiUrl = "https://localhost:44383/priceChange";

			using (WebClient client = new WebClient())
			{
				client.Headers["Content-type"] = "application/json";
				client.Encoding = Encoding.UTF8;

				string json = client.DownloadString($"/{product}/{newPrice}");

				var itemPrice = JsonConvert.DeserializeObject<decimal>(json);
			}
		}
    }
}
