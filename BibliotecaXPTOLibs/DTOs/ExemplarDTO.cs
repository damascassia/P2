using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaXPTOLibs.DTOs
{
    public class ExemplarDTO
    {
        public int Id { get; set; }
        public byte[] ImagemCapa { get; set; }
        public string Edicao { get; set; }
        public int Ano { get; set; }
        public string ISBN { get; set; }
        public bool Disponivel { get; set; }
        public int Id_Obra { get; set; }
        public int Id_Nucleo { get; set; }
    }
}
