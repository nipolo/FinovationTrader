using System;
using System.Runtime.Serialization;

namespace Finovation.Core.Common.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public int Status { get; set; } = 404;

        public AggregateNotFoundException()
        {
        }

        public AggregateNotFoundException(string message) : base(message)
        {
        }

        public AggregateNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AggregateNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
