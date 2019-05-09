using System;
using System.Collections.Generic;
using System.Text;

namespace WpEmpresas.Entities
{
    public class UsuarioXPerfil
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdPerfil { get; set; }
        public int UsuarioCriacao { get; set; }
        public int UsuarioEdicao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataEdicao { get; set; }
    }
}
