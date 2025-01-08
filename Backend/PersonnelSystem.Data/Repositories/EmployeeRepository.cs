using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonnelSystem.Core.Model;
using PersonnelSystem.Data.Entities;

namespace PersonnelSystem.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly PersonnelDbContext _context;
        public EmployeeRepository(PersonnelDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetBySubdivision(int subdivisionId)
        {
            var entities = await _context.Employees
                .AsNoTracking()
                .Where(e => e.SubdivisionId == subdivisionId)
                .ToListAsync();

            return entities.Select(e => Employee.CreateEmployee(
                e.Id,
                e.Name,
                e.SubdivisionId
                ).Employee).ToList();
        }

        public async Task<List<Employee>> GetFired()
        {
            var entities = await _context.Employees
                .AsNoTracking()
                .Where(e => e.Subdivision == null)
                .ToListAsync();

            return entities.Select(e => Employee.CreateEmployee(
                e.Id,
                e.Name,
                e.SubdivisionId
                ).Employee).ToList();
        }

        public async Task<int> Create(Employee employee)
        {
            var entity = new EmployeeEntity
            {
                Name = employee.Name,
                SubdivisionId = employee.SubdivisionId,
            };

            await _context.Employees.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> Update(int id, string name, int? subdivisionId)
        {
            await _context.Employees
                .Where(e => e.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Name, b => name)
                    .SetProperty(b => b.SubdivisionId, b => subdivisionId));
            return id;
        }

        public async Task<int> Delete(int id)
        {
            await _context.Employees
                .Where(e => e.Id == id)
                .ExecuteDeleteAsync();
            return id;
        }

        public async Task<Employee?> Find(int id)
        {
            EmployeeEntity? employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return null;
            }
            return Employee.CreateEmployee(employee.Id, employee.Name, employee.SubdivisionId).Employee;
        }

        public Task<int> Create(string name, int? subdivisionId)
        {
            throw new NotImplementedException();
        }
    }
}
