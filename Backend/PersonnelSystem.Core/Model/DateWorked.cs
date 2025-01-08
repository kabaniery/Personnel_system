using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelSystem.Core.Model
{
    public class DateWorked
    {
        public int ID { get; }
        public Employee Employee { get; }
        public Subdivision Subdivision { get; }
        public DateTime TimeStarted { get; }
        private DateTime? _timeFinished;
        public DateTime? TimeFinished => _timeFinished;

        private DateWorked(int id, Employee employee, Subdivision subdivision, DateTime timeStarted, DateTime? timeFinished) 
        {
            ID = id;
            Employee = employee;
            Subdivision = subdivision;
            TimeStarted = timeStarted;
            _timeFinished = timeFinished;
        }

        public static (DateWorked Date, string error) CreateDate(int id, Employee employee, Subdivision subdivision, DateTime timeStarted, DateTime? timeFinished)
        {
            return (new DateWorked(id, employee, subdivision, timeStarted, timeFinished), string.Empty);
        }

        public bool CompleteEntity()
        {
            if (_timeFinished != null)
            {
                _timeFinished = DateTime.Now.ToUniversalTime().Date;
                return true;
            }
            return false;
        }
    }
}
