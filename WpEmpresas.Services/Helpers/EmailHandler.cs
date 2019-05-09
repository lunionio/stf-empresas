using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpEmpresas.Entities;

namespace WpEmpresas.Services.Helpers
{
    public class EmailHandler
    {
        public async Task EnviarEmailAsync(int tipo, int entityId, IEnumerable<Usuario> admins)
        {
            try
            {
                RestClient client = new RestClient("http://webmail.talanservices.com.br/");
                var url = "api/Enviar";
                RestRequest request = null;
                request = new RestRequest(url, Method.POST);

                //var destinatario = new EmailModel(usuario.Nome, usuario.Login);
                var remetenteEmail = admins.FirstOrDefault()?.Login;
                var remetente = new EmailModel("Atendimento", remetenteEmail);

                var html = File.ReadAllText("wwwroot/TemplateSaude.html");

                switch (tipo)
                {
                    case 1:
                        html = html.Replace("TEXTO_CADASTRO", "Uma nova clínica");
                        break;
                    case 2:
                        html = html.Replace("TEXTO_CADASTRO", "Um novo Fornecedor");
                        break;
                }

                var guid = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ tipo }+{ entityId }"));

                html = html.Replace("TEXTO_CADASTRO", guid);

                var destinatarios = new List<EmailModel>();

                foreach (var item in admins)
                {
                    destinatarios.Add(new EmailModel(item.Nome, item.Login));
                }

                var mail = new Mail()
                {
                    Assunto = $"Cadastro - Saúde Com Vc",
                    Remetente = remetente,
                    Html = true,
                    Mensagem = html,
                    Destinatarios = destinatarios,
                };

                var jsonToSend = JsonConvert.SerializeObject(mail);

                request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
                var response = await client.ExecuteTaskAsync(request);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
