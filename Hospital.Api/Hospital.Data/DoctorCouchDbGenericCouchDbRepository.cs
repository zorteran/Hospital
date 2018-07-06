using System;
using System.Collections.Generic;
using System.Text;
using Hospital.Data.DbManagers;
using Hospital.Data.IRepositories;
using Hospital.Model;

namespace Hospital.Data
{
    public class DoctorCouchDbGenericCouchDbRepository : GenericCouchDbRepository<Doctor>, IDoctorRepository
    {
        public DoctorCouchDbGenericCouchDbRepository(ICouchDbManager couch) : base(couch)
        {
        }
    }
}
