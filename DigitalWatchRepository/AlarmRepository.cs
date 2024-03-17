using DigitalWatchBO.Models;
using DigitalWatchDAO;
using DigitalWatchRepository.Interface;

namespace DigitalWatchRepository
{
    public class AlarmRepository : IAlarmrepository
    {
        private AlarmDAO _dao;

        public AlarmRepository()
        {
            this._dao = new AlarmDAO();
        }

        public Alarm Create(Alarm a)
        => this._dao.CreateAlarm(a);

        public List<Alarm> GetAlarms()
        => this._dao.GetAlarms();

        public bool Remove(Alarm selected)
        => this._dao.Remove(selected);

        public Alarm Update(Alarm a)
        => this._dao.UpdateAlarm(a);
    }
}
