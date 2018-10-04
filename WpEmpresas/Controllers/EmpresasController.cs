using Microsoft.AspNetCore.Mvc;
using System;
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

        public EmpresasController([FromServices]EmpresaDomain domain)
        {
            _domain = domain;
        }

        [HttpGet("{idCliente:int}/{token}")]
        public async Task<IActionResult> GetAsync([FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                var empresas = await _domain.GetAllAsync(idCliente, token);
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
                return StatusCode(500, "Ocorreu um erro ao tentar salvar a empresa recebido. Entre em contato com o suporte.");
            }
        }

        [HttpDelete("{token}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string token, [FromBody]Empresa empresa)
        {
            try
            {
                await _domain.DeleteAsync(empresa, token);

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