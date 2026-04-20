using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using BlibliotecaXPTO_WebAPI.Services.Interfaces;

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
    }
}
