﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteApplication.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class GlobalTranslations {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal GlobalTranslations() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebsiteApplication.Resources.GlobalTranslations", typeof(GlobalTranslations).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
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
        ///   Looks up a localized string similar to Miasto.
        /// </summary>
        public static string AddressCity {
            get {
                return ResourceManager.GetString("AddressCity", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Państwo.
        /// </summary>
        public static string AddressCountry {
            get {
                return ResourceManager.GetString("AddressCountry", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Województwo.
        /// </summary>
        public static string AddressProvince {
            get {
                return ResourceManager.GetString("AddressProvince", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ulica.
        /// </summary>
        public static string AddressStreet {
            get {
                return ResourceManager.GetString("AddressStreet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kod pocztowy.
        /// </summary>
        public static string AddressZipCode {
            get {
                return ResourceManager.GetString("AddressZipCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data urodzenia.
        /// </summary>
        public static string PersonBirthDate {
            get {
                return ResourceManager.GetString("PersonBirthDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Imię.
        /// </summary>
        public static string PersonFirstName {
            get {
                return ResourceManager.GetString("PersonFirstName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Numer ubezpieczenia.
        /// </summary>
        public static string PersonInsuranceNumber {
            get {
                return ResourceManager.GetString("PersonInsuranceNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nazwisko.
        /// </summary>
        public static string PersonSecondName {
            get {
                return ResourceManager.GetString("PersonSecondName", resourceCulture);
            }
        }
    }
}
