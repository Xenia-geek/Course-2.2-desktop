#pragma checksum "..\..\..\offers\ADDOffers.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "50C2AD593ED5A0153F5B561313931D12A00D62A09EE99285B256A65C38853561"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using курсач_7_мая.offers;


namespace курсач_7_мая.offers {
    
    
    /// <summary>
    /// ADDOffers
    /// </summary>
    public partial class ADDOffers : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 62 "..\..\..\offers\ADDOffers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Name;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\offers\ADDOffers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border bordername;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\offers\ADDOffers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameNOTcorrect;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\offers\ADDOffers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Password;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\offers\ADDOffers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border borderpass;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\offers\ADDOffers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PassNOTcorrect;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\..\offers\ADDOffers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AboutSerials;
        
        #line default
        #line hidden
        
        
        #line 167 "..\..\..\offers\ADDOffers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button @continue;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/курсач 7 мая;component/offers/addoffers.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\offers\ADDOffers.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Name = ((System.Windows.Controls.TextBox)(target));
            
            #line 74 "..\..\..\offers\ADDOffers.xaml"
            this.Name.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Name_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 74 "..\..\..\offers\ADDOffers.xaml"
            this.Name.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Name_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.bordername = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.NameNOTcorrect = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.Password = ((System.Windows.Controls.TextBox)(target));
            
            #line 118 "..\..\..\offers\ADDOffers.xaml"
            this.Password.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Password_TextChanged);
            
            #line default
            #line hidden
            
            #line 118 "..\..\..\offers\ADDOffers.xaml"
            this.Password.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Password_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.borderpass = ((System.Windows.Controls.Border)(target));
            return;
            case 6:
            this.PassNOTcorrect = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.AboutSerials = ((System.Windows.Controls.TextBox)(target));
            
            #line 165 "..\..\..\offers\ADDOffers.xaml"
            this.AboutSerials.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.AboutSerials_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.@continue = ((System.Windows.Controls.Button)(target));
            
            #line 176 "..\..\..\offers\ADDOffers.xaml"
            this.@continue.Click += new System.Windows.RoutedEventHandler(this.Continue_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

