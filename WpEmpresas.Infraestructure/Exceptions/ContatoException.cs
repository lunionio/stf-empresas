using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpEmpresas.Infraestructure.Exceptions
{
    public class ContatoException : Exception
    {
        public ContatoException()
        {
        }

        public ContatoException(string message) : base(message)
        {
        }

        public ContatoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ContatoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
