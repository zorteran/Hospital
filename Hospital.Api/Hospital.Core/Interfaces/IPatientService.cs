using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Interfaces
{
    public interface IPatientService
    {
        Task<Patient> AddPatient(Patient doc);
        Task<Patient> GetPatient(string id);
        Task DeletePatient(Patient doc);
        Task<Patient> UpdatePatient(Patient doc);
        Task<IEnumerable<Patient>> GetAllPatients(int? limit);
    }
}
