using BlibliotecaXPTO_WebAPI.Services.Interfaces;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using BibliotecaXPTOLibs.Repositories;

namespace BlibliotecaXPTO_WebAPI.Services
{
    public class LeitoresService : ILeitoresService
    {
        
        
            private readonly ILeitoresRepository _repo;
            private readonly ILogger _logger;

            public LeitoresService(ILogger<LeitoresService> logger, ILeitoresRepository repo)
            {
                _repo = repo;
                _logger = logger;
            }

            public void CancelarInscricao(int leitorId)
            {
                try
                {
                    _logger.LogInformation($"Cancelar inscrição: leitorId={leitorId}");
                    _repo.CancelarInscricao(leitorId);
                }
                catch { throw; }
            }
        }
    }

