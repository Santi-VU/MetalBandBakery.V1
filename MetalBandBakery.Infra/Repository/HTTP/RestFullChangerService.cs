using MetalBandBakery.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MetalBandBakey.Infra.Repository
{
    public class RestFullChangerService : IChangerService
    {
        public bool AddMatOf(char product, string mat)
        {
            string apiUrl = "https://localhost:44317/changerPrice";

            using (WebClient client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;

                string json = client.DownloadString($"{apiUrl}/addMatOf/{product}/{mat}");

                var itemPrice = JsonConvert.DeserializeObject<bool>(json);

                return itemPrice;
            }
        }

        public List<Tuple<string, decimal>> GetListOfProduct(char product)
        {
            string apiUrl = "https://localhost:44317/changerPrice";

            using (WebClient client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;

                string json = client.DownloadString($"{apiUrl}/getListOfProduct/{product}");

                var itemPrice = JsonConvert.DeserializeObject<List<Tuple<string, decimal>>>(json);

                return itemPrice;
            }
        }

        public decimal GetMatsPriceOf(char product)
        {
            string apiUrl = "https://localhost:44317/changerPrice";

            using (WebClient client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;

                string json = client.DownloadString($"{apiUrl}/GetMatsPriceOf/{product}");

                var itemPrice = JsonConvert.DeserializeObject<decimal>(json);

                return itemPrice;
            }
        }

        public bool RemoveMatOf(char product, string mat)
        {
            string apiUrl = "https://localhost:44317/changerPrice";

            using (WebClient client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;

                string json = client.DownloadString($"{apiUrl}/removeMatOf/{product}/{mat}");

                var itemPrice = JsonConvert.DeserializeObject<bool>(json);

                return itemPrice;
            }
        }
    }
}
