using System;
using System.Collections.Generic;
using System.Text;

namespace WpEmpresas.Entities
{
    public class EmpresaXEspecialidade
    {
        public EmpresaXEspecialidade(int empresaId, int especialidadeId)
        {
            EmpresaId = empresaId;
            EspecialidadeId = especialidadeId;
        }

        public EmpresaXEspecialidade()
        {

        }

        public EmpresaXEspecialidade(int iD, int empresaId, int especialidadeId)
        {
            ID = iD;
            EmpresaId = empresaId;
            EspecialidadeId = especialidadeId;
        }

        public int ID { get; set; }
        public int EmpresaId { get; set; }
        public int EspecialidadeId { get; set; }
    }
}
