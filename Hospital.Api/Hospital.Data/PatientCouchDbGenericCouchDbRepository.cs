using System;
using System.Collections.Generic;
using System.Text;
using Hospital.Data.DbManagers;
using Hospital.Data.IRepositories;
using Hospital.Model;

namespace Hospital.Data
{
    public class PatientCouchDbGenericCouchDbRepository: GenericCouchDbRepository<Patient>, IPatientRepository
    {
        public PatientCouchDbGenericCouchDbRepository(ICouchDbManager couch) : base(couch)
        {
        }
    }
}
