using MyCouch;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Data.Factories
{
    public interface ICouchConnectionFactory
    {
        MyCouchClient GetClient();
        MyCouchStore GetStore();

    }
}
