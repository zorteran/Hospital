using Hospital.Model.Interfaces;
using System.Collections.Generic;

namespace Hospital.Model
{
    public class Room : CouchDbBaseEntity
    {

        public string Number { get; set; }
        public ICollection<Profession> PermittedProffesions { get; set; }
    }
}