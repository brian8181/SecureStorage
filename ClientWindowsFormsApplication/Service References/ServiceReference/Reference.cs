﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientWindowsFormsApplication.ServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/CreateEmpty", ReplyAction="http://tempuri.org/IData/CreateEmptyResponse")]
        void CreateEmpty(string name, int len, bool random);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/CreateEmpty", ReplyAction="http://tempuri.org/IData/CreateEmptyResponse")]
        System.Threading.Tasks.Task CreateEmptyAsync(string name, int len, bool random);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/CreateAppend", ReplyAction="http://tempuri.org/IData/CreateAppendResponse")]
        bool CreateAppend(string name, byte[] data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/CreateAppend", ReplyAction="http://tempuri.org/IData/CreateAppendResponse")]
        System.Threading.Tasks.Task<bool> CreateAppendAsync(string name, byte[] data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/CreateReplace", ReplyAction="http://tempuri.org/IData/CreateReplaceResponse")]
        bool CreateReplace(string name, byte[] data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/CreateReplace", ReplyAction="http://tempuri.org/IData/CreateReplaceResponse")]
        System.Threading.Tasks.Task<bool> CreateReplaceAsync(string name, byte[] data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/Read", ReplyAction="http://tempuri.org/IData/ReadResponse")]
        byte[] Read(string name, int offset, int lenght);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/Read", ReplyAction="http://tempuri.org/IData/ReadResponse")]
        System.Threading.Tasks.Task<byte[]> ReadAsync(string name, int offset, int lenght);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/Delete", ReplyAction="http://tempuri.org/IData/DeleteResponse")]
        void Delete(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/Delete", ReplyAction="http://tempuri.org/IData/DeleteResponse")]
        System.Threading.Tasks.Task DeleteAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/GetLength", ReplyAction="http://tempuri.org/IData/GetLengthResponse")]
        long GetLength(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/GetLength", ReplyAction="http://tempuri.org/IData/GetLengthResponse")]
        System.Threading.Tasks.Task<long> GetLengthAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/GetCount", ReplyAction="http://tempuri.org/IData/GetCountResponse")]
        int GetCount();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/GetCount", ReplyAction="http://tempuri.org/IData/GetCountResponse")]
        System.Threading.Tasks.Task<int> GetCountAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/Exists", ReplyAction="http://tempuri.org/IData/ExistsResponse")]
        bool Exists(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/Exists", ReplyAction="http://tempuri.org/IData/ExistsResponse")]
        System.Threading.Tasks.Task<bool> ExistsAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/GetNames", ReplyAction="http://tempuri.org/IData/GetNamesResponse")]
        string[] GetNames(int idx, int len);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/GetNames", ReplyAction="http://tempuri.org/IData/GetNamesResponse")]
        System.Threading.Tasks.Task<string[]> GetNamesAsync(int idx, int len);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/GetAllNames", ReplyAction="http://tempuri.org/IData/GetAllNamesResponse")]
        string[] GetAllNames();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/GetAllNames", ReplyAction="http://tempuri.org/IData/GetAllNamesResponse")]
        System.Threading.Tasks.Task<string[]> GetAllNamesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/DeleteAll", ReplyAction="http://tempuri.org/IData/DeleteAllResponse")]
        void DeleteAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/DeleteAll", ReplyAction="http://tempuri.org/IData/DeleteAllResponse")]
        System.Threading.Tasks.Task DeleteAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/SHA256", ReplyAction="http://tempuri.org/IData/SHA256Response")]
        byte[] SHA256(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IData/SHA256", ReplyAction="http://tempuri.org/IData/SHA256Response")]
        System.Threading.Tasks.Task<byte[]> SHA256Async(string name);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : ClientWindowsFormsApplication.ServiceReference.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<ClientWindowsFormsApplication.ServiceReference.IService>, ClientWindowsFormsApplication.ServiceReference.IService {
        
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
        
        public void CreateEmpty(string name, int len, bool random) {
            base.Channel.CreateEmpty(name, len, random);
        }
        
        public System.Threading.Tasks.Task CreateEmptyAsync(string name, int len, bool random) {
            return base.Channel.CreateEmptyAsync(name, len, random);
        }
        
        public bool CreateAppend(string name, byte[] data) {
            return base.Channel.CreateAppend(name, data);
        }
        
        public System.Threading.Tasks.Task<bool> CreateAppendAsync(string name, byte[] data) {
            return base.Channel.CreateAppendAsync(name, data);
        }
        
        public bool CreateReplace(string name, byte[] data) {
            return base.Channel.CreateReplace(name, data);
        }
        
        public System.Threading.Tasks.Task<bool> CreateReplaceAsync(string name, byte[] data) {
            return base.Channel.CreateReplaceAsync(name, data);
        }
        
        public byte[] Read(string name, int offset, int lenght) {
            return base.Channel.Read(name, offset, lenght);
        }
        
        public System.Threading.Tasks.Task<byte[]> ReadAsync(string name, int offset, int lenght) {
            return base.Channel.ReadAsync(name, offset, lenght);
        }
        
        public void Delete(string name) {
            base.Channel.Delete(name);
        }
        
        public System.Threading.Tasks.Task DeleteAsync(string name) {
            return base.Channel.DeleteAsync(name);
        }
        
        public long GetLength(string name) {
            return base.Channel.GetLength(name);
        }
        
        public System.Threading.Tasks.Task<long> GetLengthAsync(string name) {
            return base.Channel.GetLengthAsync(name);
        }
        
        public int GetCount() {
            return base.Channel.GetCount();
        }
        
        public System.Threading.Tasks.Task<int> GetCountAsync() {
            return base.Channel.GetCountAsync();
        }
        
        public bool Exists(string name) {
            return base.Channel.Exists(name);
        }
        
        public System.Threading.Tasks.Task<bool> ExistsAsync(string name) {
            return base.Channel.ExistsAsync(name);
        }
        
        public string[] GetNames(int idx, int len) {
            return base.Channel.GetNames(idx, len);
        }
        
        public System.Threading.Tasks.Task<string[]> GetNamesAsync(int idx, int len) {
            return base.Channel.GetNamesAsync(idx, len);
        }
        
        public string[] GetAllNames() {
            return base.Channel.GetAllNames();
        }
        
        public System.Threading.Tasks.Task<string[]> GetAllNamesAsync() {
            return base.Channel.GetAllNamesAsync();
        }
        
        public void DeleteAll() {
            base.Channel.DeleteAll();
        }
        
        public System.Threading.Tasks.Task DeleteAllAsync() {
            return base.Channel.DeleteAllAsync();
        }
        
        public byte[] SHA256(string name) {
            return base.Channel.SHA256(name);
        }
        
        public System.Threading.Tasks.Task<byte[]> SHA256Async(string name) {
            return base.Channel.SHA256Async(name);
        }
    }
}
