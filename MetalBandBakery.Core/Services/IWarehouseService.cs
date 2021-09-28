using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.Core.Services
{
    public interface IWarehouseService
    {
        List<Tuple<int, string>> Get();
    }
}
