using System;
using System.Collections.Generic;
using System.Text;

namespace WpEmpresas.Entities
{
    public class Usuario : Base
    {
        public string Login { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string VAdmin { get; set; }
        public string Avatar { get; set; }
        public int IdEmpresa { get; set; }
        public bool? Termo { get; set; }
    }
}
