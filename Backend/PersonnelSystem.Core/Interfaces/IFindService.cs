using System.Text.Json.Nodes;
using PersonnelSystem.Core.Model;

namespace PersonnelSystem.Application.Services
{
    public interface IFindService
    {
        Task<List<Subdivision>> FindAllSubdivision();
        Task<Employee?> FindEmployeeById(int id);
        Task<List<Employee>> FindEmployeesBySubdivision(Subdivision subdivision);
        Task<Subdivision?> FindEmployeeSubdivision(Employee employee);
        Task<Subdivision?> FindSubdivisionById(int id);
        Task<JsonArray> GetCompanyStructure();
        Task<List<Subdivision>> GetSubdivisionChilds(Subdivision subdivision);
    }
}