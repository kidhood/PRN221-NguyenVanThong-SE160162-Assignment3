using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for Watch.xaml
    /// </summary>
    public partial class Watch : UserControl
    {
        private DispatcherTimer timer;
        public Watch()
        {
            InitializeComponent();
            InitializeTimer();
        }
        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateTimeText();
        }

        private void UpdateTimeText()
        {
            DateTime currentTime = DateTime.Now;
            timeText.Content = currentTime.ToString("HH:mm:ss");
        }
    }
}
