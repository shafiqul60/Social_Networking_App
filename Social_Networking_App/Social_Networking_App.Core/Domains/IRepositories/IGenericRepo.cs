using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Networking_App.Core.Domains.IRepositories
{
    public interface IGenericRepo<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int? id);
        Task<bool> AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task<bool> Exists(int id);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
