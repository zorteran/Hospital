using System;
using System.Collections;
using System.Collections.Generic;

namespace Hospital.Model
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<string> Address { get; set; }
        public bool NfzInsurance { get; set; }
        public DateTime NfzInsuranceValidDate { get; set; }
    }
}
