using System;
using System.Runtime.Serialization;

namespace Finovation.Core.Common.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public int Status { get; set; } = 409;

        public EntityAlreadyExistsException()
        {
        }

        public EntityAlreadyExistsException(string message) : base(message)
        {
        }

        public EntityAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
