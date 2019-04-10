using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpEmpresas.Domains.Generics;
using WpEmpresas.Entities;
using WpEmpresas.Infraestructure;
using WpEmpresas.Infraestructure.Exceptions;

namespace WpEmpresas.Domains
{
    public class ResponsavelDomain
    {
        private readonly ResponsavelRepository _rRepository;

        public ResponsavelDomain(ResponsavelRepository rRepository)
        {
            _rRepository = rRepository;
        }

        public void Delete(Responsavel entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Responsavel> GetAll(int idCliente)
        {
            throw new NotImplementedException();
        }

        public Responsavel GetById(int entityId)
        {
            try
            {
                var result = _rRepository.GetList(e => e.ID.Equals(entityId)).SingleOrDefault();

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
                throw new EmpresaException("Não foi possível recuperar o responsável.", e);
            }
        }

        public IEnumerable<Responsavel> GetByIds(IEnumerable<int> entitiesIds)
        {
            try
            {
                var result = _rRepository.GetList(e => entitiesIds.Contains(e.ID));

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
                throw new EmpresaException("Não foi possível recuperar o responsável.", e);
            }
        }

        public Responsavel Save(Responsavel entity)
        {
            try
            {
                if (entity != null)
                {
                    switch (entity.ID)
                    {
                        case 0:
                            entity.ID = _rRepository.Add(entity);
                            break;
                        default:
                            entity = Update(entity);
                            break;
                    }
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
                throw new EmpresaException("Não foi possível salvar o responsável. Entre em contato com o suporte.", e);
            }
        }

        public Responsavel Update(Responsavel entity)
        {
            try
            {
                _rRepository.Update(entity);

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
                throw new EmpresaException("Não foi possível atualizar o responsável. Entre em contato com o suporte.", e);
            }
        }
    }
}
