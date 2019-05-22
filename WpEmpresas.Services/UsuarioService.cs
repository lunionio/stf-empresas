using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WpEmpresas.Entities;
using WpEmpresas.Services.Helpers;

namespace WpEmpresas.Services
{
    public class UsuarioService
    {
        public static async Task<IEnumerable<Usuario>> GetByIdsAsync(int idCliente, IEnumerable<int> ids)
        {
            var envio = new
            {
                idCliente,
                ids,
            };

            var helper = new ServiceHelper();
            var usuarios = await helper.PostAsync<IEnumerable<Usuario>>("http://187.84.232.164:5300/api", "/Seguranca/Principal/BuscarUsuarios/" + idCliente + "/" + 999, envio);

            return usuarios;
        }
    }
}
