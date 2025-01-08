using System.Text.Json.Nodes;
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

        public async Task<List<Subdivision>> FindAllSubdivision()
        {
            return await _subdivisionRepository.GetAll();
        }
        private async Task<JsonObject> _getFullCompanyInfo(Subdivision subdivision, int level = 0)
        {
            JsonObject companyInfo = new JsonObject();
            companyInfo.Add("id", subdivision.Id);
            companyInfo.Add("name", subdivision.Name);
            List<int> employees = (await _employeeRepository.GetBySubdivision(subdivision.Id)).Select(e => e.Id).ToList();
            JsonArray employeesJson = new JsonArray();
            foreach (int e in employees)
            {
                employeesJson.Add(e);
            }
            companyInfo.Add("employees", employeesJson);

            List<Subdivision> childs = await _subdivisionRepository.GetChilds(subdivision.Id);
            JsonArray childsJson = new JsonArray();
            if (level > 7)
            {
                companyInfo.Add("childs", childsJson);
                return companyInfo;
            }
            foreach (Subdivision child in childs)
            {
                childsJson.Add(_getFullCompanyInfo(child, level + 1));
            }
            companyInfo.Add("childs", childsJson);
            return companyInfo;
        }
        public async Task<JsonArray> GetCompanyStructure()
        {
            JsonArray companyStructure = new JsonArray();
            foreach (Subdivision subdivision in await _subdivisionRepository.GetHead())
            {
                companyStructure.Add(_getFullCompanyInfo(subdivision));
            }
            return companyStructure;
        }
    }
}
