using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WpEmpresas.Entities
{
    public class Empresa : Base
    {
        public string CNAE_S { get; set; }
        public string RazaoSocial { get; set; }
        [Required]
        public string CNPJ { get; set; }
        public Endereco Endereco { get; set; }
        public IList<Contato> Contatos { get; set; }
        public string Email { get; set; }
        public Telefone Telefone { get; set; }
        public int CodigoExterno { get; set; }
        public int TipoEmpresaId { get; set; }
        public TipoEmpresa TipoEmpresa { get; set; }

        public string NomeResponsavel { get; set; }
        public string EmailResponsavel { get; set; }
        public string CpfResponsavel { get; set; }

        public int ResponsavelId { get; set; }
        public Responsavel Responsavel { get; set; }

        [NotMapped]
        public int EspecialidadeId { get; set; }
        [NotMapped]
        public Especialidade Especialidade { get; set; }
        [NotMapped]
        public string Origem { get; set; }
        [NotMapped]
        public int Tipo { get; set; }
    }
}
