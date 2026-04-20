using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaXPTOLibs.DTOs
{
    public class RegistrarUtilizadorDTO
    {
        public string NomeCompleto { get; set; }
        public string NumeroDocumento { get; set; }
        public int TipoDocumento { get; set; }
        public string Email { get; set; }
        public string Morada { get; set; }
        public string Telefone { get; set; }

        public int Id_TipoUser { get; set; }

        // Conta
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}
