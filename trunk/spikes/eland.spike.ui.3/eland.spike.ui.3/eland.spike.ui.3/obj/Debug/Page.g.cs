#pragma checksum "C:\code\everland\spikes\eland.spike.ui.3\eland.spike.ui.3\eland.spike.ui.3\Page.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FE6058044D06893A83B67F23088A9D5E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4918
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace eland.spike.ui._3 {
    
    
    public partial class Page : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Button btnGenerateNoise;
        
        internal System.Windows.Controls.Button btnGenerateHexes;
        
        internal System.Windows.Controls.Slider slScale;
        
        internal System.Windows.Controls.Canvas cnvMain;
        
        internal System.Windows.Media.ScaleTransform CanvasScaleTransform;
        
        internal System.Windows.Media.RectangleGeometry CanvasClip;
        
        internal System.Windows.Controls.Image imgMain;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/eland.spike.ui.3;component/Page.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.btnGenerateNoise = ((System.Windows.Controls.Button)(this.FindName("btnGenerateNoise")));
            this.btnGenerateHexes = ((System.Windows.Controls.Button)(this.FindName("btnGenerateHexes")));
            this.slScale = ((System.Windows.Controls.Slider)(this.FindName("slScale")));
            this.cnvMain = ((System.Windows.Controls.Canvas)(this.FindName("cnvMain")));
            this.CanvasScaleTransform = ((System.Windows.Media.ScaleTransform)(this.FindName("CanvasScaleTransform")));
            this.CanvasClip = ((System.Windows.Media.RectangleGeometry)(this.FindName("CanvasClip")));
            this.imgMain = ((System.Windows.Controls.Image)(this.FindName("imgMain")));
        }
    }
}
