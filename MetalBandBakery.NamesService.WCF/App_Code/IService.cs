using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{

    [OperationContract]
    string GetProductName(char product);

    [OperationContract]
    string[] GetAllProducts();

    [OperationContract]
    char GetProductSort(string product);

    [OperationContract]
    char[] GetAllProductSorts();
}
