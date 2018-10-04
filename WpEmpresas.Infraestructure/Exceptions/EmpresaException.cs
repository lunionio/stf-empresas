using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpEmpresas.Infraestructure.Exceptions
{
    public class EmpresaException : Exception
    {
        public EmpresaException()
        {
        }

        public EmpresaException(string message) : base(message)
        {
        }

        public EmpresaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmpresaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
