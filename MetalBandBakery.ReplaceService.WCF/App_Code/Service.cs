using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    public Service() {
    }
    public bool NeedToBeReplace(char product)
    {
        MetalBandBakery.Infra.Repository.HTTP.SoapStockService _wcfStock =  new MetalBandBakery.Infra.Repository.HTTP.SoapStockService();
        if (_wcfStock.ManyStock(product) > 1)
            return false;
        return true;
    }

    public void ReplaceProduct(char product)
    {
        MetalBandBakery.Infra.Repository.HTTP.SoapStockService _wcfStock = new MetalBandBakery.Infra.Repository.HTTP.SoapStockService();
        _wcfStock.AddStock(product);
    }
}
