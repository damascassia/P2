using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using BlibliotecaXPTO_WebAPI.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace BlibliotecaXPTO_WebAPI.Services
{
    public class ObraService : IObraService
    {
        private readonly IObrasRepository _repoObras;

        private readonly string _activeTag;
        public ObraService(IObrasRepository obrasRepository, IConfiguration config)
        {
            _repoObras = obrasRepository;
            _activeTag = config.GetValue<string>("Settings:ActiveTag");
        }

        public List<HistObrasDTO> GetHistorico(RequestHistObrasDTO dto)
        {
            return _repoObras.GetHistorico(dto, _activeTag);
        }

        public int Create(CreateObraDTO dto)
        {
            return _repoObras.Insert(dto, _activeTag);
        }

        public bool Update(int id, CreateObraDTO dto)
        {
            return _repoObras.Update(id, dto, _activeTag);
        }

        public int UpdateCount(int id)
        {
            return _repoObras.UpdateCount(id, _activeTag);
        }

        public bool Delete(int id)
        {
            return _repoObras.Delete(id, _activeTag);

        }

    
        public (bool Sucesso, string Mensagem, List<ObraDisponivelDTO> Dados) PesquisarObrasDisponiveis(
            string nomeNucleo, string assunto)
        {
            try
            {
                var dados = _repoObras.PesquisarObrasDisponiveis(nomeNucleo, assunto);

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