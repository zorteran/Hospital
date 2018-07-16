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


        public async Task<Patient> AddPatient(Patient doc)
        {
            return await _patientRepository.InsertAsync(doc);
        }

        public Task<Patient> GetPatient(string id)
        {
            return _patientRepository.GetByIdAsync(id);
        }

        public async Task DeletePatient(Patient doc)
        {
            await _patientRepository.DeleteAsync(doc);
        }

        public async Task<Patient> UpdatePatient(Patient doc)
        {
            return await _patientRepository.UpdateAsync(doc);
        }

        public async Task<IEnumerable<Patient>> GetAllPatients(int? limit)
        {
            return await _patientRepository.ListAsync(limit);
        }
    }
}
