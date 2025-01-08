using PersonnelSystem.Core.Model;

namespace PersonnelSystem.Data.Repositories
{
    public interface IEmployeeRepository
    {
        Task<int> Create(Employee employee);
        Task<int> Delete(int id);
        Task<Employee?> Find(int id);
        Task<List<Employee>> GetBySubdivision(int subdivisionId);
        Task<List<Employee>> GetFired();
        Task<int> Update(int id, string name, int? subdivisionId);
    }
}