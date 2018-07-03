using System.Collections.Generic;

namespace Hospital.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public ICollection<Proffesion> PermittedProffesions { get; set; }
    }
}