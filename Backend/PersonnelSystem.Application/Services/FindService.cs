using PersonnelSystem.Core.Model;
using PersonnelSystem.Data.Repositories;

namespace PersonnelSystem.Application.Services
{
    public class FindService : IFindService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISubdivisionRepository _subdivisionRepository;
        public FindService(IEmployeeRepository employeeRepository, ISubdivisionRepository subdivisionRepository)
        {
            _employeeRepository = employeeRepository;
            _subdivisionRepository = subdivisionRepository;
        }

        public async Task<Employee?> FindEmployeeById(int id)
        {
            return await _employeeRepository.Find(id);
        }

        public async Task<Subdivision?> FindEmployeeSubdivision(Employee employee)
        {
            if (employee.SubdivisionId == null)
            {
                return null;
            }
            return await _subdivisionRepository.Find(employee.SubdivisionId.Value);
        }

        public async Task<Subdivision?> FindSubdivisionById(int id)
        {
            return await _subdivisionRepository.Find(id);
        }

        public async Task<List<Subdivision>> GetSubdivisionChilds(Subdivision subdivision)
        {
            return await _subdivisionRepository.GetChilds(subdivision.Id);
        }

        public async Task<List<Employee>> FindEmployeesBySubdivision(Subdivision subdivision)
        {
            return await _employeeRepository.GetBySubdivision(subdivision.Id);
        }
    }
}
