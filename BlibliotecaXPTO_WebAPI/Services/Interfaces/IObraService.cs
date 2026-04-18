using BibliotecaXPTOLibs.DTOs;

namespace BlibliotecaXPTO_WebAPI.Services.Interfaces
{
    public interface IObraService
    {
        (bool Sucesso, string Mensagem, List<ObraDisponivelDTO> Dados) PesquisarObrasDisponiveis(
   string nomeNucleo, string assunto);
    }
}
