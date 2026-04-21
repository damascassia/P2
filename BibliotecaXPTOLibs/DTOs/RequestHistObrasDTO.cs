using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaXPTOLibs.DTOs
{
    public class RequestHistObrasDTO
    {
        public string DocumentoLeitor { get; set; }

        public string TipoDocumento { get; set; }

        public bool AgruparNucleo { get; set; }

        public bool AgruparDatas { get; set; }

        public DateOnly InicioIntervalo {get; set; }

        public DateOnly FimIntervalo { get; set; }
    }
}
