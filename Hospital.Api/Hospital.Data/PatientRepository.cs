using System;
using System.Collections.Generic;
using System.Text;
using Hospital.Data.Factories;
using Hospital.Data.IRepositories;
using Hospital.Model;

namespace Hospital.Data
{
    public class PatientRepository: Repository<Patient>, IPatientRepository
    {
        public PatientRepository(ICouchConnectionFactory couch) : base(couch)
        {
        }
    }
}
