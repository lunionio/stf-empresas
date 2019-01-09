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
    public class TelefoneDomain : IDomain<Telefone>
    {
        private readonly TelefoneRepository _repository;
        private readonly SegurancaService _segService;

        public TelefoneDomain(TelefoneRepository repository, SegurancaService segService)
        {
            _repository = repository;
            _segService = segService;
        }

        public Task DeleteAsync(Telefone entity, string token)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int empresaId, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                var telefone = _repository.GetList(t => t.EmpresaId.Equals(empresaId)).SingleOrDefault();

                if (telefone != null)
                {
                    telefone.Status = 9;
                    telefone.Ativo = false;
                    _repository.Update(telefone);
                }
            }
            catch (ServiceException e)
            {
                throw e;
            }
            catch (InvalidTokenException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new EnderecoException("Não foi possível remover o endereço da oportunidade.", e);
            }
        }

        public Task<IEnumerable<Telefone>> GetAllAsync(int idCliente, string token)
        {
            throw new NotImplementedException();
        }

        public Task<Telefone> GetByIdAsync(int entityId, int idCliente, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<Telefone> SaveAsync(Telefone entity, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);

                switch (entity.ID)
                {
                    case 0:
                        entity.DataCriacao = DateTime.UtcNow;
                        entity.DateAlteracao = DateTime.UtcNow;
                        entity.Ativo = true;
                        entity.ID = _repository.Add(entity);
                        break;
                    default:
                        entity = await UpdateAsync(entity, token);
                        break;
                }

                return entity;
            }
            catch (ServiceException e)
            {
                throw e;
            }
            catch (InvalidTokenException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new TelefoneException("Não foi possível salvar o telefone da empresa. Entre em contato com o suporte.", e);
            }
        }

        public async Task<Telefone> UpdateAsync(Telefone entity, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                entity.DateAlteracao = DateTime.UtcNow;
                _repository.Update(entity);

                return entity;
            }
            catch (ServiceException e)
            {
                throw e;
            }
            catch (InvalidTokenException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new TelefoneException("Não foi possível atualizar o telefone da empresa. Entre em contato com o suporte.", e);
            }
        }

        public async Task<IEnumerable<Telefone>> GetAllAsync(IEnumerable<int> empresasIds, string token)
        {
            try
            {
                //await _segService.ValidateTokenAsync(token);
                var telefones = _repository.GetList(t => empresasIds.Contains(t.EmpresaId) && t.Ativo);

                return telefones;
            }
            catch (ServiceException e)
            {
                throw e;
            }
            catch (InvalidTokenException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new TelefoneException("Não foi possível recuperar o telefone.", e);
            }
        }

        public async Task<Telefone> GetByEmpresaIdAsync(int empresaId, int idCliente, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);

                var telefone = _repository.GetList(t => t.EmpresaId.Equals(empresaId)
                                        && t.IdCliente.Equals(idCliente)).SingleOrDefault();
                return telefone;
            }
            catch (ServiceException e)
            {
                throw e;
            }
            catch (InvalidTokenException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new TelefoneException("Não foi possível recuperar o telefone da empresa. Entre em contato com o suporte.", e);
            }
        }
    }
}
