using System;
using MetalBandBakery.Infra.Repository.DB;
using MetalBandBakery.Infra.Repository.HTTP;
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
        if (!DBService.ExistsIdInFile(product, DBService.stocksFile))
            return false;

        SoapStockService _wcfStock =  new SoapStockService();
        if (_wcfStock.ManyStock(product) > 1)
            return false;
        return true;
    }

    public void ReplaceProduct(char product)
    {
        if (!DBService.ExistsIdInFile(product, DBService.stocksFile))
            return;

        SoapStockService _wcfStock = new SoapStockService();
        _wcfStock.AddStock(product);
    }
}
