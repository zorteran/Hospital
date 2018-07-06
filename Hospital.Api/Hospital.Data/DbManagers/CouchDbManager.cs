using MyCouch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hospital.Data.Exceptions;

namespace Hospital.Data.DbManagers
{
    public class CouchDbManager : ICouchDbManager
    {
        private static string _serverAddress = "http://localhost:5984";
        private static string _dbName = "DefaultDbName";

        private readonly IDictionary<string, string> _views = new Dictionary<string, string>()
        {
            {
                "_design/all",
                "{\n  \"_id\": \"_design/all\",\n  \"views\": {\n    \"list\": {\n      \"map\": \"function (doc) {\\n  emit(doc.$doctype, doc);\\n}\"\n    }\n  },\n  \"language\": \"javascript\"\n}"
            }
        };

        public CouchDbManager(string url, string dbName)
        {
            _serverAddress = url;
            _dbName = dbName.ToLower();
            EnsureDbCreated();
            EnsureViewsCreated();
        }
        public CouchDbManager()
        {
            SetDbUrlFromEnv();
            SetDbNameFromEnv();
        }
        

        public void EnsureViewsCreated()
        {
            foreach (var view in _views)
            {
                using (var client = GetClient())
                {
                    var getResponse = client.Documents.GetAsync(view.Key).Result;
                    if (!getResponse.IsSuccess)
                    {
                        var postResponse = client.Documents.PostAsync(view.Value).Result;
                        if (!postResponse.IsSuccess)
                        {
                            throw new CouchDbException("Problem z utworzeniem widoków");
                        }
                    }
                }
            }
        }

        public MyCouchClient GetClient() => new MyCouchClient(_serverAddress, _dbName);
        public MyCouchStore GetStore() => new MyCouchStore(_serverAddress, _dbName);
        private static MyCouchServerClient GetCouchServerClient() => new MyCouchServerClient(GetDbUrl());

        private static string GetDbUrl()
        {
            return _serverAddress;
        }
        private static void SetDbUrlFromEnv()
        {
            _serverAddress = Environment.GetEnvironmentVariable("COUCH_DB_URL");
        }
        private static void SetDbNameFromEnv()
        {
            _dbName = (Environment.GetEnvironmentVariable("HOSPITAL_NAME") + "-Db").ToLower();
        }
        private static string GetDbName()
        {
            return _dbName;
        }
        public void EnsureDbCreated()
        {
            using (var client = GetCouchServerClient())
            {
                var response = client.Databases.GetAsync(_dbName).Result;
                if (response.IsSuccess) return;
                var result = client.Databases.PutAsync(_dbName).Result;
                if (!result.IsSuccess)
                {
                    throw new CouchDbException("Problem z utworzeniem bazy danych");
                }
            }
        }

    }
}
