using MetalBandBakery.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MetalBandBakey.Infra.Repository
{
    public class RestFullCartService : ICartService
    {
        public bool AddProduct(char product)
        {
            string apiUrl = "https://localhost:44307/cart";

            using (WebClient client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;

                string json = client.DownloadString($"{apiUrl}/addProduct/{product}");

                var itemPrice = JsonConvert.DeserializeObject<bool>(json);

                return itemPrice;
            }
        }

        public List<Tuple<char, int>> Get()
        {
            string apiUrl = "https://localhost:44307/cart";

            using (WebClient client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;

                string json = client.DownloadString($"{apiUrl}");

                var itemPrice = JsonConvert.DeserializeObject<List<Tuple<char,int>>>(json);

                return itemPrice;
            }
        }

        public decimal GetCurrentPayed()
        {
            string apiUrl = "https://localhost:44307/cart";

            using (WebClient client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;

                string json = client.DownloadString($"{apiUrl}/getCurrPayed");

                var itemPrice = JsonConvert.DeserializeObject<decimal>(json);

                return itemPrice;
            }
        }

        public bool Pay(decimal payValue)
        {
            string apiUrl = "https://localhost:44307/cart";

            string aux = "";
            if (payValue.ToString().Contains(","))
                aux = payValue.ToString().Replace(',', '.');

            using (WebClient client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;

                string json = client.DownloadString($"{apiUrl}/pay/{aux}");

                var itemPrice = JsonConvert.DeserializeObject<bool>(json);

                return itemPrice;
            }
        }

        public bool RemoveProduct(char product)
        {
            string apiUrl = "https://localhost:44307/cart";

            using (WebClient client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;

                string json = client.DownloadString($"{apiUrl}/removeProduct/{product}");

                var itemPrice = JsonConvert.DeserializeObject<bool>(json);

                return itemPrice;
            }
        }

        public void RestartCart()
        {
            string apiUrl = "https://localhost:44307/cart";

            using (WebClient client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;

                string json = client.DownloadString($"{apiUrl}/restartCart");
            }
        }
    }
}
