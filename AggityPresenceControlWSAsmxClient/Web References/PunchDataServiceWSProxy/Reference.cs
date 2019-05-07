﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.42000.
// 
#pragma warning disable 1591

namespace AggityPresenceControlWSAsmxClient.PunchDataServiceWSProxy {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="PunchDataServiceSoap", Namespace="http://tempuri.org/")]
    public partial class PunchDataService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback SendPunchDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback ExportPunchDataOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public PunchDataService() {
            this.Url = global::AggityPresenceControlWSAsmxClient.Properties.Settings.Default.AggityPresenceControlWSAsmxClient_PunchDataServiceWSProxy_PunchDataService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event SendPunchDataCompletedEventHandler SendPunchDataCompleted;
        
        /// <remarks/>
        public event ExportPunchDataCompletedEventHandler ExportPunchDataCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SendPunchData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SendPunchData(string punchDataBaseJson, string hash) {
            object[] results = this.Invoke("SendPunchData", new object[] {
                        punchDataBaseJson,
                        hash});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void SendPunchDataAsync(string punchDataBaseJson, string hash) {
            this.SendPunchDataAsync(punchDataBaseJson, hash, null);
        }
        
        /// <remarks/>
        public void SendPunchDataAsync(string punchDataBaseJson, string hash, object userState) {
            if ((this.SendPunchDataOperationCompleted == null)) {
                this.SendPunchDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendPunchDataOperationCompleted);
            }
            this.InvokeAsync("SendPunchData", new object[] {
                        punchDataBaseJson,
                        hash}, this.SendPunchDataOperationCompleted, userState);
        }
        
        private void OnSendPunchDataOperationCompleted(object arg) {
            if ((this.SendPunchDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendPunchDataCompleted(this, new SendPunchDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ExportPunchData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void ExportPunchData() {
            this.Invoke("ExportPunchData", new object[0]);
        }
        
        /// <remarks/>
        public void ExportPunchDataAsync() {
            this.ExportPunchDataAsync(null);
        }
        
        /// <remarks/>
        public void ExportPunchDataAsync(object userState) {
            if ((this.ExportPunchDataOperationCompleted == null)) {
                this.ExportPunchDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnExportPunchDataOperationCompleted);
            }
            this.InvokeAsync("ExportPunchData", new object[0], this.ExportPunchDataOperationCompleted, userState);
        }
        
        private void OnExportPunchDataOperationCompleted(object arg) {
            if ((this.ExportPunchDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ExportPunchDataCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void SendPunchDataCompletedEventHandler(object sender, SendPunchDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendPunchDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendPunchDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void ExportPunchDataCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591