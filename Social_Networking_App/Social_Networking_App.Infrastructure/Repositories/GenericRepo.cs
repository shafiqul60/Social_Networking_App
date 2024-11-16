using Microsoft.EntityFrameworkCore;
using Social_Networking_App.Core.Domains.IRepositories;
using Social_Networking_App.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Networking_App.Infrastructure.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        public GenericRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> AddAsync(T entity)
        {
            await _db.AddAsync(entity);
            var isSaved = await _db.SaveChangesAsync();
            return isSaved > 0;
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await _db.AddRangeAsync(entities);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _db.Set<T>().Remove(entity);
            var isDelete = await _db.SaveChangesAsync();
            return isDelete > 0;

        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return await _db.Set<T>().FindAsync(id);
        }


        public async Task<bool> UpdateAsync(T entity)
        {
            _db.Update(entity);
            var isUpdated = await _db.SaveChangesAsync();
            return isUpdated > 0;
        }
    }
}
