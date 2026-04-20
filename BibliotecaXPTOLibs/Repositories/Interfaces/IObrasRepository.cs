using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaXPTOLibs.DTOs;

namespace BibliotecaXPTOLibs.Repositories.Interfaces
{
    public interface IObrasRepository
    {
        ObraDTO? GetById(int id, string tagRepo);

        int Insert(CreateObraDTO dto, string tagRepo);

        bool Update(int id, CreateObraDTO dto, string tagRepo);

        bool Delete(int id, string tagRepo);
    }
}
