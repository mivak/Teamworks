﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ElecrtronicStoreSQLiteDB.Data {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class sqliteSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static sqliteSettings defaultInstance = ((sqliteSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new sqliteSettings())));
        
        public static sqliteSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=..\\StoreData.sqlite; Version=3;Pooling=False;")]
        public string SQLITE_URI {
            get {
                return ((string)(this["SQLITE_URI"]));
            }
            set {
                this["SQLITE_URI"] = value;
            }
        }
    }
}
