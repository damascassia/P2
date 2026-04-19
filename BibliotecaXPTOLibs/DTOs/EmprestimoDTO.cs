using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaXPTOLibs.DTOs
{
    public class EmprestimoDTO
    {
        public string LeitorDoc { get; set; }   
        public int ExemplarId { get; set; }     
    }


    public class DevolucaoDTO
    {
        public int ExemplarId { get; set; }     
    }

    public class EmprestimoAtivoDTO
    {
        public int Id { get; set; }
        public string StatusEmprestimo { get; set; }  
    }
}

