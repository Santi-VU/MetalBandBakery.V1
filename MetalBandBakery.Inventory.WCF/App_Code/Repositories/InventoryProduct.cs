using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.InventoryWCF.Repositories
{
    public class InventoryProduct
    {
        public static Dictionary<char, int> _stock = new Dictionary<char, int>()
        {
            {'B', 5 },
            {'M', 5 },
            {'C', 5 },
            {'W', 5 },
        };
    }
}
