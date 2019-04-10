using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpEmpresas.Infraestructure.Exceptions
{
    public class EspecialidadeException : Exception
    {
        public EspecialidadeException()
        {
        }

        public EspecialidadeException(string message) : base(message)
        {
        }

        public EspecialidadeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EspecialidadeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
