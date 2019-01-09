using System;
using System.Collections.Generic;
using System.Text;

namespace WpEmpresas.Entities
{
    public class Telefone : Base
    {
        public string Numero { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
    }
}
