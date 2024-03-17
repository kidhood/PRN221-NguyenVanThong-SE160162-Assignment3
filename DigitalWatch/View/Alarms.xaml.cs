using DigitalWatchBO.Models;
using DigitalWatchService;
using DigitalWatchService.Interface;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

namespace DigitalWatch.View
{
    /// <summary>
    /// Interaction logic for Alarm.xaml
    /// </summary>
    public partial class Alarms : UserControl
    {
        private CancellationTokenSource cancellationTokenSource;
        private ObservableCollection<Alarm> listAlarms;
        private readonly IAlarmService alarmService;
        public Alarms()
        {
            InitializeComponent();
            alarmService = new AlarmService();
            listAlarms = new ObservableCollection<Alarm>(alarmService.GetAlarms());
            dataListGrid.ItemsSource = listAlarms;
            Start();
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

            if ((hours > 0 || minutes > 0 || seconds > 0) && !string.IsNullOrWhiteSpace(purposeText.Text))
            {
                // Get the current date and time
                DateTime currentDate = DateTime.Now;

                // Create a new DateTime object with the desired time
                DateTime alarmTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, hours, minutes, seconds);

                // Check if the alarm time is in the past (if so, set it to the next day)
                if (alarmTime < currentDate)
                {
                    alarmTime = alarmTime.AddDays(1);
                }

                var pur = purposeText.Text;
                var a = new Alarm
                {
                    AlarmName = pur,
                    Timer = alarmTime
                };

                
                this.Stop();
                if(int.TryParse(alarmId.Text, out int id))
                {
                    a.Id = id;
                    if (this.alarmService.Update(a) != default)
                    {
                        this.listAlarms = this.RemoveAlarm(a);
                        this.listAlarms.Add(a);
                        dataListGrid.ItemsSource = this.listAlarms;
                        purposeText.Text = "";
                        alarmId.Text = "";
                        hoursTextBox.Text = "00";
                        minutesTextBox.Text = "00";
                        secondsTextBox.Text = "00";
                    }
                }
                else if(this.alarmService.Create(a) != default)
                {
                    this.listAlarms.Add(a);
                    purposeText.Text = "";
                    hoursTextBox.Text = "00";
                    minutesTextBox.Text = "00";
                    secondsTextBox.Text = "00";
                }
                this.Start();
            }
            else
            {
                MessageBox.Show("Please enter full input");
            }
        }

        private void btn_Remove(object sender, RoutedEventArgs e)
        {
            var selected = (Alarm)dataListGrid.SelectedItem;

            if (selected != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this alarm?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    this.Stop();
                    if (this.alarmService.Remove(selected))
                    {
                        this.listAlarms = this.RemoveAlarm(selected);
                        dataListGrid.ItemsSource = this.listAlarms;
                    }
                    this.Start();
                }
            }
        }


        public void Start()
        {
            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                Task.Run(async () =>
                {
                    while (!cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        // Get current time
                        DateTime currentTime = DateTime.Now;

                        // Retrieve all alarms from the database
                        List<Alarm> alarms = this.alarmService.GetAlarms();

                        // Check if any alarm matches the current time
                        foreach (var alarm in alarms)
                        {
                            if (alarm.Timer.Hour == currentTime.Hour && alarm.Timer.Minute == currentTime.Minute && alarm.Timer.Second == currentTime.Second)
                            {
                                // Notify user about the alarm
                                this.alarmService.Remove(alarm);

                                // Perform UI updates on the UI thread
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    // Remove from the UI-bound collection
                                    this.listAlarms = this.RemoveAlarm(alarm);
                                    dataListGrid.ItemsSource = listAlarms;
                                    // Show message box
                                    MessageBox.Show($"Alarm '{alarm.AlarmName}' is ringing!", "Alarm", MessageBoxButton.OK, MessageBoxImage.Information);
                                });
                            }
                        }

                        // Check every 1 second
                        await Task.Delay(1000);
                    }
                }, cancellationTokenSource.Token);
            }catch (Exception ex)
            {
                this.Stop();
                this.Start();
            }
        }



        public void Stop()
        {
            cancellationTokenSource?.Cancel();
        }

        private void btn_update(object sender, MouseButtonEventArgs e)
        {
            var selected = (Alarm)dataListGrid.SelectedItem;

            if (selected != null)
            {
                purposeText.Text = selected.AlarmName;
                hoursTextBox.Text = selected.Timer.Hour.ToString("00");
                minutesTextBox.Text = selected.Timer.Minute.ToString("00");
                secondsTextBox.Text = selected.Timer.Second.ToString("00");

                alarmId.Text = selected.Id.ToString();
            }
        }

        private ObservableCollection<Alarm> RemoveAlarm(Alarm a)
        {
            if(this.listAlarms != default)
            {
                return new ObservableCollection<Alarm>(this.listAlarms.Where(x => x.Id != a.Id));
            }
            return new ObservableCollection<Alarm>(this.alarmService.GetAlarms());
        }
    }
}
