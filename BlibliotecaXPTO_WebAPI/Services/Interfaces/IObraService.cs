using BibliotecaXPTOLibs.DTOs;

namespace BlibliotecaXPTO_WebAPI.Services.Interfaces
{
    public interface IObraService
    {
        int Create(CreateObraDTO dto);

        bool Update(int id, CreateObraDTO dto);

        bool Delete(int id);
    }
}
