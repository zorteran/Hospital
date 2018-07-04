using Hospital.Model.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Data.IRepositories
{
    public interface IRepository<TEntity> where TEntity : ICouchDbEntity
    {
        IEnumerable<TEntity> List();
        Task<TEntity> GetByIdAsync(string id);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(string id);
    }
}
