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
        DispatcherTimer Timer;
        DateTime currentTime;
        // Create level number storer, amount of timer seconds, and button UI state storer
        int level, seconds, aButtonState, sButtonState, dButtonState;
        // Create the sentences to be used for each numbered level, with 6 total levels
        string sentence1, sentence2, sentence3, sentence4, sentence5, sentence6;
        // Create a list to store the sentences to reference by level number
        List<string> sentenceList;

        public MainPage()
        {
            InitializeComponent();
            // Create event to shrink or expand elements according to page size
            DisplayScrollViewer.SizeChanged += DisplayScrollViewer_SizeChanged;
            // Create events for the buttons in the UI
            AButton.Click += AButton_Click;
            SButton.Click += SButton_Click;
            DButton.Click += DButton_Click;
            // Construct new DispatcherTimer and set its interval
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(100);
            // Make the amount of seconds total
            seconds = 18;
            // Set the starting time amount
            currentTime = DateTime.Now.AddSeconds(seconds);
            // Set starting level
            level = 1;
            // Set the button states 
            aButtonState = 0;
            sButtonState = 0;
            dButtonState = 0;
            // Store the sentences to be used in the game
            sentence1 = "Just type this.";
            sentence2 = "How fast can you type?";
            sentence3 = "Excellent job so far, but it gets\ntrickier!";
            sentence4 = "You've passed Level 1, 2, and 3.\nVery good.";
            sentence5 = "The 6th and final level's coming\nup! Are you sure you're ready?";
            sentence6 = "You asked for it! This level's\ndifficulty is crazier, harder, and\nlonger than the others. Is it beatable?";
            // Store the sentences to be used in the list
            sentenceList = new List<string> { sentence1, sentence2, sentence3, sentence4, sentence5, sentence6 };
        }

        private void StartTimer()
        {
            // Set the emoticon to default face
            EmoteText.Text = "'_'";
            // Create the timer event and start it
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            // Make the timer a countdown timer
            var elapsedTime = currentTime - DateTime.Now;
            // Remove the line breaks from the displayed sentence text and store this string in a variable
            string displayString = DisplayText.Text.Replace("\r", " ").Replace("\n", " ");
            // Store the player input in a variable
            string inputString = InputText.Text;
            // Checking if the sentence typed is the sentence displayed
            if (inputString == displayString)
            {
                // Check if it's the 6th level
                if (level == 6)
                {
                    // Display a game win message and emoticon, then reset the level
                    EmoteText.Text = "*o*";
                    DisplayText.Text = "Congratulations! You win! Try again?";
                    level = 1;
                }
                else
                {
                    // For other levels just have a default win emoticon and message, and then increase level number
                    EmoteText.Text = "^_^";
                    DisplayText.Text = "Good job!";
                    level++;
                }
                // Stop the timer
                Timer.Stop();
                // Enable the left button, make it fully opaque, and give it focus
                AButton.IsEnabled = true;
                AButton.Opacity = 1;
                AButton.Focus();
                // Reset the button state
                aButtonState = 0;
            }
            // Checking the timer and change the emoticon according to the time
            switch (Math.Ceiling(elapsedTime.TotalSeconds))
            {
                // Check if time's up 
                case 0:
                    // Change the emoticon to a death face and display a game over and try again message
                    EmoteText.Text = "X_X";
                    DisplayText.Text = "Game over! Try again?";
                    // Stop the timer
                    Timer.Stop();
                    // Enable the left button, make it opaque again, and focus on it
                    AButton.IsEnabled = true;
                    AButton.Opacity = 1;
                    AButton.Focus();
                    // Reset the level to start over from the beginning
                    level = 1;
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
            // Set the correct sentence for the correct level and fix the line break problem
            switch (level)
            {
                case 1:
                    DisplayText.Text.Replace("\n", Environment.NewLine);
                    DisplayText.Text = sentence1;
                    break;
                case 2:
                    DisplayText.Text.Replace("\n", Environment.NewLine);
                    DisplayText.Text = sentence2;
                    break;
                case 3:
                    DisplayText.Text.Replace("\n", Environment.NewLine);
                    DisplayText.Text = sentence3;
                    break;
                case 4:
                    DisplayText.Text.Replace("\n", Environment.NewLine);
                    DisplayText.Text = sentence4;
                    break;
                case 5:
                    DisplayText.Text.Replace("\n", Environment.NewLine);
                    DisplayText.Text = sentence5;
                    break;
                case 6:
                    DisplayText.Text.Replace("\n", Environment.NewLine);
                    DisplayText.Text = sentence6;
                    break;
            }
            // Check if it's the first time the left button was clicked without clicking the right button
            if (aButtonState == 0 && sButtonState == 0 && dButtonState == 0)
            {
                // Disable this button and make it disappear until either the full sentence is typed or the timer runs out
                AButton.IsEnabled = false;
                AButton.Opacity = 0;
                // Make other buttons except this one go away
                SButton.IsEnabled = false;
                SButton.Opacity = 0;
                DButton.IsEnabled = false;
                DButton.Opacity = 0;
                // Display the timer, emoticon face, and input text box
                EmoteText.Opacity = 1;
                TimerText.Opacity = 1;
                InputText.Opacity = 1;
                // Move the sentence text down a bit for better spacing below the timer and emoticon
                Canvas.SetTop(DisplayText, 60);
                // Clear the text input box for the next level after completing the previous and focus on it
                InputText.Text = "";
                InputText.Focus();
                // Start the timer
                StartTimer();
            }
            // Show different buttons when the right button is clicked
            else if (aButtonState == 0 && sButtonState == 0 && dButtonState == 1)
            {
                // Change all the buttons to increase and decrease level number choice and to accept level number
                AButtonText.Text = "Accept";
                AButtonSymbol.Text = "a";
                SButtonText.Text = "Next";
                SButtonSymbol.Text = "4";
                DButtonText.Text = "Cancel";
                DButtonSymbol.Text = "r";
                // Show the current level number and the level's sentence to edit in the input text box
                DisplayText.Text = "Level: " + level.ToString();
                InputText.IsEnabled = true;
                InputText.Opacity = 1;
                InputText.Text = sentenceList[level];
                // Set this button's UI state to a deeper UI state
                aButtonState = 1;
            }
            // Edit the sentence to be used in the level
            else if (aButtonState == 1 && sButtonState == 0 && dButtonState == 1)
            {
                DisplayText.Text = "Saved!";
                sentenceList[level] = InputText.Text;
            }
            else if (aButtonState == 0 && sButtonState == 1 && dButtonState == 1)
            {
                DisplayText.Text = "Saved!";
                int.TryParse(currentTime.ToString(), out seconds);
                currentTime = DateTime.Now.AddSeconds(seconds);
            }
        }

        private void SButton_Click(object sender, RoutedEventArgs e)
        {
            // What happens when the middle button or right button weren't clicked before
            if (aButtonState == 0 && dButtonState == 0)
            {
                // Move the sentence text up a bit so it looks better and have a help description to show how to play 
                Canvas.SetTop(DisplayText, 30);
                DisplayText.Text = "Just type the sentences that appear on the\nscreen in the textbox.  Type fast, because\nthere's a time limit.  Type the sentences on\ntime to go to the next level.  The next level\nwill increase in difficulty.  Finish all 6 levels to\nwin the game.";
            }
            // When the right button was clicked first
            else if (aButtonState == 0 && dButtonState == 1)
            {
                DisplayText.Text = "Timer: " + seconds;
                InputText.IsEnabled = true;
                InputText.Opacity = 1;
                InputText.Text = seconds.ToString();
                // Change all the buttons to accept timer amount
                AButtonText.Text = "Accept";
                AButtonSymbol.Text = "a";
                DButtonText.Text = "Cancel";
                DButtonSymbol.Text = "r";
                // Get rid of the middle button for now
                SButton.IsEnabled = false;
                SButton.Opacity = 0;
                // Set the state of the middle button
                sButtonState = 1;
            }
        }

        private void DButton_Click(object sender, RoutedEventArgs e)
        {
            // Have the buttons change when player clicks the right button
            switch (dButtonState)
            {
                // Show the selectable options through the button changes
                case 0:
                    DisplayText.Text = "What option do you want to change?";
                    AButtonText.Text = "Levels";
                    AButtonSymbol.Text = "";
                    SButtonText.Text = "Timer";
                    SButtonSymbol.Text = "¡";
                    DButtonText.Text = "Cancel";
                    DButtonSymbol.Text = "r";
                    dButtonState = 1;
                    break;
                // Change it back
                case 1:
                    DisplayText.Text = "";
                    AButtonText.Text = "Play";
                    AButtonSymbol.Text = "4";
                    SButtonText.Text = "Help";
                    SButtonSymbol.Text = "s";
                    DButtonText.Text = "Options";
                    DButtonSymbol.Text = "¼";
                    dButtonState = 0;
                    break;
            }
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
    }
}
