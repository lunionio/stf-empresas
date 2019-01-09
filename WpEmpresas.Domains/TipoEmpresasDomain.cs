using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpEmpresas.Domains.Generics;
using WpEmpresas.Entities;
using WpEmpresas.Infraestructure;
using WpEmpresas.Infraestructure.Exceptions;
using WpEmpresas.Services;

namespace WpEmpresas.Domains
{
    public class TipoEmpresasDomain : IDomain<TipoEmpresa>
    {
        private readonly TipoEmpresaRepository _repository;
        private readonly SegurancaService _segService;

        public TipoEmpresasDomain(TipoEmpresaRepository repository, SegurancaService segService)
        {
            _repository = repository;
            _segService = segService;
        }

        public Task DeleteAsync(TipoEmpresa entity, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TipoEmpresa>> GetAllAsync(int idCliente, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                var result = _repository.GetList(te => te.IdCliente.Equals(idCliente) && te.Ativo);

                return result;
            }
            catch (InvalidTokenException e)
            {
                throw e;
            }
            catch (ServiceException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new EmpresaException("Não foi possível recuperar a lista de tipos de empresas.", e);
            }
        }

        public async Task<IEnumerable<TipoEmpresa>> GetAllAsync(IEnumerable<int> ids, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                var result = _repository.GetList(te => ids.Contains(te.ID) && te.Ativo);

                return result;
            }
            catch (InvalidTokenException e)
            {
                throw e;
            }
            catch (ServiceException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new EmpresaException("Não foi possível recuperar a lista de tipos de empresas.", e);
            }
        }

        public async Task<TipoEmpresa> GetByIdAsync(int entityId, int idCliente, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                var result = _repository.GetList(t => t.ID.Equals(entityId)
                        && t.Ativo && t.IdCliente.Equals(idCliente)).SingleOrDefault();

                return result;
            }
            catch (InvalidTokenException e)
            {
                throw e;
            }
            catch (ServiceException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new EmpresaException("Não foi possível recuperar o tipo da empresa solicitado.", e);
            }
        }

        public Task<TipoEmpresa> SaveAsync(TipoEmpresa entity, string token)
        {
            throw new NotImplementedException();
        }

        public Task<TipoEmpresa> UpdateAsync(TipoEmpresa entity, string token)
        {
            throw new NotImplementedException();
        }
    }
}
