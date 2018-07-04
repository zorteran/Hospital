using MyCouch;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Data.Factories
{
    public class CouchConnectionFactory : ICouchConnectionFactory
    {
        private static string ServerAddress = "http://localhost:5984";
        private static string DbName = "DefaultDbName";
        public CouchConnectionFactory()
        {
            ServerAddress = GetDbUrl();
            DbName = GetDbName();
            using (var client = new MyCouchServerClient(Environment.GetEnvironmentVariable("COUCH_DB_URL")))
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

        private static string GetDbUrl()
        {
            return Environment.GetEnvironmentVariable("COUCH_DB_URL");
        }
        private static string GetDbName()
        {
            return (Environment.GetEnvironmentVariable("HOSPITAL_NAME") + "-Db").ToLower();
        }
    }
}
