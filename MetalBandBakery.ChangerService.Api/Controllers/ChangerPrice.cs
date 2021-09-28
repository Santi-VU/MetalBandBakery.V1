using MetalBandBakery.ChangerService.Api.Repositories;
using MetalBandBakery.Core.Services;
using MetalBandBakery.Infra.Repository.DB;
using MetalBandBakey.Infra.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.ChangerService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChangerPrice : ControllerBase
    {
        private readonly ILogger<ChangerPrice> _logger;

        public ChangerPrice(ILogger<ChangerPrice> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Tuple<char, decimal>> Get()
        {
            List<Tuple<char, decimal>> list = new List<Tuple<char, decimal>>();
            foreach (var i in Repositories.ChangerPrice._prices)
            {
                list.Add(new Tuple<char, decimal>(i.Key, i.Value));
            }
            return list;
        }

        [HttpGet("getMatsPriceOf/{product}")]
        public decimal GetMatsPriceOf(char product)
        {
            if (!DBService.ExistsIdInFile(product, DBService.productMatsFile))
                return -1;
            List<string> productMats = DBService.ReadTextFromFile(DBService.productMatsFile);
            int index = DBService.GetIndexOfText(product, productMats);
            if (index == -1)
                return -1;

            string[] currMats = productMats[index].Split('=')[1].Split(',');

            if (currMats == null || currMats.Length == 0)
                return -1;

            decimal totalPrice = 0.00m;
            List<string> prices = DBService.ReadTextFromFile(DBService.materialsFile);
            int j = -1;
            foreach (var i in currMats)
            {
                j = DBService.GetIndexOfText(i, prices);
                if (j == -1)
                    continue;
                totalPrice += Decimal.Parse(prices[j].Split('=')[1]);
            }

            return totalPrice;
        }

        [HttpGet("removeMatOf/{product}/{mat}")]
        public bool RemoveMatOf(char product, string mat)
        {
            if (!DBService.ExistsIdInFile(product, DBService.productMatsFile))
                return false;
            List<string> productsMats = DBService.ReadTextFromFile(DBService.productMatsFile);
            int index = DBService.GetIndexOfText(product, productsMats);
            if (index == -1)
                return false;

            string[] currMats = productsMats[index].Split('=')[1].Split(',');
            bool firstOcurrence = false;
            StringBuilder sb = new StringBuilder();
            foreach (var i in currMats)
            {
                if (i == mat && firstOcurrence == false)
                {
                    firstOcurrence = true;
                    continue;
                }

                sb.Append(i + ",");
            }
            string aux = sb.ToString().Substring(0, sb.ToString().Length - 1);

            productsMats[index] = product.ToString() + "=" + aux;
            DBService.ReWriteFile(DBService.productMatsFile, productsMats);
            
            return true;
        }

        [HttpGet("addMatOf/{product}/{mat}")]
        public bool AddMatOf(char product, string mat)
        {
            if (!DBService.ExistsIdInFile(product, DBService.productMatsFile))
                return false;
            List<string> productsMats = DBService.ReadTextFromFile(DBService.productMatsFile);

            int index = DBService.GetIndexOfText(product, productsMats);
            if (index == -1)
                return false;

            string currProducts = productsMats[index].Split('=')[1];
            currProducts = currProducts + "," + mat;
            productsMats[index] = product.ToString() + "=" + currProducts;

            DBService.ReWriteFile(DBService.productMatsFile, productsMats);
            return true;
        }

        [HttpGet("getListOfProduct/{product}")]
        public List<Tuple<string, decimal>> GetListOfProduct(char product)
        {
            if (!DBService.ExistsIdInFile(product, DBService.productMatsFile))
                return null;

            List<string> productsMats = DBService.ReadTextFromFile(DBService.productMatsFile);
            List<string> pricesMats = DBService.ReadTextFromFile(DBService.materialsFile);
            List<Tuple<string, decimal>> list = new List<Tuple<string, decimal>>();

            int index = DBService.GetIndexOfText(product, productsMats);
            if (index == -1)
                return null;
            string[] currMats = productsMats[index].Split('=')[1].Split(',');

            if (currMats == null || currMats.Length == 0)
                return null;

            int indexPrice = -1;
            foreach (var i in currMats){
                indexPrice = DBService.GetIndexOfText(i, pricesMats);
                if (indexPrice == -1)
                    continue;
                list.Add(new Tuple<string, decimal>(i, Decimal.Parse(pricesMats[indexPrice].Split('=')[1])));
            }

            return list;
        }

        [HttpGet("getAllMaterials")]
        public List<Tuple<string, decimal>> GetAllMaterials()
        {
            List<Tuple<string, decimal>> list = new List<Tuple<string, decimal>>();
            List<string> lines = DBService.ReadTextFromFile(DBService.materialsFile);

            string[] aux = null;
            foreach(var i in lines)
            {
                aux = i.Split('=');
                list.Add(new Tuple<string, decimal>(aux[0], Decimal.Parse(aux[1])));
            }

            return list;
        }

        [HttpGet("modifiyMatPrice/{material}/{newPrice}")]
        public void ModifiyMatPrice(string material, decimal newPrice)
        {
            List<string> lines = DBService.ReadTextFromFile(DBService.materialsFile);

            int index = DBService.GetIndexOfText(material, lines);
            if (index == -1)
                return;

            lines[index] = material + "=" + newPrice;
            DBService.ReWriteFile(DBService.materialsFile, lines);
        }
    }
}
