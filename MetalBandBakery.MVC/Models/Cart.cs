using System;
using System.Collections.Generic;
using MetalBandBakery.Infra.Repository.HTTP;
using System.Linq;
using System.Web;
using MetalBandBakey.Infra.Repository;

namespace MetalBandBakery.MVC.Models
{
    public class Cart
    {
        public List<Tuple<char, int>> productList { get; set; }
        public decimal totalValue { get => GetTotalValueOfCart(); }
        public decimal totalPayed { get; set; }
        public bool isPayed { get => totalPayed >= totalValue; }

        public Cart()
        {
            productList = new List<Tuple<char, int>>();
            totalPayed = 0;
        }

        private bool ExistInCart(char product)
        {
            bool exist = false;
            foreach (var i in productList)
            {
                if (i.Item1 == product)
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        private int GetIndexOfProduct(char product)
        {
            int index = 0;
            foreach (var i in productList)
            {
                if (i.Item1 == product)
                    break;
                index++;
            }
            return index;
        }

        private decimal GetTotalValueOfCart()
        {
            RestfullPriceService restPriceService = new RestfullPriceService();
            decimal total = 0.00m;
            foreach(var i in productList)
            {
                total = total + (restPriceService.GetProductPrice(i.Item1) * i.Item2);
            }
            return total;
        }

        public void AddProduct(char product)
        {
            int auxStock = 0;
            if (!ExistInCart(product))
            {
                productList.Add(new Tuple<char, int>(product, 1));
            } else
            {
                int index = GetIndexOfProduct(product);
                if (index == -1)
                    return;
                auxStock = productList[index].Item2 + 1;
                productList[index] = new Tuple<char, int>(product, auxStock);
            }
        }

        public void RemoveProduct(char product)
        {
            if (!ExistInCart(product))
            {
                return;
            }

            int index = GetIndexOfProduct(product);
            if (index == -1)
                return;

            int auxStock = 0;
            if (productList[index].Item2 == 1)
            {
                productList.Remove(productList[index]);
            } else
            {
                auxStock = productList[index].Item2;
                productList[index] = new Tuple<char, int>(product, (auxStock-1));
            }
            
        }

        public List<Tuple<char, int>> GetCart()
        {
            return productList;
        }

        public void PayCart(decimal totalPay)
        {
            totalPayed += totalPay;
        }
    }
}