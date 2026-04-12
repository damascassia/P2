using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaXPTOLibs.Models
{
    public class Emprestimos
    {
        public int Id { get; set; }
        public int Id_Leitor {  get; set; }
        public int Id_Exemplar { get; set; }

        public DateTime DataInicio { get; set; }
        public DateTime DataEntrega {  get; set; }
    }
}
