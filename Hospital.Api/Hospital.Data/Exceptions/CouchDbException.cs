using System;
using System.Runtime.Serialization;

namespace Hospital.Data.Exceptions
{
    [Serializable]
    public class CouchDbException : Exception
    {
        public CouchDbException()
        {
        }

        public CouchDbException(string message) : base(message)
        {
        }

        public CouchDbException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CouchDbException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}