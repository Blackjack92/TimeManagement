using System;

namespace TimeManager.Infrastructure.Data
{
    public class DataStoreException : Exception
    {
        public DataStoreException(string message) : base(message)
        { }
    }
}
