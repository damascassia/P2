using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaXPTOLibs.DTOs
{
    public class EmprestimoDTO
    {
        public string LeitorDoc { get; set; }   // Número de documento do leitor
        public int ExemplarId { get; set; }     // ID do exemplar a requisitar
    }


    public class DevolucaoDTO
    {
        public int ExemplarId { get; set; }     // ID do exemplar a devolver
    }

    public class EmprestimoAtivoDTO
    {
        public int Id { get; set; }
        public string StatusEmprestimo { get; set; }  // "Atraso", "Devolução Urgente", "Devolver em breve", "Dentro do Prazo", "Entregue"
    }
}

