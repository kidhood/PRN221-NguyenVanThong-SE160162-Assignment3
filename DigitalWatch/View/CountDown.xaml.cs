using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DigitalWatch.View
{
    /// <summary>
    /// Interaction logic for CountDown.xaml
    /// </summary>
    public partial class CountDown : UserControl
    {
        private DispatcherTimer countdownTimer;
        private TimeSpan countdownTime;
        private const int RefreshRate = 1000;

        public CountDown()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBoxHours(object sender, TextCompositionEventArgs e)
        {
            // Create a regular expression to match non-numeric characters
            string currentText = (sender as TextBox).Text;

            // Combine the existing text with the incoming character
            string newText = currentText.Substring(0, (sender as TextBox).SelectionStart) + e.Text +
                             currentText.Substring((sender as TextBox).SelectionStart + (sender as TextBox).SelectionLength);

            if (!int.TryParse(newText, out int value))
            {
                // If not, cancel the input
                e.Handled = true;
                return;
            }

            // Check if the value is within the valid range (0 to 24)
            if (value < 0 || value > 24)
            {
                // If not, cancel the input
                e.Handled = true;
                return;
            }

            // If the input is a valid integer and within the valid range, allow the input
            e.Handled = false;
        }

        private void hoursTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (hoursTextBox.Text == "__")
            {
                hoursTextBox.Text = "";
            }
        }

        private void hoursTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(hoursTextBox.Text))
            {
                hoursTextBox.Text = "__";
            }
        }


        private void NumberValidationTextBoxMinutes(object sender, TextCompositionEventArgs e)
        {
            // Create a regular expression to match non-numeric characters
            string currentText = (sender as TextBox).Text;

            // Combine the existing text with the incoming character
            string newText = currentText.Substring(0, (sender as TextBox).SelectionStart) + e.Text +
                             currentText.Substring((sender as TextBox).SelectionStart + (sender as TextBox).SelectionLength);

            if (!int.TryParse(newText, out int value))
            {
                // If not, cancel the input
                e.Handled = true;
                return;
            }

            // Check if the value is within the valid range (0 to 24)
            if (value < 0 || value > 59)
            {
                // If not, cancel the input
                e.Handled = true;
                return;
            }

            // If the input is a valid integer and within the valid range, allow the input
            e.Handled = false;
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            int hours = int.Parse(hoursTextBox.Text);
            int minutes = int.Parse(minutesTextBox.Text);
            int seconds = int.Parse(secondsTextBox.Text);

            if(hours > 0 || minutes > 0 || seconds > 0)
            {
                // Calculate total time in seconds
                countdownTime = new TimeSpan(hours, minutes, seconds);

                // Initialize and start the countdown timer
                countdownTimer = new DispatcherTimer();
                countdownTimer.Interval = TimeSpan.FromMilliseconds(RefreshRate);
                countdownTimer.Tick += CountdownTimer_Tick;
                countdownTimer.Start();
            }
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            // Update countdown time
            countdownTime = countdownTime.Subtract(TimeSpan.FromSeconds(1));

            // Update text boxes with new time values
            hoursTextBox.Text = countdownTime.Hours.ToString("00");
            minutesTextBox.Text = countdownTime.Minutes.ToString("00");
            secondsTextBox.Text = countdownTime.Seconds.ToString("00");

            // Check if countdown is completed
            if (countdownTime.TotalSeconds <= 0)
            {
                countdownTimer.Stop();
                // Reset color to default (white)
                hoursTextBox.Foreground = Brushes.White;
                minutesTextBox.Foreground = Brushes.White;
                secondsTextBox.Foreground = Brushes.White;
                MessageBox.Show("Times up!");
                // Countdown completed, perform any necessary actions
            }
            else if (countdownTime.TotalSeconds <= 10)
            {
                // Change color to red when remaining time is 10 seconds or less
                hoursTextBox.Foreground = Brushes.Red;
                minutesTextBox.Foreground = Brushes.Red;
                secondsTextBox.Foreground = Brushes.Red;
            }
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            countdownTimer.Stop();
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            countdownTimer.Stop();
            hoursTextBox.Text = "00";
            minutesTextBox.Text = "00";
            secondsTextBox.Text = "00";
        }
    }
}
