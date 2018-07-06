using Hospital.Core.Interfaces;
using Hospital.Model;
using System;
using System.Threading.Tasks;

namespace Hospital.Core
{
    public class DoctorService : IDoctorService
    {

        public DoctorService()
        {

        }
        public Task<Doctor> AddDoctor(Doctor doc)
        {
            throw new NotImplementedException();
        }

        public Task<Doctor> DeleteDoctor(Doctor doc)
        {
            throw new NotImplementedException();
        }

        public Task<Doctor> GetDoctor(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Doctor> UpdateDoctor(Doctor doc)
        {
            throw new NotImplementedException();
        }
    }
}
