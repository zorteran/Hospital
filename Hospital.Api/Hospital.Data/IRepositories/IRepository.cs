using Hospital.Model.Interfaces;
using System.Collections.Generic;

namespace Hospital.Data.IRepositories
{
    public interface IRepository<TEntity> where TEntity : ICouchDbEntity
    {
        IEnumerable<TEntity> List();
        TEntity GetById(int id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
    }
}
