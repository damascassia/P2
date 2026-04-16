using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaXPTOLibs.DTOs;

namespace BibliotecaXPTOLibs.Repositories.Interfaces
{
    public interface IAssuntoRepository
    {
        AssuntoDTO? GetById(int id, string tagRepo);
    }
}
