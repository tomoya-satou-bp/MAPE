﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MAPE.Windows.GUI.Properties {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MAPE.Windows.GUI.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   厳密に型指定されたこのリソース クラスを使用して、すべての検索リソースに対し、
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   MAPE に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string App_Title {
            get {
                return ResourceManager.GetString("App_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The credential for {0} is required. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string CredentialDialog_Description {
            get {
                return ResourceManager.GetString("CredentialDialog_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   How _save the credential? に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string CredentialDialog_Group_Persistency {
            get {
                return ResourceManager.GetString("CredentialDialog_Group_Persistency", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cancel に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string CredentialDialog_Label_Cancel {
            get {
                return ResourceManager.GetString("CredentialDialog_Label_Cancel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   OK に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string CredentialDialog_Label_OK {
            get {
                return ResourceManager.GetString("CredentialDialog_Label_OK", resourceCulture);
            }
        }
        
        /// <summary>
        ///   _Password: に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string CredentialDialog_Label_Password {
            get {
                return ResourceManager.GetString("CredentialDialog_Label_Password", resourceCulture);
            }
        }
        
        /// <summary>
        ///   _UserName: に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string CredentialDialog_Label_UserName {
            get {
                return ResourceManager.GetString("CredentialDialog_Label_UserName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   save the credential に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string CredentialDialog_Persistency_Persistent {
            get {
                return ResourceManager.GetString("CredentialDialog_Persistency_Persistent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   keep during this application に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string CredentialDialog_Persistency_Process {
            get {
                return ResourceManager.GetString("CredentialDialog_Persistency_Process", resourceCulture);
            }
        }
        
        /// <summary>
        ///   do not save the credential に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string CredentialDialog_Persistency_Session {
            get {
                return ResourceManager.GetString("CredentialDialog_Persistency_Session", resourceCulture);
            }
        }
    }
}