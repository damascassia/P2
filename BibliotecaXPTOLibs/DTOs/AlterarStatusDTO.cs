using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaXPTOLibs.DTOs
{
    public class AlterarStatusDTO
    {
        public int UtilizadorId { get; set; }
        public Models.StatusUtilizadores NovoStatusId { get; set; }
    }
}
