﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ivw.DevPatch {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ivw.DevPatch.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找 System.Byte[] 类型的本地化资源。
        /// </summary>
        internal static byte[] DevExpress_Patch_Common {
            get {
                object obj = ResourceManager.GetObject("DevExpress_Patch_Common", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   查找类似 &lt;RSAKeyValue&gt;&lt;Modulus&gt;1I4AydMb/+OCJ+f6OVqM8cFBPTDGf9kFhzF8FMqaxhFNweoM5ZZU/5Mpp3dxE45Rc/bf9p3ibq8JaR+nhlXfk0ArPDyIorvaqyN9rzKKQmo+SfibQy/KLp8Zg1/PSzvSoZDWjCmpYSULNKY0cECzxSSoVdI2etISovj+n5pIyHc=&lt;/Modulus&gt;&lt;Exponent&gt;AQAB&lt;/Exponent&gt;&lt;P&gt;8jP8RzwmNijAXEo4ZoMAPf3gyv+KEaXXWLtrHm21Bd305a2ZwE7hUC3g0ZUUdlqvMk6ZT9DxWX04gC656EOmSQ==&lt;/P&gt;&lt;Q&gt;4Kmo0zgRwvTp4/cGoZIEklkRgb9RtV/QRy1YKaWjOMjseY2QEzsbgwumxzj00u4anyuNvAZfp95ifiW4XPH4vw==&lt;/Q&gt;&lt;DP&gt;CQKSEfxU15LwhP5l1rps2eGF6UdUVY+70Rs3wuwF3fAB2kF5BMRqcVcjk+hd2IFLHy35WhTFxbR405vpYpCRsQ==&lt; [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string PatchRsaKey {
            get {
                return ResourceManager.GetString("PatchRsaKey", resourceCulture);
            }
        }
    }
}
