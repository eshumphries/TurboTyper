using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TurboTyper
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DisplayScrollViewer.SizeChanged += DisplayScrollViewer_SizeChanged;
        }

        private void DisplayScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double scale = Math.Min(e.NewSize.Width / 800, e.NewSize.Height / 600);
            DisplayGrid.RenderTransform = new ScaleTransform
            {
                ScaleX = scale * 1.8,
                ScaleY = scale * 1.8
            };
        }
    }
}
