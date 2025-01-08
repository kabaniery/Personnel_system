using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PersonnelSystem.Core.Model;
using PersonnelSystem.Data.Repositories;

namespace PersonnelSystem.Application.Services
{
    public class DateService : IDateService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDateRepository _dateRepository;
        private readonly ISubdivisionRepository _subdivisionRepository;
        private readonly ILogger<DateService> _logger;
        public DateService(IDateRepository dateRepository, ISubdivisionRepository subdivisionRepository, IEmployeeRepository employeeRepository, ILogger<DateService> logger)
        {
            _dateRepository = dateRepository;
            _subdivisionRepository = subdivisionRepository;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<DateWorked> StoreDate(Employee employee, Subdivision subdivision)
        {
            return await _dateRepository.Generate(employee, subdivision);
        }

        public async Task<string> CompleteWork(Employee employee)
        {
            if (employee.SubdivisionId == null)
            {
                return "Employee is fired";
            }

            int subdivisionId = employee.SubdivisionId.Value;
            Subdivision? subdivision = await _subdivisionRepository.Find(subdivisionId);
            if (subdivision == null)
            {
                return "Internal error";
            }

            DateWorked? date = await _dateRepository.FindLast(employee, subdivision);
            if (date == null)
            {
                return "Internal error";
            }
            if (!date.CompleteEntity())
            {
                await _employeeRepository.Update(employee.Id, employee.Name, null);
                return "Employee is already fired";
            }
            return string.Empty;
        }

        public async Task<List<Employee>> GetEmployeesByTime(int subdivisionId, DateTime start, DateTime end)
        {
            Subdivision? subdivision = await _subdivisionRepository.Find(subdivisionId);
            if (subdivision == null)
            {
                return new List<Employee>();
            }
            return  (await _dateRepository.FindBySubdivisionAndTime(subdivision, start, end))
                .Select(d => d.Employee)
                .ToList();
        }

        public async Task<List<Subdivision>> GetSubdivisionByTime(DateTime time)
        {
            return await _subdivisionRepository.GetBy(time);
        }
    }
}
