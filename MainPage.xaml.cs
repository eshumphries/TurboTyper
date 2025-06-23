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
        // Create timer and current time retriever
        private DispatcherTimer Timer;
        private DateTime currentTime;
        // Create level number storer
        private int level;
        // Create the sentences to be used for each numbered level, with 6 total levels
        public class SentenceStorer
        {
            public string sentence1 { get; set; }
            public string sentence2 { get; set; }
            public string sentence3 { get; set; }
            public string sentence4 { get; set; }
            public string sentence5 { get; set; }
            public string sentence6 { get; set; }
        }

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
            // Set the emoticon to default face
            EmoteText.Text = "'_'";
            // Set the starting time amount
            currentTime = DateTime.Now.AddSeconds(18);
            // Construct new DispatcherTimer and set its interval
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(100);
            // Create the timer event and start it
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            // Make the timer a countdown timer
            var elapsedTime = currentTime - DateTime.Now;
            // Checking if the sentence typed is the sentence displayed
            if (InputText.Text == DisplayText.Text)
            {
                // Display a congratulation message and emoticon face and stop the timer
                EmoteText.Text = "^_^";
                DisplayText.Text = "Good job!";
                Timer.Stop();
                // Increase level number
                level++;
            }
            // Checking the timer and change the emoticon according to the time
            switch (Math.Ceiling(elapsedTime.TotalSeconds))
            {
                // Check if time's up 
                case 0:
                    // Stop the timer, enable the AButton, and change the emoticon to a death face
                    EmoteText.Text = "X_X";
                    AButton.IsEnabled = true;
                    Timer.Stop();
                    break;
                case 5:
                    // Change the emoticon to crying face
                    EmoteText.Text = ";_;";
                    break;
                case 10:
                    // Change the emoticon to a disappointed face
                    EmoteText.Text = "-_-";
                    break;
            }
            // Display the timer in the UI
            TimerText.Text = Math.Ceiling(elapsedTime.TotalSeconds).ToString();
        }

        private void AButton_Click(object sender, RoutedEventArgs e)
        {
            // Disable this button and fade it until either the full sentence is typed or the timer runs out
            AButton.IsEnabled = false;
            AButton.Opacity = 0.3;
            // Make other buttons except this one go away
            SButton.IsEnabled = false;
            SButton.Opacity = 0;
            DButton.IsEnabled = false;
            DButton.Opacity = 0;
            // Display the timer, emoticon face, and input text box
            EmoteText.Opacity = 100;
            TimerText.Opacity = 100;
            InputText.Opacity = 100;
            // Move the sentence text down a bit for better spacing below the timer and emoticon
            Canvas.SetTop(DisplayText, 60);
            // Store the sentences to be used in the game
            SentenceStorer Sentences = new SentenceStorer();
            Sentences.sentence1 = "Just type this.";
            Sentences.sentence2 = "How fast can you type?";
            Sentences.sentence3 = "Excellent job so far, but it gets trickier!";
            Sentences.sentence4 = "You've passed Level 1, 2, and 3. Very good.";
            Sentences.sentence5 = "The 6th and final level's coming up! Are you sure you're ready?";
            Sentences.sentence6 = "You asked for it! This level's difficulty is crazier, harder, and longer than the others. Is it beatable?";
            // Set level based on current level
            switch (level)
            {
                case 1:
                    level = 2;
                    DisplayText.Text = Sentences.sentence2;
                    break;
                case 2:
                    level = 3;
                    DisplayText.Text = Sentences.sentence3;
                    break;
                case 3:
                    level = 4;
                    DisplayText.Text = Sentences.sentence4;
                    break;
                case 4:
                    level = 5;
                    DisplayText.Text = Sentences.sentence5;
                    break;
                case 5:
                    level = 6;
                    DisplayText.Text = Sentences.sentence6;
                    break;
                default:
                    level = 1;
                    DisplayText.Text = Sentences.sentence1;
                    break;
            }
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

        // Here's the game levels used and the sentences for each level to type in the game:
        // 1. Just type this.
        // 2. How fast can you type?
        // 3. Excellent job so far, but it gets trickier!
        // 4. You've passed Level 1, 2, and 3. Very good.
        // 5. The 6th and final level's coming up! Are you sure you're ready?
        // 6. You asked for it! This level's difficulty is crazier, longer, and harder than the others. Is it beatable?

        // Here's the emoticons used for each timer number:
        // 18. '_'
        // 10. -_-
        // 5. ;_;
        // 0. X_X

        // Here's the emoticon used when you beat the level:
        // ^_^
    }
}
