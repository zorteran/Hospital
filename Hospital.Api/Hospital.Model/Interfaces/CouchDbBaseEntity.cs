using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model.Interfaces
{
    public class CouchDbBaseEntity
    {
        public string _id { get; set; }
        public string _rev { get; set; }
    }
}
