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

        public MetricsService(ILogger<UtilizadoresService> logger, IMetricsRepository repo)
        {
            _repo = repo;
            _logger = logger;
        }

        public List<TopBooksDTO> GetTopBooks(DateTime dataInicio, DateTime dataFim)
        {
            var topBooks = _repo.GetTopBooks(dataInicio, dataFim);
            return topBooks.Select( t => new TopBooksDTO
            {
                Autor = t.Autor,
                Titulo = t.Titulo,
                TotalEmprestimos = t.TotalEmprestimos
            }).ToList();
        }
        public List<BottomBooksDTO> GetBottomBooks(DateTime dataInicio, DateTime dataFim)
        {
            var bottomBooks = _repo.GetBottomBooks(dataInicio, dataFim);
            return bottomBooks.Select(t => new BottomBooksDTO
            {
                Autor = t.Autor,
                Titulo = t.Titulo,
                TotalEmprestimos = t.TotalEmprestimos
            }).ToList();
        }
        public List<Emprestimo_NucleoDTO> GetBottomEmprestimos(DateTime dataInicio, DateTime dataFim)
        {
            var bottomEmprestimos = _repo.GetBottomEmprestimos(dataInicio, dataFim);
            return bottomEmprestimos.Select(e => new Emprestimo_NucleoDTO
            {
                Nucleo = e.Nucleo,
                TotalEmprestimos = e.TotalEmprestimos
            }).ToList();
        }
        public List<Emprestimo_NucleoDTO> GetTopEmprestimos(DateTime dataInicio, DateTime dataFim)
        {
            var topEmprestimos = _repo.GetTopEmprestimos(dataInicio, dataFim);
            return topEmprestimos.Select(e => new Emprestimo_NucleoDTO
            {
                Nucleo = e.Nucleo,
                TotalEmprestimos = e.TotalEmprestimos
            }).ToList();
        }
        public List<TopLeitoresDTO> GetTopLeitores(DateTime dataInicio, DateTime dataFim)
        {
            var topLeitores = _repo.GetTopLeitores(dataInicio, dataFim);
            return topLeitores.Select(l => new TopLeitoresDTO
            {
                Leitor = l.Leitor,
                TotalEmprestimos = l.TotalEmprestimos
            }).ToList();
        }
    }
}
