using PersonnelSystem.Core.Model;

namespace PersonnelSystem.Data.Repositories
{
    public interface IDateRepository
    {
        Task<List<DateWorked>> FindBySubdivisionAndTime(Subdivision subdivision, DateTime start, DateTime end);
        Task<DateWorked?> FindLast(Employee employee, Subdivision subdivision);
        Task<DateWorked> Generate(Employee employee, Subdivision subdivision);
        Task<int> Update(DateWorked dateWorked);
    }
}