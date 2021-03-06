﻿using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Interfaces
{
    public interface IDoctorService
    {
        Task<Doctor> AddDoctor(Doctor doc);
        Task<Doctor> GetDoctor(string id);
        Task DeleteDoctor(Doctor doc);
        Task<Doctor> UpdateDoctor(Doctor doc);
        Task<IEnumerable<Doctor>> GetAllDoctors(int? limit);
    }
}
