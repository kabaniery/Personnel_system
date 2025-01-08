using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelSystem.Data.Entities
{
    public class EmployeeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int? SubdivisionId { get; set; }
        public SubdivisionEntity? Subdivision { get; set; }

        public List<DateWorkedEntity> Dates {  get; set; } = new List<DateWorkedEntity>();
    }
}
