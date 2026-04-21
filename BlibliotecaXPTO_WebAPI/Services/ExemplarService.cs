using BlibliotecaXPTO_WebAPI.Services.Interfaces;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using BibliotecaXPTOLibs.DTOs;

namespace BlibliotecaXPTO_WebAPI.Services
{
    public class ExemplarService : IExemplarService
    {
        private readonly IExemplaresRepository _exemplaresRepository;

        private readonly string _activeTag;
        public ExemplarService(IExemplaresRepository exemplaresRepository, IConfiguration config)
        {
            _exemplaresRepository = exemplaresRepository;

            _activeTag = config.GetValue<string>("Settings:ActiveTag");
        }
        public bool ChangeNucleo(ChangeNucleoDTO dto)
        {
            return _exemplaresRepository.ChangeNucleo(dto, _activeTag);   
        }
    }
}
