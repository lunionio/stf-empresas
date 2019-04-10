using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpEmpresas.Domains.Generics;
using WpEmpresas.Entities;
using WpEmpresas.Infraestructure;
using WpEmpresas.Infraestructure.Exceptions;
using WpEmpresas.Services;

namespace WpEmpresas.Domains
{
    public class EmpresaDomain : IDomain<Empresa>
    {
        private readonly EmpresaRepository _repository;
        private readonly SegurancaService _segService;

        public EmpresaDomain(EmpresaRepository repository, SegurancaService segService)
        {
            _repository = repository;
            _segService = segService;
        }

        public async Task DeleteAsync(Empresa entity, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                var empresa = _repository.GetList(e => e.ID.Equals(entity.ID)).SingleOrDefault();
                empresa.Status = 9;
                empresa.Ativo = false;
                _repository.Update(empresa);
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
                throw new EmpresaException("Não foi possível remover a empresa informada.", e);
            }
        }

        public async Task<IEnumerable<Empresa>> GetAllAsync(int idCliente, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                var result = _repository.GetList(e => e.IdCliente.Equals(idCliente));

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
                throw new EmpresaException("Não foi possível recuperar a lista de empresas.", e);
            }
        }

        public async Task<IEnumerable<Empresa>> GetAllAsync(int idCliente, int tipoId, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                var result = _repository.GetList(e => e.IdCliente.Equals(idCliente) && e.TipoEmpresaId.Equals(tipoId));

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
                throw new EmpresaException("Não foi possível recuperar a lista de empresas.", e);
            }
        }

        public async Task<Empresa> GetByIdAsync(int entityId, int idCliente, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                var result = _repository.GetList(e => e.ID.Equals(entityId) && e.IdCliente.Equals(idCliente)).SingleOrDefault();

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
                throw new EmpresaException("Não foi possível recuperar a empresa solicitada.", e);
            }
        }

        public async Task<Empresa> SaveAsync(Empresa entity, string token)
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
                        entity.Endereco.EmpresaId = entity.ID;

                        if (entity.Telefone != null)
                        {
                            entity.Telefone.EmpresaId = entity.ID;
                        }

                        if (entity.Contatos != null)
                        {
                            foreach (var c in entity.Contatos)
                            {
                                c.EmpresaId = entity.ID;
                            }
                        }
                        break;
                    default:
                        entity = await UpdateAsync(entity, token);
                        break;
                }

                return entity;
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
                throw new EmpresaException("Não foi possível salvar a empresa fornecida. Entre em contato com o suporte.", e);
            }
        }

        public async Task<Empresa> UpdateAsync(Empresa entity, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                entity.DateAlteracao = DateTime.UtcNow;
                _repository.Update(entity);

                return entity;
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
                throw new EmpresaException("Não foi possível atualizar a empresa informada. Entre em contato com o suporte.", e);
            }
        }
    }
}