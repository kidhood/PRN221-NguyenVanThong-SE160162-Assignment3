using System;
using System.Collections.Generic;

namespace DigitalWatchBO.Models
{
    public partial class Alarm
    {
        public int Id { get; set; }
        public string AlarmName { get; set; } = null!;
        public DateTime Timer { get; set; }
    }
}
