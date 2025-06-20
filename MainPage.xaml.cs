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
        // Create timer and total seconds
        private DispatcherTimer Timer;
        private int seconds;

        public MainPage()
        {
            InitializeComponent();
            // Create event to shrink or expand elements according to page size
            DisplayScrollViewer.SizeChanged += DisplayScrollViewer_SizeChanged;
            // Create events for the buttons in the UI
            AButton.Click += AButton_Click;
            SButton.Click += SButton_Click;
            DButton.Click += DButton_Click;
        }

        private void StartTimer()
        {
            // Start seconds at 18 to count down
            seconds = 18;
            // Construct new DispatcherTimer and set its interval
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            // Create the timer event and start it
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            // Make the timer a countdown timer and display it in the UI
            seconds--;
            TimerText.Text = $"{seconds}";
        }

        private void AButton_Click(object sender, RoutedEventArgs e)
        {
            // Make other buttons except this one go away
            SButton.IsEnabled = false;
            SButton.Opacity = 0;
            DButton.IsEnabled = false;
            DButton.Opacity = 0;
            // Display the timer and emoticon face
            EmoteText.Opacity = 100;
            TimerText.Opacity = 100;
            // Move the sentence text down a bit for better spacing below the timer and emoticon
            Canvas.SetTop(DisplayText, 60);
            // Start the timer
            StartTimer();
        }

        private void SButton_Click(object sender, RoutedEventArgs e)
        {
            // Move the sentence text up a bit so it looks better and have a help description to show how to play 
            Canvas.SetTop(DisplayText, 30);
            DisplayText.Text = "Just type the sentences that appear on the\nscreen in the textbox.  Type fast, because\nthere's a time limit.  Type the sentences on\ntime to go to the next level.  The next level\nwill increase in difficulty.  Finish all 6 levels to\nwin the game.";
        }

        private void DButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void DisplayScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // create a double that represents the default screen size
            double scale = Math.Min(e.NewSize.Width / 800, e.NewSize.Height / 600);
            // make the page elements scale according to page size
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
