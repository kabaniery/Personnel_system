using PersonnelSystem.Core.Model;

namespace PersonnelSystem.Application.Services
{
    public interface IDateService
    {
        Task<string> CompleteWork(Employee employee);
        Task<List<Employee>> GetEmployeesByTime(int subdivisionId, DateTime start, DateTime end);
        Task<List<Subdivision>> GetSubdivisionByTime(DateTime time);
        Task<DateWorked> StoreDate(Employee employee, Subdivision subdivision);
    }
}