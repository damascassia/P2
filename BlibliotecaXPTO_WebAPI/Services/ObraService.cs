using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using BlibliotecaXPTO_WebAPI.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace BlibliotecaXPTO_WebAPI.Services
{
    public class ObraService : IObraService
    {
        private readonly IObrasRepository _repo;
        private readonly ILogger _logger;

        public ObraService(ILogger<ObraService> logger, IObrasRepository repo)
        {
            _repo = repo;
            _logger = logger;
        }

        public List<ObraDisponivelDTO> PesquisarObrasDisponiveis(string nomeNucleo, string assunto)
        {
            _logger.LogInformation($"Pesquisa obras: nucleo={nomeNucleo} assunto={assunto}");
            return _repo.PesquisarObrasDisponiveis(nomeNucleo, assunto);
        }
    }
    }
