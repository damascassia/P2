using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaXPTOLibs.Models
{
    public class Hist_Emprestimos
    {
        public int Id { get; set; }
        public int Id_Hist_Leitor { get; set; }
        public string Autor { get; set; }
        public string Titulo { get; set; }
        public string Assunto { get; set; }
        public string NomeNucleo { get; set; }
        public DateTime InicioEmprestimo { get; set; }
        public DateTime EntregaEmprestimo { get; set; }
    }
}
