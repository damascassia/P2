using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using BlibliotecaXPTO_WebAPI.Services.Interfaces;

namespace BlibliotecaXPTO_WebAPI.Services
{
    public class ObraService : IObraService
    {
        private readonly IObrasRepository _repoObras;
        public ObraService(IObrasRepository obrasRepository)
        {
            _repoObras = obrasRepository;   
        }

        public int Create(CreateObraDTO dto)
        {
            return _repoObras.Insert(dto, "DB_BibliotecaPazu");
        }

        public bool Update(int id, CreateObraDTO dto)
        {
            return _repoObras.Update(id, dto, "DB_BibliotecaPazu");
        }

        public bool Delete(int id)
        {
            return _repoObras.Delete(id, "DB_BibliotecaPazu");

        }
    }
}
