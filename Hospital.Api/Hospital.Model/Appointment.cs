using Hospital.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model
{
    public class Appointment : ICouchDbEntity
    {
        public string PurposeOfVisit { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Room Room { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Done { get; set; }

    }
}
