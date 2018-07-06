using Hospital.Core.Interfaces;
using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core
{
    public class PatientService : IPatientService
    {
        public Task<Patient> AddDoctor(Patient doc)
        {
            throw new NotImplementedException();
        }

        public Task<Patient> DeleteDoctor(Patient doc)
        {
            throw new NotImplementedException();
        }

        public Task<Patient> GetDoctor(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Patient> UpdateDoctor(Patient doc)
        {
            throw new NotImplementedException();
        }
    }
}
