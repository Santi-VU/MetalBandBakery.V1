using MetalBandBakery.Core.Services;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MetalBandBakey.Infra.Repository
{
	public class RestfullPriceService : IPriceService
	{
        public decimal GetProductPrice(char product)
        {
			string apiUrl = "https://localhost:44383/prices";

			using (WebClient client = new WebClient())
			{
				client.Headers["Content-type"] = "application/json";
				client.Encoding = Encoding.UTF8;

				string json = client.DownloadString($"{apiUrl}/{product}");

				var itemPrice = JsonConvert.DeserializeObject<decimal>(json);

				return itemPrice;
			}
		}

        public bool ItIsEnoughtMoney(decimal moneyForPay, decimal totalBuy)
        {
			string apiUrl = "https://localhost:44383/prices";

			using (WebClient client = new WebClient())
			{
				client.Headers["Content-type"] = "application/json";
				client.Encoding = Encoding.UTF8;

				string json = client.DownloadString($"{apiUrl}/{moneyForPay}/{totalBuy}");

				var itsEnough = JsonConvert.DeserializeObject<bool>(json);

				return itsEnough;
			}
		}
    }
}
