using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaXPTOLibs.DTOs
{
    public class SituacaoEmprestimoDTO
    {
        public string NomeNucleo { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public DateTime DataInicio { get; set; }
        public int DiasRestantes { get; set; }
        public string StatusEmprestimo { get; set; } 
    }
}
