using System.Timers;
using System.Windows;

namespace DigitalWatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //System.Timers.Timer timer;
        //int h, m, s, ms;

        //private void StopBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    timer.Stop();
        //}

        //private void ResetBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    timer.Stop();
        //    h = 0;
        //    m = 0;
        //    s = 0;
        //    ms = 0;
        //    timeText.Content = "00:00:00:00";
        //}

        public MainWindow()
        {
            InitializeComponent();
            //timer = new System.Timers.Timer();
            //timer.Interval = 1;
            //timer.Elapsed += OnTimeEvent;
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        => Close();

        //private void OnTimeEvent(object? sender, ElapsedEventArgs e)
        //{
        //    Dispatcher.Invoke(new Action(() =>
        //    {

        //        ms += 1;
        //        if (ms == 100)
        //        {
        //            ms = 0;
        //            s += 1;
        //        }

        //        if (s == 60)
        //        {
        //            s = 0;
        //            m += 1;

        //        }

        //        if (m == 60)
        //        {
        //            m = 0;
        //            h += 1;
        //        }

        //        UpdateTimeText();
        //    }));

        //}

        //private void UpdateTimeText()
        //{
        //    timeText.Content = string.Format("{0}:{1}:{2}:{3}", h.ToString().PadLeft(2, '0'),
        //                                    m.ToString().PadLeft(2, '0'),
        //                                    s.ToString().PadLeft(2, '0'),
        //                                    ms.ToString().PadLeft(2, '0'));
        //}

        //private void OnTimeCountDownEvent(object sender, ElapsedEventArgs e)
        //{
        //    Dispatcher.Invoke(() =>
        //    {
        //        s--;
        //        if (s < 0)
        //        {
        //            s = 59;
        //            m--;

        //            if (m < 0)
        //            {
        //                m = 59;
        //                h--;

        //                if (h < 0)
        //                {
        //                    timer.Stop();
        //                    MessageBox.Show("Countdown completed!");
        //                }
        //            }
        //        }

        //        UpdateTimeText();
        //    });
        //}


    }
}