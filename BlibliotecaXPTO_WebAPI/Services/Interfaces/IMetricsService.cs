using BibliotecaXPTOLibs.DTOs;

namespace BlibliotecaXPTO_WebAPI.Services.Interfaces
{
    public interface IMetricsService
    {

        public List<TopBooksDTO> GetTopBooks(DateTime dataInicio, DateTime dataFim);
        public List<BottomBooksDTO> GetBottomBooks(DateTime dataInicio, DateTime dataFim);
        public List<Emprestimo_NucleoDTO> GetBottomEmprestimos(DateTime dataInicio, DateTime dataFim);
        public List<Emprestimo_NucleoDTO> GetTopEmprestimos(DateTime dataInicio, DateTime dataFim);
        public List<TopLeitoresDTO> GetTopLeitores(DateTime dataInicio, DateTime dataFim);

    }
}
