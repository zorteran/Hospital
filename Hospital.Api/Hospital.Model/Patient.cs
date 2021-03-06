﻿using Hospital.Model.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Hospital.Model
{
    public class Patient : CouchDbBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<string> Address { get; set; }
        public bool NfzInsurance { get; set; }
        public DateTime NfzInsuranceValidDate { get; set; }

        public Patient()
        {
            Address = new List<string>();
        }
    }
}
