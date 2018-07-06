using Hospital.Core.Interfaces;
using Hospital.Model;
using System;
using System.Threading.Tasks;
using Hospital.Data.IRepositories;

namespace Hospital.Core
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public async Task<Doctor> AddDoctor(Doctor doc)
        {
            return await _doctorRepository.InsertAsync(doc);
        }

        public async Task DeleteDoctor(Doctor doc)
        {
            await _doctorRepository.DeleteAsync(doc);
        }

        public Task<Doctor> GetDoctor(string id)
        {
            return _doctorRepository.GetByIdAsync(id);
        }

        public Task<Doctor> UpdateDoctor(Doctor doc)
        {
            return _doctorRepository.UpdateAsync(doc);
        }
    }
}
