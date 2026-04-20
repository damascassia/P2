using BibliotecaXPTOLibs.DTOs;

namespace BlibliotecaXPTO_WebAPI.Services.Interfaces
{
    public interface IObraService
    {
        List<HistObrasDTO> GetHistorico(RequestHistObrasDTO dto);

        int Create(CreateObraDTO dto);

        bool Update(int id, CreateObraDTO dto);

        int UpdateCount(int id);

        bool Delete(int id);
        (bool Sucesso, string Mensagem, List<ObraDisponivelDTO> Dados) PesquisarObrasDisponiveis(
   string nomeNucleo, string assunto);
    }
}
