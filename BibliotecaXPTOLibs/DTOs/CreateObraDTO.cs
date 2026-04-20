using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaXPTOLibs.DTOs
{
    public class CreateObraDTO
    {
        public object Autor { get; internal set; }
        public object Titulo { get; internal set; }
        public object Assunto_Id { get; internal set; }
    }
}
