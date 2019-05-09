using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WpEmpresas.Entities
{
    public class Mail
    {
        public string Assunto { get; set; }
        public bool Html { get; set; }
        public string Mensagem { get; set; }
        public IEnumerable<EmailModel> Destinatarios { get; set; }
        public EmailModel Remetente { get; set; }
    }
}
