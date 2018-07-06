using Hospital.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model
{
    public class Doctor : CouchDbBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<string> Professions { get; set; }
        public Doctor()
        {
            Professions = new List<string>();
        }
        public Doctor(Doctor doc)
        {
            _id = doc._id;
            _rev = doc._rev;
            FirstName = doc.FirstName;
            LastName = doc.LastName;
            Professions = doc.Professions;

        }
    }
}
