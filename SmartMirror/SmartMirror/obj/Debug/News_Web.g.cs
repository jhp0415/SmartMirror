﻿#pragma checksum "..\..\News_Web.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "73186A9B4B98DB14F3915BDD56156173"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SmartMirror;
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


namespace SmartMirror {
    
    
    /// <summary>
    /// News_Web
    /// </summary>
    public partial class News_Web : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\News_Web.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUrl;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\News_Web.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WebBrowser wbSample;
        
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
            System.Uri resourceLocater = new System.Uri("/SmartMirror;component/news_web.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\News_Web.xaml"
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
            
            #line 13 "..\..\News_Web.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.BrowseBack_CanExecute);
            
            #line default
            #line hidden
            
            #line 13 "..\..\News_Web.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.BrowseBack_Executed);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 14 "..\..\News_Web.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.BrowseForward_CanExecute);
            
            #line default
            #line hidden
            
            #line 14 "..\..\News_Web.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.BrowseForward_Executed);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 15 "..\..\News_Web.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.GoToPage_CanExecute);
            
            #line default
            #line hidden
            
            #line 15 "..\..\News_Web.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.GoToPage_Executed);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtUrl = ((System.Windows.Controls.TextBox)(target));
            
            #line 28 "..\..\News_Web.xaml"
            this.txtUrl.KeyUp += new System.Windows.Input.KeyEventHandler(this.txtUrl_KeyUp);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 33 "..\..\News_Web.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.exit_click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.wbSample = ((System.Windows.Controls.WebBrowser)(target));
            
            #line 38 "..\..\News_Web.xaml"
            this.wbSample.Navigating += new System.Windows.Navigation.NavigatingCancelEventHandler(this.wbSample_Navigating);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
