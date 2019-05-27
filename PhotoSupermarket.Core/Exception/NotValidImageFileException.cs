using System;
using System.Runtime.Serialization;

namespace PhotoSupermarket.Core.Util
{
    [Serializable]
    internal class NotValidImageFileException : Exception
    {
        public NotValidImageFileException()
        {
        }

        public NotValidImageFileException(string message) : base(message)
        {
        }

        public NotValidImageFileException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotValidImageFileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}