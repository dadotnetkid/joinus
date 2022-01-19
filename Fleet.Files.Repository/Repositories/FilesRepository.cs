

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Files.Repository.Repositories
{
    public interface IFilesRepository
    {
        IQueryable<Entities.Files> GetAll(Expression<Func<Entities.Files, bool>> filter = null);
        Task<Entities.Files?> GetById(int id);
        Task<Entities.Files?> Insert(Entities.Files file);
        Task<Entities.Files?> Update(Entities.Files file);
        Task Delete(int id);
        Task Delete(int[] ids);
    }

    public class FilesRepository : IFilesRepository
    {
        private readonly FileDbContext _db;

        public FilesRepository(FileDbContext db)
        {
            _db = db;
        }

        public IQueryable<Entities.Files> GetAll(Expression<Func<Entities.Files, bool>> filter = null)
        {
            var files = _db.Files.AsQueryable();
            if (filter != null) files = _db.Files.Where(filter);
            return files;
        }
        public Task<Entities.Files?> GetById(int id)
        {
            return _db.Files.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Entities.Files?> Insert(Entities.Files file)
        {
            _db.Files.Add(file);
            await _db.SaveChangesAsync();
            return file;
        }
        public async Task<Entities.Files?> Update(Entities.Files file)
        {
            _db.Update(file);
            await _db.SaveChangesAsync();
            return file;
        }
        public async Task Delete(int id)
        {
            _db.Remove(await _db.Files.FindAsync(id));
            await _db.SaveChangesAsync();
        }
        public async Task Delete(int[] ids)
        {
            foreach (var id in ids)
            {
                _db.Remove(await _db.Files.FindAsync(id));
                await _db.SaveChangesAsync();
            }    
        }
    }
}
