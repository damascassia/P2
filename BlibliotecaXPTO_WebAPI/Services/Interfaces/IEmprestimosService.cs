using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Repositories.Interfaces;


namespace BlibliotecaXPTO_WebAPI.Services.Interfaces
{
    public interface IEmprestimosService
    {
        void RealizarRequisicao(EmprestimoDTO dto);
        void RealizarDevolucao(DevolucaoDTO dto);
        List<SituacaoEmprestimoDTO> ObterSituacaoLeitor(int leitorId);


    }

}
