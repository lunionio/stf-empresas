using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WpEmpresas.Domains;
using WpEmpresas.Infraestructure.Exceptions;
using WpEmpresas.Services;

namespace WpEmpresas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaXEspecialidadesController : ControllerBase
    {
        private readonly EmpresaXEspecialidadesDomain _domain;
        private readonly SegurancaService _service;

        public EmpresaXEspecialidadesController([FromServices]EmpresaXEspecialidadesDomain domain, [FromServices]SegurancaService service)
        {
            _domain = domain;
            _service = service;
        }

        [HttpPost("{idCliente:int}/{token}")]
        public async Task<IActionResult> BuscarPorIdsAsync([FromRoute]int idCliente, [FromRoute]string token, [FromBody]int id)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetByIdEmpresa(id);

                return Ok(result);
            }
            catch (ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (EspecialidadeException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }
    }
}