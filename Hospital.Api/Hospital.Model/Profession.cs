using Hospital.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model
{
    public class Profession : CouchDbBaseEntity
    {
        public string Name { get; set; }
    }
}
