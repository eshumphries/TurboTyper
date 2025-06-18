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
            AButton.Click += AButton_Click;
            SButton.Click += SButton_Click;
            DButton.Click += DButton_Click;
        }

        private void AButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayText.Text = "Just type the sentences that appear on the\nscreen in the textbox.  Type fast, because\nthere's a time limit.  Type the sentences on\ntime to go to the next level.  The next level\nwill increase in difficulty.  Finish all 6 levels to\nwin the game.";
        }

        private void DButton_Click(object sender, RoutedEventArgs e)
        {
            
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

        // Here's the sentences to type in the game:
        // 1. Just type this.
        // 2. How fast can you type?
        // 3. Excellent job so far, but it gets trickier!
        // 4. You've passed Level 1, 2, and 3. Very good.
        // 5. The 6th and final level's coming up! Are you sure you're ready?
        // 6. You asked for it! This level's difficulty is crazier, longer, and harder than the others. Is it beatable?
    }
}
