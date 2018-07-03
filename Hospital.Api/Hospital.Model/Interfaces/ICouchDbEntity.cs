using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model.Interfaces
{
    public class ICouchDbEntity
    {
        string _id { get; set; }
        string _rev { get; set; }
    }
}
