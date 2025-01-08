using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonnelSystem.Core.Model;
using PersonnelSystem.Data.Entities;

namespace PersonnelSystem.Data.Repositories
{
    public class DateRepository : IDateRepository
    {
        private readonly PersonnelDbContext _context;
        private ILogger<DateRepository> _logger;
        public DateRepository(PersonnelDbContext context, ILogger<DateRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<DateWorked> Generate(Employee employee, Subdivision subdivision)
        {
            DateWorkedEntity entity = new DateWorkedEntity
            {
                EmployeeId = employee.Id,
                SubdivisionId = subdivision.Id,
                TimeStarted = DateTime.Now.ToUniversalTime().Date,
                TimeFinished = null
            };
            await _context.Dates.AddAsync(entity);
            await _context.SaveChangesAsync();

            DateWorked date = DateWorked.CreateDate(entity.ID, employee, subdivision, entity.TimeStarted, entity.TimeFinished).Date;
            return date;
        }

        public async Task<int> Update(DateWorked dateWorked)
        {
            await _context.Dates
                .Where(d => d.ID == dateWorked.ID)
                .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.TimeStarted, dateWorked.TimeStarted.ToUniversalTime())
                .SetProperty(b => b.TimeFinished, dateWorked.TimeFinished == null ? null : dateWorked.TimeFinished.Value.ToUniversalTime()));
            return dateWorked.ID;
        }

        public async Task<DateWorked?> FindLast(Employee employee, Subdivision subdivision)
        {
            DateWorkedEntity entity = await _context.Dates
                .Where(d => d.EmployeeId == employee.Id && d.SubdivisionId == subdivision.Id)
                .LastAsync();
            if (entity == null)
            {
                return null;
            }
            return DateWorked.CreateDate(
                entity.ID,
                Employee.CreateEmployee(entity.Employee.Id, entity.Employee.Name, entity.Employee.SubdivisionId).Employee,
                Subdivision.CreateSubdivision(entity.Subdivision.Id, entity.Subdivision.Name, entity.Subdivision.CreatedAt).Subdivision,
                entity.TimeStarted,
                entity.TimeFinished).Date;
        }

        public async Task<List<DateWorked>> FindBySubdivisionAndTime(Subdivision subdivision, DateTime start, DateTime end)
        {
            var result = await _context.Dates
                .AsNoTracking()
                .Where(d => d.SubdivisionId == subdivision.Id && (d.TimeFinished == null || d.TimeFinished >= start) && d.TimeStarted <= end)
                .Select(d => DateWorked.CreateDate(
                        d.ID,
                        Employee.CreateEmployee(d.Employee.Id, d.Employee.Name, d.Employee.SubdivisionId).Employee,
                        subdivision,
                        d.TimeStarted,
                        d.TimeFinished).Date)
                .ToListAsync();
            return result;
        }
    }
}
