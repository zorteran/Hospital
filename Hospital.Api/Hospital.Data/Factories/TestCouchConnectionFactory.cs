using MyCouch;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Data.Factories
{
    public class TestCouchConnectionFactory : ICouchConnectionFactory
    {
        private static string ServerAddress = "http://localhost:5984";
        private static string DbName = "Test-Hospital-Db";

        public TestCouchConnectionFactory()
        {
            DbName = DbName.ToLower();
            using (var client = new MyCouchServerClient(ServerAddress))
            {

                var response = client.Databases.GetAsync(DbName).Result;
                if (!response.IsSuccess)
                {
                    var result = client.Databases.PutAsync(DbName).Result;
                    if (!result.IsSuccess)
                    {
                        throw new Exception("Problem z utworzeniem bazy danych");
                    }
                }

            }
        }
        public MyCouchClient GetClient() => new MyCouchClient(ServerAddress, DbName);
        public MyCouchStore GetStore() => new MyCouchStore(ServerAddress, DbName);
    }
}
