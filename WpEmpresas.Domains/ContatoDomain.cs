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
    public class ContatoDomain : IDomain<Contato>
    {
        private readonly ContatoRepository _repository;
        private readonly SegurancaService _segService;

        public ContatoDomain(ContatoRepository repository, SegurancaService segService)
        {
            _repository = repository;
            _segService = segService;
        }

        public async Task DeleteAsync(Contato entity, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                entity.Status = 9;
                entity.Ativo = false;
                _repository.Update(entity);
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
                throw new ContatoException("Não foi possível remover o contato da empresa.", e);
            }
        }

        public async Task DeleteAsync(int empresaId, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);

                var contatos = _repository.GetList(c => c.EmpresaId.Equals(empresaId));

                if (contatos != null && contatos.Count > 0)
                {
                    foreach (var c in contatos)
                    {
                        c.Status = 9;
                        c.Ativo = false;
                    }

                    _repository.Update(contatos.ToArray());                    
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
                throw new ContatoException("Não foi possível remover o contato da empresa.", e);
            }
        }

        public async Task<IEnumerable<Contato>> GetAllAsync(int idCliente, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                var contatos = _repository.GetList(c => c.IdCliente.Equals(idCliente));

                return contatos;
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
                throw new EnderecoException("Não foi possível recuperar os contatos.", e);
            }
        }

        public async Task<IEnumerable<Contato>> GetAllAsync(IEnumerable<int> empresasIds, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                var contatos = _repository.GetList(c => empresasIds.Contains(c.EmpresaId));

                return contatos;
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
                throw new EnderecoException("Não foi possível recuperar os contatos.", e);
            }
        }

        public async Task<Contato> GetByIdAsync(int entityId, int idCliente, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                var contato = _repository.GetList(c => c.ID.Equals(entityId)
                                    && c.IdCliente.Equals(idCliente)).SingleOrDefault();
                return contato;
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
                throw new EnderecoException("Não foi possível recuperar o endereço.", e);
            }
        }

        public async Task<Contato> SaveAsync(Contato entity, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);

                switch (entity.ID)
                {
                    case 0:
                        entity.DataCriacao = DateTime.UtcNow;
                        entity.DateAlteracao = DateTime.UtcNow;
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
                throw new EnderecoException("Não foi possível salvar o contato da empresa. Entre em contato com o suporte.", e);
            }
        }

        public async Task<IEnumerable<Contato>> GetByEmpresaId(int empresaId, int idCliente, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);

                var contatos = _repository.GetList(c => c.EmpresaId.Equals(empresaId)
                                        && c.IdCliente.Equals(idCliente));
                return contatos;
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
                throw new EnderecoException("Não foi possível recuperar os contatos da empresa. Entre em contato com o suporte.", e);
            }
        }

        public async Task<Contato> UpdateAsync(Contato entity, string token)
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
                throw new EnderecoException("Não foi possível atualizar o contato da empresa. Entre em contato com o suporte.", e);
            }
        }

        public async Task SaveAsync(IList<Contato> entities, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);

                if (entities != null && entities.Count > 0)
                {
                    switch (entities.FirstOrDefault().ID)
                    {
                        case 0:
                            foreach (var c in entities)
                            {
                                c.DataCriacao = DateTime.UtcNow;
                                c.DateAlteracao = DateTime.UtcNow;
                                c.Ativo = true;
                            }

                            _repository.Add(entities.ToArray());
                            break;
                        default:
                            await UpdateAsync(entities, token);
                            break;
                    }
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
                throw new EnderecoException("Não foi possível salvar os contatos da empresa. Entre em contato com o suporte.", e);
            }
        }

        public async Task UpdateAsync(IList<Contato> entities, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);

                foreach (var c in entities)
                {
                    c.DateAlteracao = DateTime.UtcNow;
                }

                _repository.Update(entities.ToArray());
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
                throw new EnderecoException("Não foi possível atualizar os contatos da empresa. Entre em contato com o suporte.", e);
            }
        }
    }
}
