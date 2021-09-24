using MetalBandBakery.Core.Services;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;

namespace MetalBandBakey.Infra.Repository
{
    public class RestFullChangerService : IChangerService
    {
        public bool ModifyPrice(char product, decimal newPrice)
        {
			string apiUrl = "https://localhost:44317/changerPrice";

			string aux = newPrice.ToString();
			if (aux.Contains(","))
				aux = aux.Replace(',', '.');

			using (WebClient client = new WebClient())
			{
				client.Headers["Content-type"] = "application/json";
				client.Encoding = Encoding.UTF8;

				string json = client.DownloadString($"{apiUrl}/{product}/{aux}");

				var itemPrice = JsonConvert.DeserializeObject<bool>(json);

				return itemPrice;
			}
		}
    }
}
