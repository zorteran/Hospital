using System;
using System.Collections.Generic;
using System.Text;
using Hospital.Data.Factories;
using Hospital.Data.IRepositories;
using Hospital.Model;

namespace Hospital.Data
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(ICouchConnectionFactory couch) : base(couch)
        {
        }
    }
}
