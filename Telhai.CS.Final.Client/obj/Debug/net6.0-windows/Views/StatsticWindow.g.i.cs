﻿#pragma checksum "..\..\..\..\Views\StatsticWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5D1965A686451CFA5671B53C0EB6F6E7DC53AE09"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using Telhai.CS.Final.Client.Views;


namespace Telhai.CS.Final.Client.Views {
    
    
    /// <summary>
    /// StatsticWindow
    /// </summary>
    public partial class StatsticWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\..\..\Views\StatsticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtExamName;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Views\StatsticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtExamId;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Views\StatsticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtExamAvg;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Views\StatsticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBoxStudents;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\Views\StatsticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtStudentName;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\Views\StatsticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtStudentID;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\Views\StatsticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtStudentGrade;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\Views\StatsticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtSelectedAns;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\Views\StatsticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtCurrectAns;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\Views\StatsticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBoxsErrors;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.7.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Telhai.CS.Final.Client;V1.0.0.0;component/views/statsticwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\StatsticWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.7.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtExamName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.txtExamId = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.txtExamAvg = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.listBoxStudents = ((System.Windows.Controls.ListBox)(target));
            
            #line 42 "..\..\..\..\Views\StatsticWindow.xaml"
            this.listBoxStudents.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.listBoxStudents_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtStudentName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.txtStudentID = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.txtStudentGrade = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.txtSelectedAns = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.txtCurrectAns = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.listBoxsErrors = ((System.Windows.Controls.ListBox)(target));
            
            #line 72 "..\..\..\..\Views\StatsticWindow.xaml"
            this.listBoxsErrors.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.listBoxsErrors_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

