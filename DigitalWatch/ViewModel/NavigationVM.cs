using DigitalWatch.Utilities;
using DigitalWatch.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Controls;
using System.Windows.Input;

namespace DigitalWatch.ViewModel
{
    class NavigationVM : ViewModelBase
    {
        private UserControl _currentView;
        private UserControl _watch;
        private UserControl _countDown;
        private UserControl _alram;

        public UserControl CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand WatchCommand { get; set; }

        public ICommand CountDownCommand { get; set; }

        public ICommand AlarmCommand { get; set; }

        private void Watch(object obj) => CurrentView = this._watch;
       
        private void CountDown(object obj) => CurrentView = this._countDown;
        private void Alarm(object obj) => CurrentView = this._alram;

        public NavigationVM()
        {
            _watch = new Watch();
            _countDown = new CountDown();
            _alram = new Alarms();

            WatchCommand = new RelayCommand(Watch);
            CountDownCommand = new RelayCommand(CountDown);
            AlarmCommand = new RelayCommand(Alarm);
            // Set CurrentView to an instance of the Watch UserControl
            CurrentView = _watch;
        }
    }
}
