using Hospital.Data.Factories;
using Hospital.Data.IRepositories;
using Hospital.Model.Interfaces;
using MyCouch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : ICouchDbEntity
    {
        private readonly ICouchConnectionFactory _couchDb;
        public Repository(ICouchConnectionFactory couch)
        {
            _couchDb = couch;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            using (var store = _couchDb.GetStore())
            {
                return await store.DeleteAsync(id);
            }
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            using (var store = _couchDb.GetStore())
            {
                return await store.GetByIdAsync<TEntity>(id);
            }
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            using (var store = _couchDb.GetStore())
            {
                return await store.StoreAsync(entity);
            }
        }

        public IEnumerable<TEntity> List()
        {
            throw new NotImplementedException();
            using (var store = _couchDb.GetStore())
            {
                //return store.QueryAsync<TEntity>(new Query());
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using (var store = _couchDb.GetStore())
            {
                return await store.StoreAsync(entity);

            }
        }
    }

}
