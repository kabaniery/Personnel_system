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
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDateRepository _dateRepository;
        private readonly IFindService _findService;
        private readonly ILogger<EmployeeService> _logger;
        public EmployeeService(IEmployeeRepository employeeRepository, IFindService findService, ILogger<EmployeeService> logger, IDateRepository dateRepository)
        {
            _employeeRepository = employeeRepository;
            _findService = findService;
            _logger = logger;
            _dateRepository = dateRepository;
        }

        public async Task<(int id, string error)> CreateEmployee(string name, int subdivisionId)
        {
            Subdivision? subdivision = await _findService.FindSubdivisionById(subdivisionId);
            if (subdivision == null)
            {
                return (0, "Subdivision is incorrect");
            }
            var (employee, error) = Employee.CreateEmployee(0, name, subdivisionId);
            if (!string.IsNullOrEmpty(error))
            {
                return (0, error);
            }
            var result = (await _employeeRepository.Create(employee), string.Empty);
            await _dateRepository.Generate(employee, subdivision);
            return result;
        }

        public async Task<string> FireEmployee(int employeeId)
        {
            Employee? employee = await _findService.FindEmployeeById(employeeId);
            if (employee == null)
            {
                return "Employee id is invalid";
            }
            if (employee.SubdivisionId == null)
            {
                return "Employee is already Fired";
            }
            Subdivision? subdivision = await _findService.FindSubdivisionById(employee.SubdivisionId.Value);
            if (subdivision != null)
            { 
                DateWorked? date = await _dateRepository.FindLast(employee, subdivision);
                if (date != null && date.CompleteEntity())
                {
                    await _dateRepository.Update(date);
                }
            }
            await _employeeRepository.Update(employee.Id, employee.Name, null);
            return string.Empty;
        }

        public async Task<string> MoveEmployee(int employeeId, int newSubdivisionId)
        {
            Employee? employee = await _findService.FindEmployeeById(employeeId);
            if (employee == null)
            {
                return "Employee id is invalid";
            }
            if (employee.SubdivisionId == null)
            {
                return "Employee is fired";
            }
            Subdivision? subdivision = await _findService.FindSubdivisionById(newSubdivisionId);
            if (subdivision == null)
            {
                return "Subdivision id is invalid";
            }
            DateWorked? date = await _dateRepository.FindLast(employee, subdivision);
            if (date != null && date.CompleteEntity())
            {
                await _dateRepository.Update(date);
            }
            await _employeeRepository.Update(employee.Id, employee.Name, subdivision.Id);
            return string.Empty;
        }
    }
}
