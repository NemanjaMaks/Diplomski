﻿#pragma checksum "..\..\..\..\CustomComponents\PrimljeniZahteviPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E0B46EAD882321115B015D8C473FF8D746C16E96C54B7F287E7B4ECFCC29F44B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Diplomski.CustomComponents;
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


namespace Diplomski.CustomComponents {
    
    
    /// <summary>
    /// PrimljeniZahteviPage
    /// </summary>
    public partial class PrimljeniZahteviPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 47 "..\..\..\..\CustomComponents\PrimljeniZahteviPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGrid;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\CustomComponents\PrimljeniZahteviPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_prihvati;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\CustomComponents\PrimljeniZahteviPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_odbij;
        
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
            System.Uri resourceLocater = new System.Uri("/Diplomski;component/customcomponents/primljenizahtevipage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\CustomComponents\PrimljeniZahteviPage.xaml"
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
            this.dataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.btn_prihvati = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\..\CustomComponents\PrimljeniZahteviPage.xaml"
            this.btn_prihvati.Click += new System.Windows.RoutedEventHandler(this.btn_prihvati_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btn_odbij = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\..\CustomComponents\PrimljeniZahteviPage.xaml"
            this.btn_odbij.Click += new System.Windows.RoutedEventHandler(this.btn_odbij_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

