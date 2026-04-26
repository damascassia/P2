using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaXPTOLibs.DTOs
{
    internal class UtilizadoresDTO
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
