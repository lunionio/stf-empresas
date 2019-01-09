using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpEmpresas.Infraestructure.Exceptions
{
    public class TelefoneException : Exception
    {
        public TelefoneException()
        {
        }

        public TelefoneException(string message) : base(message)
        {
        }

        public TelefoneException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TelefoneException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
