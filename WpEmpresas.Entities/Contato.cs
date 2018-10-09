using System;
using System.Collections.Generic;
using System.Text;

namespace WpEmpresas.Entities
{
    public class Contato : Base
    {
        public Tipo Tipo { get; set; }
        public int TipoId { get; set; }
        public string ContatoInfo { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
    }
}
