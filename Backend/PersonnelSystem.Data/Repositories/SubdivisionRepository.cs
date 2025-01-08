using Microsoft.EntityFrameworkCore;
using PersonnelSystem.Core.Model;
using PersonnelSystem.Data.Entities;

namespace PersonnelSystem.Data.Repositories
{
    public class SubdivisionRepository : ISubdivisionRepository
    {
        private readonly PersonnelDbContext _context;
        public SubdivisionRepository(PersonnelDbContext context)
        { _context = context; }

        public async Task<int> Create(Subdivision subdivision)
        {
            var subdivsion = new SubdivisionEntity
            {
                Id = subdivision.Id,
                Name = subdivision.Name
            };

            await _context.AddAsync(subdivision);
            await _context.SaveChangesAsync();

            return subdivision.Id;
        }

        public async Task<List<Subdivision>> GetHead()
        {
            var entities = await _context.Subdivisions
                .AsNoTracking()
                .Where(s => s.Header == null)
                .ToListAsync();

            return entities.Select(s => Subdivision.CreateSubdivision(
                s.Id,
                s.Name,
                s.CreatedAt,
                s.HeaderId,
                s.Childs.Select(c => c.Id).ToList()
                ).Subdivision).ToList();
        }

        public async Task<List<Subdivision>> GetChilds(int subdivisionId)
        {
            var entities = await _context.Subdivisions
                .AsNoTracking()
                .Where(s => s.HeaderId == subdivisionId)
                .ToListAsync();

            return entities.Select(s => Subdivision.CreateSubdivision(
                s.Id,
                s.Name,
                s.CreatedAt,
                s.HeaderId,
                s.Childs.Select(c => c.Id).ToList()
                ).Subdivision).ToList();
        }

        public async Task<Subdivision?> Find(int id)
        {
            SubdivisionEntity? entity = await _context.Subdivisions.FindAsync(id);
            if (entity == null)
                return null;
            return Subdivision.CreateSubdivision(entity.Id, entity.Name, entity.CreatedAt, entity.HeaderId, entity.Childs.Select(c => c.Id).ToList()).Subdivision;
        }

        public async Task<List<Subdivision>> GetBy(DateTime date)
        {
            return await _context.Subdivisions
                .Where(s => s.CreatedAt == date)
                .Select(s => Subdivision.CreateSubdivision(s.Id, s.Name, s.CreatedAt).Subdivision)
                .ToListAsync();
        }
    }
}
