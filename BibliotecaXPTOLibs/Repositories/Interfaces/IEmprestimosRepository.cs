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
        // Realiza a requisição de um exemplar por um leitor.
        // Internamente chama a SP [Emprestimo] que já valida:
        //   - se o leitor existe e está ativo
        //   - se o leitor não tem já 4 exemplares
        //   - se o exemplar está disponível
        void RealizarRequisicao(string leitorDoc, int exemplarId);

        // Regista a devolução de um exemplar.
        // Internamente chama a SP [Devolucao] que:
        //   - define DataEntrega = GETDATE()
        //   - actualiza a disponibilidade do exemplar via SP [ExemplarDisponivel]
        void RealizarDevolucao(int exemplarId);

        // Devolve os empréstimos ativos do leitor com o respetivo status de prazo.
        // Chama a SP [sp_StatusEmprestimo].
        List<EmprestimoAtivoDTO> ObterEmprestimosAtivos(int leitorId);
    }
}
