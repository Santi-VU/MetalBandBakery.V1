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
	bool CheckStock(char product);

	[OperationContract]
	bool RemoveStock(char product, int amount);

	[OperationContract]
	int ManyStock(char product);

	[OperationContract]
	bool CanBeRemoved(char product, int amount);

	[OperationContract]
	bool AddStock(char product);

	[OperationContract]
	List<int> GetStocks();

	[OperationContract]
	bool AddStockWithQuantity(char product, int quantity);

	[OperationContract]
	bool RemoveStockUnit(char product);

	[OperationContract]
	void AddStockUnit(char product);

	[OperationContract]
	List<Tuple<char, int>> ManyStockOfWarehouse(string warehouse);

	[OperationContract]
	bool CanBenRemovedMaster(string warehouse, char product, int quantity);

	[OperationContract]
	bool RemoveStockMaster(string warehouse, char product, int quantity);

	[OperationContract]
	bool AddStockMaster(string warehouse, char product, int quantity);

	[OperationContract]
	int ManyStockOfWarehouseProduct(string warehouse, char product);
}
