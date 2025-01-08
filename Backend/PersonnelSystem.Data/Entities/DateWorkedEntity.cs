using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelSystem.Data.Entities
{
    public class DateWorkedEntity
    {
        public int ID { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeEntity Employee { get; set; }
        public int SubdivisionId { get; set; }
        public SubdivisionEntity Subdivision { get; set; }
        public DateTime TimeStarted { get; set; } = DateTime.Now.ToUniversalTime().Date;
        public DateTime? TimeFinished { get; set; }
    }
}
