﻿#pragma checksum "..\..\..\..\Views\buildQuestionWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "08A0E3FD75C8E0F564F39315ED7716DC885E2B59"
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
    /// buildQuestionWindow
    /// </summary>
    public partial class buildQuestionWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\..\Views\buildQuestionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtQuesDesciption;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Views\buildQuestionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtAnswer;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Views\buildQuestionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddAnswer;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\Views\buildQuestionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRmAnswer;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\Views\buildQuestionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lstAnswers;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\Views\buildQuestionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddQuestion;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\..\Views\buildQuestionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancelQuestion;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Telhai.CS.Final.Client;component/views/buildquestionwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\buildQuestionWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtQuesDesciption = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtAnswer = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.btnAddAnswer = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\..\Views\buildQuestionWindow.xaml"
            this.btnAddAnswer.Click += new System.Windows.RoutedEventHandler(this.btnAddAnswer_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnRmAnswer = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.lstAnswers = ((System.Windows.Controls.ListBox)(target));
            return;
            case 6:
            this.btnAddQuestion = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\..\..\Views\buildQuestionWindow.xaml"
            this.btnAddQuestion.Click += new System.Windows.RoutedEventHandler(this.btnAddQuestion_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnCancelQuestion = ((System.Windows.Controls.Button)(target));
            
            #line 117 "..\..\..\..\Views\buildQuestionWindow.xaml"
            this.btnCancelQuestion.Click += new System.Windows.RoutedEventHandler(this.btnCancelQuestion_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

