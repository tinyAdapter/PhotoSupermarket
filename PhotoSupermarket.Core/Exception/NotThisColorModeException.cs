using System;
using System.Runtime.Serialization;

namespace PhotoSupermarket.Core
{
    [Serializable]
    public class NotThisColorModeException : Exception
    {
        public NotThisColorModeException()
        {
        }

        public NotThisColorModeException(string message) : base(message)
        {
        }

        public NotThisColorModeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotThisColorModeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}