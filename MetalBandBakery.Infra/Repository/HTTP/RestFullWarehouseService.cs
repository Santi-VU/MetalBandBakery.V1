using MetalBandBakery.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MetalBandBakey.Infra.Repository
{
    public class RestFullWarehouseService : IWarehouseService
    {
        public List<Tuple<int, string>> Get()
        {
            string apiUrl = "https://localhost:44358/warehouse";

            using (WebClient client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;

                string json = client.DownloadString($"{apiUrl}");

                var itemPrice = JsonConvert.DeserializeObject<List<Tuple<int, string>>>(json);

                return itemPrice;
            }
        }
    }
}
