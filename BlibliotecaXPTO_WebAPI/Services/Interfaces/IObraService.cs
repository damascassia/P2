using BibliotecaXPTOLibs.DTOs;

namespace BlibliotecaXPTO_WebAPI.Services.Interfaces
{
    public interface IObraService
    {
        List<ObraDisponivelDTO> PesquisarObrasDisponiveis(string nomeNucleo, string assunto);
    }

    }

