﻿#pragma checksum "..\..\ucMenu.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "02506AB4CB5E71B9C13883479C93C5960767DB2EBE18B9DDECE11022B4A51DE9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FontAwesome.Sharp;
using MediaPlayer;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace MediaPlayer {
    
    
    /// <summary>
    /// ucMenu
    /// </summary>
    public partial class ucMenu : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 46 "..\..\ucMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton radbtnOpenFiles;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\ucMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton radbtnNowPlaying;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\ucMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton radbtnRecentPlay;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\ucMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvPlaylist;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\ucMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContextMenu ctmnPlayLists;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\ucMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnRename;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\ucMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FontAwesome.Sharp.IconImage icPen;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\ucMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnDelete;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\ucMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FontAwesome.Sharp.IconImage icDelete;
        
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
            System.Uri resourceLocater = new System.Uri("/MediaPlayer;component/ucmenu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ucMenu.xaml"
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
            this.radbtnOpenFiles = ((System.Windows.Controls.RadioButton)(target));
            
            #line 46 "..\..\ucMenu.xaml"
            this.radbtnOpenFiles.Checked += new System.Windows.RoutedEventHandler(this.radbtnOpenFiles_Checked);
            
            #line default
            #line hidden
            return;
            case 2:
            this.radbtnNowPlaying = ((System.Windows.Controls.RadioButton)(target));
            
            #line 53 "..\..\ucMenu.xaml"
            this.radbtnNowPlaying.Checked += new System.Windows.RoutedEventHandler(this.radbtnNowPlaying_Checked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.radbtnRecentPlay = ((System.Windows.Controls.RadioButton)(target));
            
            #line 60 "..\..\ucMenu.xaml"
            this.radbtnRecentPlay.Checked += new System.Windows.RoutedEventHandler(this.RadioButton_Checked);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 77 "..\..\ucMenu.xaml"
            ((FontAwesome.Sharp.IconImage)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.IconImage_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lvPlaylist = ((System.Windows.Controls.ListView)(target));
            
            #line 86 "..\..\ucMenu.xaml"
            this.lvPlaylist.PreviewMouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.lvPlaylist_PreviewMouseRightButtonDown);
            
            #line default
            #line hidden
            
            #line 87 "..\..\ucMenu.xaml"
            this.lvPlaylist.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lvPlaylist_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ctmnPlayLists = ((System.Windows.Controls.ContextMenu)(target));
            return;
            case 7:
            this.mnRename = ((System.Windows.Controls.MenuItem)(target));
            
            #line 107 "..\..\ucMenu.xaml"
            this.mnRename.Click += new System.Windows.RoutedEventHandler(this.mnRename_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.icPen = ((FontAwesome.Sharp.IconImage)(target));
            return;
            case 9:
            this.mnDelete = ((System.Windows.Controls.MenuItem)(target));
            
            #line 119 "..\..\ucMenu.xaml"
            this.mnDelete.Click += new System.Windows.RoutedEventHandler(this.mnDelete_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.icDelete = ((FontAwesome.Sharp.IconImage)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

