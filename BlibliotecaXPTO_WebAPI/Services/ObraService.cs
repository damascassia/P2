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

        private readonly ILogger _logger;   

        public ObraService(IObrasRepository obrasRepository, IConfiguration config, ILogger logger)
        {
            _repoObras = obrasRepository;
            _activeTag = config.GetValue<string>("Settings:ActiveTag");
            _logger = logger;
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

        public List<ObraDisponivelDTO> PesquisarObrasDisponiveis(string nomeNucleo, string assunto)
        {
            _logger.LogInformation($"Pesquisa obras: nucleo={nomeNucleo} assunto={assunto}");
            return _repoObras.PesquisarObrasDisponiveis(nomeNucleo, assunto);
        }
    }
}