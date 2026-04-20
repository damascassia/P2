using BibliotecaXPTOLibs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaXPTOLibs.Repositories.Interfaces
{
    public interface IEmprestimosRepository
    {
        void RealizarRequisicao(string leitorDoc, int exemplarId);
        void RealizarDevolucao(int exemplarId);
        List<SituacaoEmprestimoDTO> ObterSituacaoLeitor(int leitorId);
    }
}
