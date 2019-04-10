﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WpEmpresas.Domains;
using WpEmpresas.Entities;
using WpEmpresas.Infraestructure.Exceptions;

namespace WpEmpresas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly EmpresaDomain _domain;
        private readonly EnderecoDomain _edDomain;
        private readonly TelefoneDomain _tDomain;
        private readonly ContatoDomain _cDomain;
        private readonly TipoEmpresasDomain _teDomain;
        private readonly ResponsavelDomain _rDomain;
        private readonly EmpresaXEspecialidadesDomain _eXeDomain;

        public EmpresasController([FromServices]EmpresaDomain domain,
            [FromServices]EnderecoDomain edDomain, [FromServices]ContatoDomain cDomain, [FromServices]TelefoneDomain tDomain, [FromServices]TipoEmpresasDomain teDomain,
            [FromServices]ResponsavelDomain rDomain, [FromServices]EmpresaXEspecialidadesDomain eXeDomain)
        {
            _domain = domain;
            _edDomain = edDomain;
            _cDomain = cDomain;
            _tDomain = tDomain;
            _teDomain = teDomain;
            _rDomain = rDomain;
            _eXeDomain = eXeDomain;
        }

        [HttpGet("{idCliente:int}/{token}")]
        public async Task<IActionResult> GetAsync([FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                var empresas = await _domain.GetAllAsync(idCliente, token);
                var enderecos = _edDomain.GetAll(empresas.Select(e => e.ID), token);
                var contatos = _cDomain.GetAll(empresas.Select(e => e.ID), token);
                var telefones = _tDomain.GetAll(empresas.Select(e => e.ID), token);
                var tipos = _teDomain.GetAll(empresas.Select(e => e.TipoEmpresaId), token);
                var responsveis = _rDomain.GetByIds(empresas.Select(e => e.ResponsavelId));

                foreach (var empresa in empresas)
                {
                    empresa.Endereco = enderecos.FirstOrDefault(e => e.EmpresaId.Equals(empresa.ID));
                    empresa.Contatos = contatos.Where(c => c.EmpresaId.Equals(empresa.ID)).ToList();
                    empresa.Telefone = telefones.FirstOrDefault(t => t.EmpresaId.Equals(empresa.ID));
                    empresa.TipoEmpresa = tipos.FirstOrDefault(t => t.ID.Equals(empresa.TipoEmpresaId));
                    empresa.Responsavel = responsveis.FirstOrDefault(r => r.ID.Equals(empresa.ResponsavelId));
                    empresa.Especialidade = _eXeDomain.GetByEmpresaId(empresa.ID, empresa.IdCliente);

                    if(empresa.Especialidade != null)
                    {
                        empresa.EspecialidadeId = empresa.Especialidade.ID;
                    }
                }

                return Ok(empresas);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (ServiceException e)
            {
                return StatusCode(401, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (EmpresaException e)
            {
                return StatusCode(400, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar recuperar as empresas solicitadas. Entre em contato com o suporte.");
            }
        }

        [HttpPost("{token}")]
        public async Task<IActionResult> SaveAsync([FromRoute]string token, [FromBody]Empresa empresa)
        {
            try
            {
                if (empresa.Responsavel != null)
                {
                    var responsavel = _rDomain.Save(empresa.Responsavel);
                    empresa.ResponsavelId = responsavel.ID;
                    empresa.Responsavel = responsavel;
                }
                var ep = await _domain.SaveAsync(empresa, token);
                ep.Endereco = await _edDomain.SaveAsync(ep.Endereco, token);
                await _cDomain.SaveAsync(ep.Contatos, token);
                ep.Telefone = await _tDomain.SaveAsync(ep.Telefone, token);
                ep.Responsavel = empresa.Responsavel;

                var oldEXe = default(EmpresaXEspecialidade);

                if(ep.ID > 0 && ep.EspecialidadeId > 0)
                {
                    oldEXe = _eXeDomain.Save(new EmpresaXEspecialidade(ep.ID, ep.EspecialidadeId));
                }

                return Ok(ep);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (ServiceException e)
            {
                return StatusCode(401, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (EmpresaException e)
            {
                return StatusCode(400, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar salvar a empresa recebida. Entre em contato com o suporte.");
            }
        }

        [HttpPost("Delete/{token}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string token, [FromBody]Empresa empresa)
        {
            try
            {
                await _domain.DeleteAsync(empresa, token);
                await _edDomain.DeleteAsync(empresa.ID, token);
                await _cDomain.DeleteAsync(empresa.ID, token);
                await _tDomain.DeleteAsync(empresa.ID, token);

                return Ok("Empresa removida com sucesso.");
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (EmpresaException e)
            {
                return StatusCode(400, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (ServiceException e)
            {
                return StatusCode(400, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocoreu um erro ao tentar remover a oportunidade. Entre em contato com o suporte.");
            }
        }

        [HttpGet("{id:int}/{idCLiente:int}/{token}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]string token, [FromRoute]int id, [FromRoute]int idCliente)
        {
            try
            {
                var empresa = await _domain.GetByIdAsync(id, idCliente, token);
                empresa.Endereco = await _edDomain.GetByEmpresaIdAsync(empresa.ID, idCliente, token);
                empresa.Contatos = (await _cDomain.GetByEmpresaIdAsync(empresa.ID, idCliente, token)).ToList();
                empresa.Telefone = await _tDomain.GetByEmpresaIdAsync(empresa.ID, idCliente, token);
                empresa.TipoEmpresa = await _teDomain.GetByIdAsync(empresa.TipoEmpresaId, idCliente, token);
                empresa.Responsavel = _rDomain.GetById(empresa.ResponsavelId);
                empresa.Especialidade = _eXeDomain.GetByEmpresaId(empresa.ID, empresa.IdCliente);

                if (empresa.Especialidade != null)
                {
                    empresa.EspecialidadeId = empresa.Especialidade.ID;
                }

                return Ok(empresa);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (EmpresaException e)
            {
                return StatusCode(400, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (ServiceException e)
            {
                return StatusCode(400, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar recuperar a empresa solicitada. Entre em contato com o suporte.");
            }
        }

        [HttpGet("GetByTipo/{idCliente:int}/{tipoId:int}/{token}")]
        public async Task<IActionResult> GetByTipoAsync([FromRoute]int idCliente, [FromRoute]int tipoId, [FromRoute]string token)
        {
            try
            {
                var empresas = await _domain.GetAllAsync(idCliente, tipoId, token);
                var enderecos = _edDomain.GetAll(empresas.Select(e => e.ID), token);
                var contatos = _cDomain.GetAll(empresas.Select(e => e.ID), token);
                var telefones = _tDomain.GetAll(empresas.Select(e => e.ID), token);
                var tipos = _teDomain.GetAll(empresas.Select(e => e.TipoEmpresaId), token);
                var responsveis = _rDomain.GetByIds(empresas.Select(e => e.ResponsavelId));

                foreach (var empresa in empresas)
                {
                    empresa.Endereco = enderecos.FirstOrDefault(e => e.EmpresaId.Equals(empresa.ID));
                    empresa.Contatos = contatos.Where(c => c.EmpresaId.Equals(empresa.ID)).ToList();
                    empresa.Telefone = telefones.FirstOrDefault(t => t.EmpresaId.Equals(empresa.ID));
                    empresa.TipoEmpresa = tipos.FirstOrDefault(t => t.ID.Equals(empresa.TipoEmpresaId));
                    empresa.Responsavel = responsveis.FirstOrDefault(r => r.ID.Equals(empresa.ResponsavelId));
                    empresa.Especialidade = _eXeDomain.GetByEmpresaId(empresa.ID, empresa.IdCliente);

                    if (empresa.Especialidade != null)
                    {
                        empresa.EspecialidadeId = empresa.Especialidade.ID;
                    }
                }

                return Ok(empresas);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (ServiceException e)
            {
                return StatusCode(401, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (EmpresaException e)
            {
                return StatusCode(400, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar recuperar as empresas solicitadas. Entre em contato com o suporte.");
            }
        }
    }
}