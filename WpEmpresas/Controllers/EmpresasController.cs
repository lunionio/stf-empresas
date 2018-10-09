using Microsoft.AspNetCore.Mvc;
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
        private readonly ContatoDomain _cDomain;

        public EmpresasController([FromServices]EmpresaDomain domain, [FromServices]EnderecoDomain edDomain, [FromServices]ContatoDomain cDomain)
        {
            _domain = domain;
            _edDomain = edDomain;
            _cDomain = cDomain;
        }

        [HttpGet("{idCliente:int}/{token}")]
        public async Task<IActionResult> GetAsync([FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                var empresas = await _domain.GetAllAsync(idCliente, token);
                var enderecos = await _edDomain.GetAllAsync(empresas.Select(e => e.ID), token);
                var contatos = await _cDomain.GetAllAsync(empresas.Select(e => e.ID), token);

                foreach (var empresa in empresas)
                {
                    empresa.Endereco = enderecos.FirstOrDefault(e => e.EmpresaId.Equals(empresa.ID));
                    empresa.Contatos = contatos.Where(c => c.EmpresaId.Equals(empresa.ID)).ToList();
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
                var ep = await _domain.SaveAsync(empresa, token);
                var ed = await _edDomain.SaveAsync(ep.Endereco, token);
                await _cDomain.SaveAsync(ep.Contatos, token);

                return Ok("Empresa salva com sucesso.");
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

        [HttpDelete("{token}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string token, [FromBody]Empresa empresa)
        {
            try
            {
                await _domain.DeleteAsync(empresa, token);
                await _edDomain.DeleteAsync(empresa.Endereco, token);
                await _cDomain.DeleteAsync(empresa.Contatos, token);

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
                empresa.Endereco = await _edDomain.GetByEmpresaId(empresa.ID, idCliente, token);
                empresa.Contatos = (await _cDomain.GetByEmpresaId(empresa.ID, idCliente, token)).ToList();

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
    }
}