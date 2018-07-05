using Hospital.Model.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Data.IRepositories
{
    public interface IRepository<TEntity> where TEntity : ICouchDbEntity
    {
        Task<IEnumerable<TEntity>> ListAsync(int? limit = null);
        Task<TEntity> GetByIdAsync(string id);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        
    }
}
