using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpEmpresas.Infraestructure.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException()
        {
        }

        public InvalidTokenException(string message) : base(message)
        {
        }

        public InvalidTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
