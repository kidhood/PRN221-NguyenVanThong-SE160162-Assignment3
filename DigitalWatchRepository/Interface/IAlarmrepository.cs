using DigitalWatchBO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWatchRepository.Interface
{
    public interface IAlarmrepository
    {
        List<Alarm> GetAlarms();

        Alarm Update(Alarm a);
        Alarm Create(Alarm a);
        bool Remove(Alarm selected);
    }
}
