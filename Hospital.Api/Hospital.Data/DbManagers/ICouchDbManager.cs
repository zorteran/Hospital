using MyCouch;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Data.DbManagers
{
    public interface ICouchDbManager
    {
        MyCouchClient GetClient();
        MyCouchStore GetStore();
        void EnsureViewsCreated();
        void EnsureDbCreated();

    }
}
