using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.Core.Models
{
    public class Order
    {
		private List<OrderLine> _listOfItems;

		public Order()
		{
			_listOfItems = new List<OrderLine>();
		}

		public decimal AmountToPay { get { return _listOfItems.Sum(t => t.TotalPrice); } }

		public IEnumerable<OrderLine> OrderLines { get { return _listOfItems; } }

		public void AddItems(string[] itemIds)
		{
			foreach (var itemId in itemIds)
			{
				var ol = _listOfItems.FirstOrDefault(l => l.ItemId == itemId);
				if (ol == null)
					_listOfItems.Add(new OrderLine(itemId));
				else
					ol.IncresaseAmount();
			}
		}

		public bool CanBePurchase()
		{
			return AmountToPay > 0;
		}

		public void SetPrice(string itemId, decimal itemPrice)
		{
			var item = _listOfItems.FirstOrDefault(li => li.ItemId == itemId);
			item.BasePrice = itemPrice;
		}
	}
}
