using DigitalWatchBO.Models;
using DigitalWatchRepository;
using DigitalWatchRepository.Interface;
using DigitalWatchService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWatchService
{
    public class AlarmService : IAlarmService
    {
        private readonly IAlarmrepository alarmrepository;

        public AlarmService()
        {
            this.alarmrepository = new AlarmRepository();
        }

        public Alarm Create(Alarm a)
        => this.alarmrepository.Create(a);

        public List<Alarm> GetAlarms()
        => this.alarmrepository.GetAlarms();

        public bool Remove(Alarm selected)
        =>this.alarmrepository.Remove(selected);

        public Alarm Update(Alarm a)
        => this.alarmrepository.Update(a);
    }
}
