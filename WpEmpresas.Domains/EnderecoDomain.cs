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
    public class EnderecoDomain : IDomain<Endereco>
    {
        private readonly SegurancaService _segService;
        private readonly EnderecoRepository _edRepository;

        public EnderecoDomain(SegurancaService segService, EnderecoRepository edRepository)
        {
            _segService = segService;
            _edRepository = edRepository;
        }

        public async Task DeleteAsync(Endereco entity, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                var endereco = _edRepository.GetList(e => e.EmpresaId.Equals(entity.EmpresaId)).SingleOrDefault();
                endereco.Status = 9;
                endereco.Ativo = false;
                _edRepository.Update(endereco);
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

        public async Task<IEnumerable<Endereco>> GetAllAsync(int idCliente, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                var enderecos = _edRepository.GetList(e => e.IdCliente.Equals(idCliente));

                return enderecos;
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
                throw new EnderecoException("Não foi possível recuperar os endereços.", e);
            }
        }

        public async Task<Endereco> GetByIdAsync(int entityId, int idCliente, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                var endereco = _edRepository.GetList(e => e.ID.Equals(entityId) 
                                    && e.IdCliente.Equals(idCliente)).SingleOrDefault();
                return endereco;
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

        public async Task<Endereco> SaveAsync(Endereco entity, string token)
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
                        entity.ID = _edRepository.Add(entity);
                        break;
                    default:
                        entity = await UpdateAsync(entity, token);
                        break;
                }

                return entity;
            }
            catch(ServiceException e)
            {
                throw e;
            }
            catch(InvalidTokenException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new EnderecoException("Não foi possível salvar o endereço da empresa. Entre em contato com o suporte.", e);
            }
        }

        public async Task<Endereco> UpdateAsync(Endereco entity, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                entity.DateAlteracao = DateTime.UtcNow;
                _edRepository.Update(entity);

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
                throw new EnderecoException("Não foi possível atualizar o endereço da empresa. Entre em contato com o suporte.", e);
            }
        }

        public async Task<IEnumerable<Endereco>> GetAllAsync(IEnumerable<int> empresasIds, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                var enderecos = _edRepository.GetList(e => empresasIds.Contains(e.EmpresaId));
                return enderecos;
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
                throw new EnderecoException("Não foi possível listar os endereços.", e);
            }
        }

        public async Task<Endereco> GetByEmpresaId(int empresaId, int idCliente, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);

                var endereco = _edRepository.GetList(e => e.EmpresaId.Equals(empresaId)
                                        && e.IdCliente.Equals(idCliente)).SingleOrDefault();
                return endereco;
            }
            catch(ServiceException e)
            {
                throw e;
            }
            catch(InvalidTokenException e)
            {
                throw e;
            }
            catch(Exception e)
            {
                throw new EnderecoException("Não foi possível recuperar o endereço da empresa. Entre em contato com o suporte.", e);
            }
        }

        public async Task DeleteAsync(int empresaId, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                var endereco = _edRepository.GetList(e => e.EmpresaId.Equals(empresaId)).SingleOrDefault();

                if (endereco != null)
                {
                    endereco.Status = 9;
                    endereco.Ativo = false;
                    _edRepository.Update(endereco);
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
    }
}
