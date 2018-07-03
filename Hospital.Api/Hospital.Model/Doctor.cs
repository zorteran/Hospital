using Hospital.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model
{
    class Doctor : ICouchDbEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Proffesion> Proffesions { get; set; }

    }
}
