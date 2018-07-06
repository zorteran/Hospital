using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Interfaces
{
    public interface IPatientService
    {
        Task<Patient> AddDoctor(Patient doc);
        Task<Patient> GetDoctor(string id);
        Task<Patient> DeleteDoctor(Patient doc);
        Task<Patient> UpdateDoctor(Patient doc);
    }
}
