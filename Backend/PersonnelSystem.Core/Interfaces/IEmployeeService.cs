namespace PersonnelSystem.Application.Services
{
    public interface IEmployeeService
    {
        Task<(int id, string error)> CreateEmployee(string name, int subdivisionId);
        Task<string> FireEmployee(int employeeId);
        Task<string> MoveEmployee(int employeeId, int newSubdivisionId);
    }
}