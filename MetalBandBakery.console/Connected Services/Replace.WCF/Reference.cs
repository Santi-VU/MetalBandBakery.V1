//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MetalBandBakery.console.Replace.WCF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Replace.WCF.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/NeedToBeReplace", ReplyAction="http://tempuri.org/IService/NeedToBeReplaceResponse")]
        bool NeedToBeReplace(char product);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/NeedToBeReplace", ReplyAction="http://tempuri.org/IService/NeedToBeReplaceResponse")]
        System.Threading.Tasks.Task<bool> NeedToBeReplaceAsync(char product);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ReplaceProduct", ReplyAction="http://tempuri.org/IService/ReplaceProductResponse")]
        void ReplaceProduct(char product);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ReplaceProduct", ReplyAction="http://tempuri.org/IService/ReplaceProductResponse")]
        System.Threading.Tasks.Task ReplaceProductAsync(char product);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : MetalBandBakery.console.Replace.WCF.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<MetalBandBakery.console.Replace.WCF.IService>, MetalBandBakery.console.Replace.WCF.IService {
        
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
        
        public bool NeedToBeReplace(char product) {
            return base.Channel.NeedToBeReplace(product);
        }
        
        public System.Threading.Tasks.Task<bool> NeedToBeReplaceAsync(char product) {
            return base.Channel.NeedToBeReplaceAsync(product);
        }
        
        public void ReplaceProduct(char product) {
            base.Channel.ReplaceProduct(product);
        }
        
        public System.Threading.Tasks.Task ReplaceProductAsync(char product) {
            return base.Channel.ReplaceProductAsync(product);
        }
    }
}
