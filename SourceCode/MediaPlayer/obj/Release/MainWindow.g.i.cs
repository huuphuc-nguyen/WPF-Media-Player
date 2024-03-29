﻿#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A500347DE7C02E8D4D6E07513276438B05F2FC1B570CB26110CD8959303404C9"
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
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 58 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl ccSideView;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel pnlControlBar;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMaximize;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMinimize;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl ccMainView;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbCurrentTime;
        
        #line default
        #line hidden
        
        
        #line 151 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sliderMySlider;
        
        #line default
        #line hidden
        
        
        #line 160 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbTotalTime;
        
        #line default
        #line hidden
        
        
        #line 176 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FontAwesome.Sharp.IconImage icPrevious;
        
        #line default
        #line hidden
        
        
        #line 179 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FontAwesome.Sharp.IconImage icPlayPause;
        
        #line default
        #line hidden
        
        
        #line 182 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FontAwesome.Sharp.IconImage icNext;
        
        #line default
        #line hidden
        
        
        #line 187 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FontAwesome.Sharp.IconImage icRepeat;
        
        #line default
        #line hidden
        
        
        #line 190 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FontAwesome.Sharp.IconImage icRepeat_1;
        
        #line default
        #line hidden
        
        
        #line 195 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FontAwesome.Sharp.IconImage icVolume;
        
        #line default
        #line hidden
        
        
        #line 198 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FontAwesome.Sharp.IconImage icOffVolume;
        
        #line default
        #line hidden
        
        
        #line 203 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sliderVolume;
        
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
            System.Uri resourceLocater = new System.Uri("/MediaPlayer;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
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
            
            #line 16 "..\..\MainWindow.xaml"
            ((MediaPlayer.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ccSideView = ((System.Windows.Controls.ContentControl)(target));
            return;
            case 3:
            this.pnlControlBar = ((System.Windows.Controls.StackPanel)(target));
            
            #line 96 "..\..\MainWindow.xaml"
            this.pnlControlBar.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.pnlControlBar_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 100 "..\..\MainWindow.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnMaximize = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.btnMinimize = ((System.Windows.Controls.Button)(target));
            
            #line 112 "..\..\MainWindow.xaml"
            this.btnMinimize.Click += new System.Windows.RoutedEventHandler(this.btnMinimize_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ccMainView = ((System.Windows.Controls.ContentControl)(target));
            return;
            case 8:
            this.tbCurrentTime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.sliderMySlider = ((System.Windows.Controls.Slider)(target));
            
            #line 153 "..\..\MainWindow.xaml"
            this.sliderMySlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.Slider_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.tbTotalTime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.icPrevious = ((FontAwesome.Sharp.IconImage)(target));
            
            #line 178 "..\..\MainWindow.xaml"
            this.icPrevious.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.icPrevious_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 12:
            this.icPlayPause = ((FontAwesome.Sharp.IconImage)(target));
            
            #line 180 "..\..\MainWindow.xaml"
            this.icPlayPause.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.IconImage_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 13:
            this.icNext = ((FontAwesome.Sharp.IconImage)(target));
            
            #line 184 "..\..\MainWindow.xaml"
            this.icNext.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.icNext_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 14:
            this.icRepeat = ((FontAwesome.Sharp.IconImage)(target));
            
            #line 189 "..\..\MainWindow.xaml"
            this.icRepeat.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.icRepeat_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 15:
            this.icRepeat_1 = ((FontAwesome.Sharp.IconImage)(target));
            
            #line 192 "..\..\MainWindow.xaml"
            this.icRepeat_1.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.icRepeat_1_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 16:
            this.icVolume = ((FontAwesome.Sharp.IconImage)(target));
            
            #line 197 "..\..\MainWindow.xaml"
            this.icVolume.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.icVolume_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 17:
            this.icOffVolume = ((FontAwesome.Sharp.IconImage)(target));
            
            #line 200 "..\..\MainWindow.xaml"
            this.icOffVolume.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.icOffVolume_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 18:
            this.sliderVolume = ((System.Windows.Controls.Slider)(target));
            
            #line 205 "..\..\MainWindow.xaml"
            this.sliderVolume.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.sliderVolume_ValueChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

