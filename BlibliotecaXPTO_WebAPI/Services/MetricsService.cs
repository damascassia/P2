using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Repositories;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using BlibliotecaXPTO_WebAPI.Services.Interfaces;

namespace BlibliotecaXPTO_WebAPI.Services
{
    public class MetricsService : IMetricsService
    {
        private readonly IMetricsRepository _repo;
        private readonly ILogger _logger;

        public MetricsService(ILogger<UtilizadoresService> logger, MetricsRepository repo)
        {
            _repo = repo;
            _logger = logger;
        }

        public List<TopBooksDTO> GetTopBooks()
        {
            var topBooks = _repo.GetTopBooks();
            return topBooks.Select( t => new TopBooksDTO
            {
                Autor = t.Autor,
                Titulo = t.Titulo,
                TotalEmprestimos = t.TotalEmprestimos
            }).ToList();
        }
    }
}
