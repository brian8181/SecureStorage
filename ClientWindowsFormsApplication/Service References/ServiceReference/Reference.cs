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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/CreateEmpty", ReplyAction="http://tempuri.org/ILowLevel/CreateEmptyResponse")]
        void CreateEmpty(string name, int len, bool random);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/CreateEmpty", ReplyAction="http://tempuri.org/ILowLevel/CreateEmptyResponse")]
        System.Threading.Tasks.Task CreateEmptyAsync(string name, int len, bool random);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/Create", ReplyAction="http://tempuri.org/ILowLevel/CreateResponse")]
        void Create(string name, byte[] data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/Create", ReplyAction="http://tempuri.org/ILowLevel/CreateResponse")]
        System.Threading.Tasks.Task CreateAsync(string name, byte[] data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/Read", ReplyAction="http://tempuri.org/ILowLevel/ReadResponse")]
        byte[] Read(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/Read", ReplyAction="http://tempuri.org/ILowLevel/ReadResponse")]
        System.Threading.Tasks.Task<byte[]> ReadAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/Write", ReplyAction="http://tempuri.org/ILowLevel/WriteResponse")]
        void Write(string name, int start, byte[] data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/Write", ReplyAction="http://tempuri.org/ILowLevel/WriteResponse")]
        System.Threading.Tasks.Task WriteAsync(string name, int start, byte[] data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/Delete", ReplyAction="http://tempuri.org/ILowLevel/DeleteResponse")]
        void Delete(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/Delete", ReplyAction="http://tempuri.org/ILowLevel/DeleteResponse")]
        System.Threading.Tasks.Task DeleteAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/Append", ReplyAction="http://tempuri.org/ILowLevel/AppendResponse")]
        void Append(string name, byte[] data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/Append", ReplyAction="http://tempuri.org/ILowLevel/AppendResponse")]
        System.Threading.Tasks.Task AppendAsync(string name, byte[] data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/Move", ReplyAction="http://tempuri.org/ILowLevel/MoveResponse")]
        void Move(string src, string dst);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/Move", ReplyAction="http://tempuri.org/ILowLevel/MoveResponse")]
        System.Threading.Tasks.Task MoveAsync(string src, string dst);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/Copy", ReplyAction="http://tempuri.org/ILowLevel/CopyResponse")]
        void Copy(string src, string dst);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/Copy", ReplyAction="http://tempuri.org/ILowLevel/CopyResponse")]
        System.Threading.Tasks.Task CopyAsync(string src, string dst);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/ReadData", ReplyAction="http://tempuri.org/ILowLevel/ReadDataResponse")]
        byte[] ReadData(string name, int start, int lenght);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/ReadData", ReplyAction="http://tempuri.org/ILowLevel/ReadDataResponse")]
        System.Threading.Tasks.Task<byte[]> ReadDataAsync(string name, int start, int lenght);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/MoveData", ReplyAction="http://tempuri.org/ILowLevel/MoveDataResponse")]
        void MoveData(string src, int src_idx, string dst, int dst_idx, int len);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/MoveData", ReplyAction="http://tempuri.org/ILowLevel/MoveDataResponse")]
        System.Threading.Tasks.Task MoveDataAsync(string src, int src_idx, string dst, int dst_idx, int len);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/CopyData", ReplyAction="http://tempuri.org/ILowLevel/CopyDataResponse")]
        void CopyData(string src, int src_idx, string dst, int dst_idx, int len);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILowLevel/CopyData", ReplyAction="http://tempuri.org/ILowLevel/CopyDataResponse")]
        System.Threading.Tasks.Task CopyDataAsync(string src, int src_idx, string dst, int dst_idx, int len);
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
        
        public void Create(string name, byte[] data) {
            base.Channel.Create(name, data);
        }
        
        public System.Threading.Tasks.Task CreateAsync(string name, byte[] data) {
            return base.Channel.CreateAsync(name, data);
        }
        
        public byte[] Read(string name) {
            return base.Channel.Read(name);
        }
        
        public System.Threading.Tasks.Task<byte[]> ReadAsync(string name) {
            return base.Channel.ReadAsync(name);
        }
        
        public void Write(string name, int start, byte[] data) {
            base.Channel.Write(name, start, data);
        }
        
        public System.Threading.Tasks.Task WriteAsync(string name, int start, byte[] data) {
            return base.Channel.WriteAsync(name, start, data);
        }
        
        public void Delete(string name) {
            base.Channel.Delete(name);
        }
        
        public System.Threading.Tasks.Task DeleteAsync(string name) {
            return base.Channel.DeleteAsync(name);
        }
        
        public void Append(string name, byte[] data) {
            base.Channel.Append(name, data);
        }
        
        public System.Threading.Tasks.Task AppendAsync(string name, byte[] data) {
            return base.Channel.AppendAsync(name, data);
        }
        
        public void Move(string src, string dst) {
            base.Channel.Move(src, dst);
        }
        
        public System.Threading.Tasks.Task MoveAsync(string src, string dst) {
            return base.Channel.MoveAsync(src, dst);
        }
        
        public void Copy(string src, string dst) {
            base.Channel.Copy(src, dst);
        }
        
        public System.Threading.Tasks.Task CopyAsync(string src, string dst) {
            return base.Channel.CopyAsync(src, dst);
        }
        
        public byte[] ReadData(string name, int start, int lenght) {
            return base.Channel.ReadData(name, start, lenght);
        }
        
        public System.Threading.Tasks.Task<byte[]> ReadDataAsync(string name, int start, int lenght) {
            return base.Channel.ReadDataAsync(name, start, lenght);
        }
        
        public void MoveData(string src, int src_idx, string dst, int dst_idx, int len) {
            base.Channel.MoveData(src, src_idx, dst, dst_idx, len);
        }
        
        public System.Threading.Tasks.Task MoveDataAsync(string src, int src_idx, string dst, int dst_idx, int len) {
            return base.Channel.MoveDataAsync(src, src_idx, dst, dst_idx, len);
        }
        
        public void CopyData(string src, int src_idx, string dst, int dst_idx, int len) {
            base.Channel.CopyData(src, src_idx, dst, dst_idx, len);
        }
        
        public System.Threading.Tasks.Task CopyDataAsync(string src, int src_idx, string dst, int dst_idx, int len) {
            return base.Channel.CopyDataAsync(src, src_idx, dst, dst_idx, len);
        }
    }
}
