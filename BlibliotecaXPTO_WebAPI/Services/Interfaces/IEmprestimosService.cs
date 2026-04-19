using BibliotecaXPTOLibs.DTOs;

namespace BlibliotecaXPTO_WebAPI.Services.Interfaces
{
    public interface IEmprestimosService
    {
        (bool Sucesso, string Mensagem, List<EmprestimoAtivoDTO> Dados) ObterEmprestimosAtivos(int leitorId);
    }
}
