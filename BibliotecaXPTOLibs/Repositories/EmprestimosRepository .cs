using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Helpers.Interfaces;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using DalProLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaXPTOLibs.Repositories
{
    public class EmprestimosRepository : IEmprestimosRepository
    {
        private readonly IConnectionHelper _connectionHelper;

        public EmprestimosRepository(IConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
            // Garante que a connection string está configurada antes de qualquer operação
            DalPro.ConnectionString = _connectionHelper.getConnectionString("DB_Biblioteca");
        }

        // -------------------------------------------------------------------
        // REQUISIÇÃO
        // Chama a SP [Emprestimo] passando o documento do leitor e o ID do exemplar.
        // A SP trata internamente todas as validações (leitor ativo, limite de 4,
        // exemplar disponível) e lança RAISERROR se alguma falhar.
        // O DalPro propaga essas excepções SQL, que apanhamos no Service.
        // -------------------------------------------------------------------
        public void RealizarRequisicao(string leitorDoc, int exemplarId)
        {
            DalPro.ExecuteSP("Emprestimo", new Dictionary<string, object>
            {
                { "@LeitorDoc", leitorDoc },
                { "@ExemplarID", exemplarId }
            });
        }

        // -------------------------------------------------------------------
        // DEVOLUÇÃO
        // Chama a SP [Devolucao] com o ID do exemplar.
        // A SP define DataEntrega = GETDATE() no registo de Emprestimos
        // e chama [ExemplarDisponivel] para recalcular a disponibilidade.
        // -------------------------------------------------------------------
        public void RealizarDevolucao(int exemplarId)
        {
            DalPro.ExecuteSP("Devolucao", new Dictionary<string, object>
            {
                { "@ExemplarID", exemplarId }
            });
        }

        // -------------------------------------------------------------------
        // EMPRÉSTIMOS ATIVOS COM STATUS
        // Chama a SP [sp_StatusEmprestimo] que devolve os empréstimos do leitor
        // com o status calculado com base nos dias decorridos:
        //   > 15 dias sem entrega  → "Atraso"
        //   13-15 dias             → "Devolução Urgente"
        //   11-12 dias             → "Devolver em breve"
        //   entregue               → "Entregue"
        //   restante               → "Dentro do Prazo"
        // -------------------------------------------------------------------
        public List<EmprestimoAtivoDTO> ObterEmprestimosAtivos(int leitorId)
        {
            var dt = DalPro.ExecuteSP("sp_StatusEmprestimo", new Dictionary<string, object>
            {
                { "@IdLeitor", leitorId }
            });

            // Mapeia as linhas do DataTable para a lista de DTOs
            var lista = new List<EmprestimoAtivoDTO>();
            foreach (System.Data.DataRow row in dt.Rows)
            {
                lista.Add(new EmprestimoAtivoDTO
                {
                    Id = Convert.ToInt32(row["Id"]),
                    StatusEmprestimo = row["Status Emprestimo"].ToString()
                });
            }

            return lista;
        }


    }
}
