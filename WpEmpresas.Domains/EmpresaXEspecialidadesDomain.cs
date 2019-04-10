using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpEmpresas.Entities;
using WpEmpresas.Infraestructure;
using WpEmpresas.Infraestructure.Exceptions;

namespace WpEmpresas.Domains
{
    public class EmpresaXEspecialidadesDomain
    {
        private readonly EmpresaXEspecialidadeRepository _repository;
        private readonly EspecialidadesDomain _espDomain;

        public EmpresaXEspecialidadesDomain(EmpresaXEspecialidadeRepository repository, EspecialidadesDomain espDomain)
        {
            _repository = repository;
            _espDomain = espDomain;
        }

        public EmpresaXEspecialidade Save(EmpresaXEspecialidade entity)
        {
            try
            {
                switch (entity.ID)
                {
                    case 0:
                        entity.ID = _repository.Add(entity);
                        break;
                    default:
                        entity = Update(entity);
                        break;
                }

                return entity;
            }
            catch (EspecialidadeException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new EspecialidadeException("Não foi possível atualizar o registro informado.", e);
            }
        }

        public EmpresaXEspecialidade Update(EmpresaXEspecialidade entity)
        {
            try
            {
                _repository.Update(entity);
                return entity;
            }
            catch (Exception e)
            {
                throw new EspecialidadeException("Não foi possível atualizar o registro informado.", e);
            }
        }

        public IEnumerable<EmpresaXEspecialidade> GetByEmpresasIds(IEnumerable<int> empresasIds)
        {
            try
            {
                var result = _repository.GetList(mXe => empresasIds.Contains(mXe.EmpresaId));
                return result;
            }
            catch (Exception e)
            {
                throw new EspecialidadeException("Não foi possível recuperar os registros solicitados.", e);
            }
        }

        public Especialidade GetByEmpresaId(int empresaId, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(mXe => mXe.EmpresaId.Equals(empresaId));

                var especialidade = default(Especialidade);

                if (result != null)
                {
                    especialidade = _espDomain.GetById(result.EspecialidadeId, idCliente);
                }

                return especialidade;
            }
            catch (Exception e)
            {
                throw new EspecialidadeException("Não foi possível recuperar os registros solicitados.", e);
            }
        }

        public IEnumerable<EmpresaXEspecialidade> GetByIds(IEnumerable<int> ids, int idCliente)
        {
            try
            {
                var result = _repository.GetList(p => ids.Contains(p.EmpresaId));
                return result;
            }
            catch (Exception e)
            {
                throw new EspecialidadeException("Não foi possível buscar os medicos solicitados.", e);
            }
        }
    }
}
