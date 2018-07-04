using Hospital.Model.Interfaces;
using System.Collections.Generic;

namespace Hospital.Model
{
    public class Room : ICouchDbEntity
    {

        public string Number { get; set; }
        public ICollection<Profession> PermittedProffesions { get; set; }
    }
}