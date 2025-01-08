using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelSystem.Data.Entities
{
    public class SubdivisionEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<EmployeeEntity> Employees { get; set; } = new List<EmployeeEntity>();

        public int? HeaderId { get; set; }
        public SubdivisionEntity? Header { get; set; }

        public ICollection<SubdivisionEntity> Childs { get; set; } = new List<SubdivisionEntity>();

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<DateWorkedEntity> Dates {  get; set; } = new List<DateWorkedEntity>();
    }
}
