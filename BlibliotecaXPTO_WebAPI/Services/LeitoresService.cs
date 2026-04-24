using BlibliotecaXPTO_WebAPI.Services.Interfaces;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using BibliotecaXPTOLibs.Repositories;

namespace BlibliotecaXPTO_WebAPI.Services
{
    public class LeitoresService : ILeitoresService
    {

        private readonly string _activeTag;
            private readonly ILeitoresRepository _repo;
            private readonly ILogger _logger;

            public LeitoresService(ILogger<LeitoresService> logger, ILeitoresRepository repo, IConfiguration config)
            {
                _activeTag = config.GetValue<string>("Settings:ActiveTag");
            _repo = repo;
                _logger = logger;
            }

            public void CancelarInscricao(int leitorId)
            {
                try
                {
                    _logger.LogInformation($"Cancelar inscrição: leitorId={leitorId}");
                    _repo.CancelarInscricao(leitorId, _activeTag);
                }
                catch { throw; }
            }
        }
    }

