using PersonnelSystem.Core.Model;

namespace PersonnelSystem.Data.Repositories
{
    public interface ISubdivisionRepository
    {
        Task<int> Create(Subdivision subdivision);
        Task<Subdivision?> Find(int id);
        Task<List<Subdivision>> GetAll();
        Task<List<Subdivision>> GetBy(DateTime date);
        Task<List<Subdivision>> GetChilds(int subdivisionId);
        Task<List<Subdivision>> GetHead();
    }
}