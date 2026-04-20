using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaXPTOLibs.DTOs;

namespace BibliotecaXPTOLibs.Repositories.Interfaces
{
    public interface IExemplaresRepository
    {
        bool ChangeNucleo(ChangeNucleoDTO dto, string tagRepo);

        ExemplarDTO? GetById(int id, string tagRepo);
    }
}
