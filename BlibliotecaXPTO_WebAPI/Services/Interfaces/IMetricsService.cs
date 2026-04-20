using BibliotecaXPTOLibs.DTOs;

namespace BlibliotecaXPTO_WebAPI.Services.Interfaces
{
    public interface IMetricsService
    {
        public List<TopBooksDTO> GetTopBooks();

    }
}
