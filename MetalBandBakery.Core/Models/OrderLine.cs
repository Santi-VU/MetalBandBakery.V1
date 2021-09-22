using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.Core.Models
{
    public class OrderLine
    {
		public OrderLine(string itemId)
		{
			ItemId = itemId;
			Amount = 1;
		}

		public int Amount { get; private set; }
		public decimal BasePrice { get; set; }
		public string ItemId { get; private set; }
		public decimal TotalPrice { get { return Amount * BasePrice; } }

		public void IncresaseAmount()
		{
			Amount++;
		}
	}
}
