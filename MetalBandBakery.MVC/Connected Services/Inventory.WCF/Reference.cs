﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MetalBandBakery.MVC.Inventory.WCF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Inventory.WCF.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/CheckStock", ReplyAction="http://tempuri.org/IService/CheckStockResponse")]
        bool CheckStock(char product);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/CheckStock", ReplyAction="http://tempuri.org/IService/CheckStockResponse")]
        System.Threading.Tasks.Task<bool> CheckStockAsync(char product);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/RemoveStock", ReplyAction="http://tempuri.org/IService/RemoveStockResponse")]
        bool RemoveStock(char product, int amount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/RemoveStock", ReplyAction="http://tempuri.org/IService/RemoveStockResponse")]
        System.Threading.Tasks.Task<bool> RemoveStockAsync(char product, int amount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ManyStock", ReplyAction="http://tempuri.org/IService/ManyStockResponse")]
        int ManyStock(char product);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ManyStock", ReplyAction="http://tempuri.org/IService/ManyStockResponse")]
        System.Threading.Tasks.Task<int> ManyStockAsync(char product);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/CanBeRemoved", ReplyAction="http://tempuri.org/IService/CanBeRemovedResponse")]
        bool CanBeRemoved(char product, int amount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/CanBeRemoved", ReplyAction="http://tempuri.org/IService/CanBeRemovedResponse")]
        System.Threading.Tasks.Task<bool> CanBeRemovedAsync(char product, int amount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/AddStock", ReplyAction="http://tempuri.org/IService/AddStockResponse")]
        bool AddStock(char product);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/AddStock", ReplyAction="http://tempuri.org/IService/AddStockResponse")]
        System.Threading.Tasks.Task<bool> AddStockAsync(char product);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetStocks", ReplyAction="http://tempuri.org/IService/GetStocksResponse")]
        int[] GetStocks();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetStocks", ReplyAction="http://tempuri.org/IService/GetStocksResponse")]
        System.Threading.Tasks.Task<int[]> GetStocksAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetShorts", ReplyAction="http://tempuri.org/IService/GetShortsResponse")]
        string[] GetShorts();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetShorts", ReplyAction="http://tempuri.org/IService/GetShortsResponse")]
        System.Threading.Tasks.Task<string[]> GetShortsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/AddStockWithQuantity", ReplyAction="http://tempuri.org/IService/AddStockWithQuantityResponse")]
        bool AddStockWithQuantity(char product, int quantity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/AddStockWithQuantity", ReplyAction="http://tempuri.org/IService/AddStockWithQuantityResponse")]
        System.Threading.Tasks.Task<bool> AddStockWithQuantityAsync(char product, int quantity);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : MetalBandBakery.MVC.Inventory.WCF.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<MetalBandBakery.MVC.Inventory.WCF.IService>, MetalBandBakery.MVC.Inventory.WCF.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool CheckStock(char product) {
            return base.Channel.CheckStock(product);
        }
        
        public System.Threading.Tasks.Task<bool> CheckStockAsync(char product) {
            return base.Channel.CheckStockAsync(product);
        }
        
        public bool RemoveStock(char product, int amount) {
            return base.Channel.RemoveStock(product, amount);
        }
        
        public System.Threading.Tasks.Task<bool> RemoveStockAsync(char product, int amount) {
            return base.Channel.RemoveStockAsync(product, amount);
        }
        
        public int ManyStock(char product) {
            return base.Channel.ManyStock(product);
        }
        
        public System.Threading.Tasks.Task<int> ManyStockAsync(char product) {
            return base.Channel.ManyStockAsync(product);
        }
        
        public bool CanBeRemoved(char product, int amount) {
            return base.Channel.CanBeRemoved(product, amount);
        }
        
        public System.Threading.Tasks.Task<bool> CanBeRemovedAsync(char product, int amount) {
            return base.Channel.CanBeRemovedAsync(product, amount);
        }
        
        public bool AddStock(char product) {
            return base.Channel.AddStock(product);
        }
        
        public System.Threading.Tasks.Task<bool> AddStockAsync(char product) {
            return base.Channel.AddStockAsync(product);
        }
        
        public int[] GetStocks() {
            return base.Channel.GetStocks();
        }
        
        public System.Threading.Tasks.Task<int[]> GetStocksAsync() {
            return base.Channel.GetStocksAsync();
        }
        
        public string[] GetShorts() {
            return base.Channel.GetShorts();
        }
        
        public System.Threading.Tasks.Task<string[]> GetShortsAsync() {
            return base.Channel.GetShortsAsync();
        }
        
        public bool AddStockWithQuantity(char product, int quantity) {
            return base.Channel.AddStockWithQuantity(product, quantity);
        }
        
        public System.Threading.Tasks.Task<bool> AddStockWithQuantityAsync(char product, int quantity) {
            return base.Channel.AddStockWithQuantityAsync(product, quantity);
        }
    }
}
