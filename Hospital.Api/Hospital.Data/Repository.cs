using Hospital.Data.Exceptions;
using Hospital.Data.Factories;
using Hospital.Data.IRepositories;
using Hospital.Model.Interfaces;
using MyCouch;
using MyCouch.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task DeleteAsync(TEntity entity)
        {
            using (var client = _couchDb.GetClient())
            {
                var response = await client.Entities.DeleteAsync(entity);
                if (!response.IsSuccess)
                {
                    throw new CouchDbException(response.Error);
                }
            }
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            using (var client = _couchDb.GetClient())
            {
                var response = await client.Entities.GetAsync<TEntity>(id);
                if (response.IsSuccess)
                {
                    return response.Content;
                }
                else
                {
                    if (response.Error == "not_found")
                    {
                        return null;
                    }
                    throw new CouchDbException(response.Error);
                }
            }
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            using (var client = _couchDb.GetClient())
            {
                var response = await client.Entities.PostAsync(entity);
                if (response.IsSuccess)
                {
                    return response.Content;
                }
                else
                {
                    throw new CouchDbException(response.Error);
                }
            }
        }

        public async Task<IEnumerable<TEntity>> ListAsync(int? limit = null)
        {
            using (var client = _couchDb.GetClient())
            {
                var query = new QueryViewRequest("all", "list");
                query.Key = typeof(TEntity).Name.ToLower();
                query.Reduce = false;
                if(limit.HasValue)
                {
                    query.Limit= limit.Value;
                }
                var response = await client.Views.QueryAsync<TEntity>(query);
                if (response.IsSuccess)
                {
                    return response.Rows.Select(r => r.Value);
                }
                else
                {
                    throw new CouchDbException(response.Error);
                }
            }
        }
        
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using (var client = _couchDb.GetClient())
            {
                var response = await client.Entities.PutAsync(entity);
                if (response.IsSuccess)
                {
                    return response.Content;
                }
                else
                {
                    throw new CouchDbException(response.Error);
                }
            }
        }
    }

}
