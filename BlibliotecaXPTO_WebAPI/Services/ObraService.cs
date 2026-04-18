using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using BlibliotecaXPTO_WebAPI.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace BlibliotecaXPTO_WebAPI.Services
{
    public class ObraService : IObraService
    {
        private readonly IObrasRepository _obrasRepository;

        public ObraService(IObrasRepository obrasRepository)
        {
            _obrasRepository = obrasRepository;
        }

    
        public (bool Sucesso, string Mensagem, List<ObraDisponivelDTO> Dados) PesquisarObrasDisponiveis(
            string nomeNucleo, string assunto)
        {
            try
            {
                var dados = _obrasRepository.PesquisarObrasDisponiveis(nomeNucleo, assunto);

                if (dados.Count == 0)
                    return (true, "Nenhuma obra disponível para os filtros indicados.", dados);

                return (true, "OK", dados);
            }
            catch (SqlException ex)
            {
                return (false, ex.Message, null);
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}", null);
            }
        }
    }
}