using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using MetalBandBakery.Infra.Repository.DB;
using System.Linq;
using System.Threading.Tasks;
using MetalBandBakery.CartService.API.Repositories;

namespace MetalBandBakery.CartService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Tuple<char, int>> Get()
        {
            List<string> lines = DBService.ReadTextFromFile(DBService.cartFile);
            if (lines == null || lines.Count == 0)
                return null;

            List<Tuple<char, int>> cart = new List<Tuple<char, int>>();
            foreach (var i in lines)
            {
                string[] split = i.Split('=');
                cart.Add(new Tuple<char, int>(split[0][0], Int32.Parse(split[1])));
            }

            return cart;
        }

        [HttpGet("addProduct/{product}")]
        public bool AddProduct(char product)
        {
            if (!DBService.ExistsProductInFile(product, DBService.namesFile))
                return false;

            List<string> lines = DBService.ReadTextFromFile(DBService.cartFile);
            int index = DBService.GetIndexOfText(product, lines);
            int auxCant = 0;

            if (index == -1)
            {
                lines.Add($"{product.ToString()}=1");
            }
            else
            {
                auxCant = Int32.Parse(lines[index].Split('=')[1]);
                auxCant += 1;
                lines[index] = $"{product.ToString()}={auxCant}";
            }

            DBService.ReWriteFile(DBService.cartFile, lines);
            return true;
        }

        [HttpGet("removeProduct/{product}")]
        public bool RemoveProduct(char product)
        {
            if (!DBService.ExistsProductInFile(product, DBService.namesFile))
                return false;

            List<string> lines = DBService.ReadTextFromFile(DBService.cartFile);
            int index = DBService.GetIndexOfText(product, lines);
            if (index == -1)
                return false;

            int auxCant = Int32.Parse(lines[index].Split('=')[1]);

            if (auxCant == 1)
            {
                lines.Remove(lines[index]);
            }
            else
            {
                auxCant -= 1;
                lines[index] = $"{product.ToString()}={auxCant}";
            }

            DBService.ReWriteFile(DBService.cartFile, lines);
            return true;
        }

        [HttpGet("pay/{payValue}")]
        public bool Pay(decimal payValue)
        {
            if (payValue <= 0)
                return false;

            List<string> lines = DBService.ReadTextFromFile(DBService.cartValueFile);
            decimal currentPayed = Decimal.Parse(lines[0]);
            currentPayed += payValue;
            lines[0] = currentPayed.ToString();
            DBService.ReWriteFile(DBService.cartValueFile, lines);
            return true;
        }

        [HttpGet("getCurrPayed")]
        public decimal GetCurrentPayed()
        {
            List<string> lines = DBService.ReadTextFromFile(DBService.cartValueFile);
            return Decimal.Parse(lines[0]);
        }

        [HttpGet("restartCart")]
        public void RestartCart()
        {
            List<string> lines = new List<string>();
            DBService.ReWriteFile(DBService.cartFile, lines);

            lines.Add("0,00");
            DBService.ReWriteFile(DBService.cartValueFile, lines);
        }

        [HttpGet("getUnitsOfProduct/{product}")]
        public int GetUnitsOfProduct(char product)
        {
            List<string> lines = DBService.ReadTextFromFile(DBService.cartFile);

            if (!DBService.ExistsProductInFile(product, DBService.cartFile))
                return -1;

            int index = DBService.GetIndexOfText(product, lines);
            if (index == -1)
                return -1;

            return Int32.Parse(lines[index].Split('=')[1]);
        }
    }
}
