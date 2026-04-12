using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaXPTOLibs.Models
{
    public class Utilizadores
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string NumeroDocumento { get; set; }
        public int TipoDocumento { get; set; }
        public string Email { get; set; }
        public string Morada { get; set; }
        public string Telefone { get; set; }
        public int Id_TipoUser { get; set; }
        public int Id_Status { get; set; }
    }
}
