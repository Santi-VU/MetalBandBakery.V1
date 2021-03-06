using MetalBandBakery.Core.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MetalBandBakey.Infra.Repository
{
	public class RestfullPriceService : IPriceService
	{
        public List<decimal> GetAllPriceS()
        {
			string apiUrl = "https://localhost:44383/prices/getAllPrices";

			using (WebClient client = new WebClient())
			{
				client.Headers["Content-type"] = "application/json";
				client.Encoding = Encoding.UTF8;

				string json = client.DownloadString($"{apiUrl}");

				var itemPrice = JsonConvert.DeserializeObject<List<decimal>>(json);

				return itemPrice;
			}
		}

        public decimal GetProductPrice(char product)
        {
			string apiUrl = "https://localhost:44383/prices/getPrice";

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
			string apiUrl = "https://localhost:44383/prices/isEnough";

			string aux = totalBuy.ToString();
			if (aux.Contains(","))
				aux = aux.Replace(',', '.');

			using (WebClient client = new WebClient())
			{
				client.Headers["Content-type"] = "application/json";
				client.Encoding = Encoding.UTF8;

				string json = client.DownloadString($"{apiUrl}/{moneyForPay}/{aux}");

				var itsEnough = JsonConvert.DeserializeObject<bool>(json);

				return itsEnough;
			}
		}

        public bool ModifyPrice(char product, decimal newPrice)
        {
			string apiUrl = "https://localhost:44383/prices/setPrice";

			string aux = newPrice.ToString();
			if (aux.Contains(","))
				aux = aux.Replace(',', '.');

			using (WebClient client = new WebClient())
			{
				client.Headers["Content-type"] = "application/json";
				client.Encoding = Encoding.UTF8;

				string json = client.DownloadString($"{apiUrl}/{product}/{aux}");

				var priceChanged = JsonConvert.DeserializeObject<bool>(json);

				return priceChanged;
			}
		}
    }
}
