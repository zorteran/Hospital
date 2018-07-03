using Hospital.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model
{
    public class Hospital : ICouchDbEntity
    {
        public string Name { get; set; }
        public ICollection<string> Address { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
    }
}
