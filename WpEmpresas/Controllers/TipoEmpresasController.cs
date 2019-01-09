using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WpEmpresas.Domains;
using WpEmpresas.Infraestructure.Exceptions;

namespace WpEmpresas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEmpresasController : ControllerBase
    {
        private readonly TipoEmpresasDomain _domain;

        public TipoEmpresasController(TipoEmpresasDomain domain)
        {
            _domain = domain;
        }

        [HttpGet("{idCliente:int}/{token}")]
        public async Task<IActionResult> GetAllAsync([FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                var result = await _domain.GetAllAsync(idCliente, token);
                return Ok(result);
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
                return StatusCode(500, "Ocorreu um erro ao tentar recuperar os tipos de empresas solicitadas. Entre em contato com o suporte.");
            }
        }
    }
}