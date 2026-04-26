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
        void RealizarRequisicao(string leitorDoc, int exemplarId, string tagRepo);
        void RealizarDevolucao(int exemplarId, string tagRepo);
        List<SituacaoEmprestimoDTO> ObterSituacaoLeitor(int leitorId, string tagRepo);
    }
}
