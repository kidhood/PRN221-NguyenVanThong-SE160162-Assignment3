using DigitalWatchBO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWatchDAO
{
    public class AlarmDAO
    {
        private DigitalWatchContext _db;

        public AlarmDAO()
        {
            this._db = new DigitalWatchContext();
        }

        public List<Alarm> GetAlarms()
        => this._db.Alarms.AsNoTracking().ToList();

        public Alarm UpdateAlarm(Alarm a)
        {
            try
            {
                this._db.Update(a);
                this._db.SaveChanges();
                this._db.ChangeTracker.Clear();
                return a;
            }catch (Exception ex)
            {
                return null;
            }
        }

        public Alarm CreateAlarm(Alarm a)
        {
            try
            {
                this._db.Add(a);
                this._db.SaveChanges();
                this._db.ChangeTracker.Clear();
                return a;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Remove(Alarm selected)
        {
            try
            {
                this._db.Remove(selected);
                this._db.SaveChanges();
                this._db.ChangeTracker.Clear();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
