﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GrabTheMoment.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\")]
        public string MLocal_path {
            get {
                return ((string)(this["MLocal_path"]));
            }
            set {
                this["MLocal_path"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool MLocal {
            get {
                return ((bool)(this["MLocal"]));
            }
            set {
                this["MLocal"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool MFtp {
            get {
                return ((bool)(this["MFtp"]));
            }
            set {
                this["MFtp"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("felhasznalod")]
        public string MFtp_user {
            get {
                return ((string)(this["MFtp_user"]));
            }
            set {
                this["MFtp_user"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("jelszavad")]
        public string MFtp_password {
            get {
                return ((string)(this["MFtp_password"]));
            }
            set {
                this["MFtp_password"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ftpszerver.hu")]
        public string MFtp_address {
            get {
                return ((string)(this["MFtp_address"]));
            }
            set {
                this["MFtp_address"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("21")]
        public string MFtp_port {
            get {
                return ((string)(this["MFtp_port"]));
            }
            set {
                this["MFtp_port"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://oldalahovafeltoltom.hu/mappa/ahovaakarommenteni")]
        public string MFtp_path {
            get {
                return ((string)(this["MFtp_path"]));
            }
            set {
                this["MFtp_path"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("mappa/ahovaakarommenteni")]
        public string MFtp_remotedir {
            get {
                return ((string)(this["MFtp_remotedir"]));
            }
            set {
                this["MFtp_remotedir"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool MDropbox {
            get {
                return ((bool)(this["MDropbox"]));
            }
            set {
                this["MDropbox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string oauth_token {
            get {
                return ((string)(this["oauth_token"]));
            }
            set {
                this["oauth_token"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string oauth_token_secret {
            get {
                return ((string)(this["oauth_token_secret"]));
            }
            set {
                this["oauth_token_secret"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool MImgur {
            get {
                return ((bool)(this["MImgur"]));
            }
            set {
                this["MImgur"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int CopyLink {
            get {
                return ((int)(this["CopyLink"]));
            }
            set {
                this["CopyLink"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string MDropbox_accesstoken {
            get {
                return ((string)(this["MDropbox_accesstoken"]));
            }
            set {
                this["MDropbox_accesstoken"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string MDropbox_accesssecret {
            get {
                return ((string)(this["MDropbox_accesssecret"]));
            }
            set {
                this["MDropbox_accesssecret"] = value;
            }
        }
    }
}
