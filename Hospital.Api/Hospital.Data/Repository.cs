using Hospital.Data.IRepositories;
using Hospital.Model.Interfaces;
using MyCouch;
using System;
using System.Collections.Generic;

namespace Hospital.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : ICouchDbEntity
    {
        public Repository()
        {
            //using (var client = new MyCouchClient("http://127/0/0/1:5984", "testdb" ))
            //{
            //    client.
            //}
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> List()
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
