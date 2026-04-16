namespace BlibliotecaXPTO_WebAPI.Services.Interfaces
{
    public interface IObraService
    {
        IEnumerable<ObraDTO> ObterObrasDisp(string pesquisa);
    }
}
