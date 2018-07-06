using Hospital.Core.Interfaces;
using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hospital.Data.IRepositories;

namespace Hospital.Core
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public async Task<Patient> AddDoctor(Patient doc)
        {
            return await _patientRepository.InsertAsync(doc);
        }

        public async Task DeleteDoctor(Patient doc)
        {
            await _patientRepository.DeleteAsync(doc);
        }

        public Task<Patient> GetDoctor(string id)
        {
            return _patientRepository.GetByIdAsync(id);
        }

        public Task<Patient> UpdateDoctor(Patient doc)
        {
            return _patientRepository.UpdateAsync(doc);
        }
    }
}
