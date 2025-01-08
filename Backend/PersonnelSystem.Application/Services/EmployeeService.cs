using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonnelSystem.Core.Model;
using PersonnelSystem.Data.Repositories;

namespace PersonnelSystem.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFindService _findService;
        public EmployeeService(IEmployeeRepository employeeRepository, IFindService findService)
        {
            _employeeRepository = employeeRepository;
            _findService = findService;
        }

        public async Task<(int id, string error)> CreateEmployee(string name, int subdivisionId)
        {
            Subdivision? subdivision = await _findService.FindSubdivisionById(subdivisionId);
            if (subdivision == null)
            {
                return (0, "Subdivision is incorrect");
            }
            var (employee, error) = Employee.CreateEmployee(0, name, subdivisionId);
            if (error != null)
            {
                return (0, error);
            }
            
            return (await _employeeRepository.Create(employee), string.Empty);
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
            await _employeeRepository.Update(employee.Id, employee.Name, subdivision.Id);
            return string.Empty;
        }
    }
}
