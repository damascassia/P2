using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using BlibliotecaXPTO_WebAPI.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace BlibliotecaXPTO_WebAPI.Services
{
    /*public class EmprestimosService : IEmprestimosService
    {
        private readonly IEmprestimosRepository _emprestimosRepository;

        public EmprestimosService(IEmprestimosRepository emprestimosRepository)
        {
            _emprestimosRepository = emprestimosRepository;
        }

        // -------------------------------------------------------------------
        // REQUISIÇÃO
        // Delega ao repository e trata os possíveis erros SQL lançados pelas SPs:
        //   - Leitor não registado
        //   - Leitor inativo ou suspenso
        //   - Leitor já tem 4 exemplares requisitados
        //   - Exemplar não disponível
        // -------------------------------------------------------------------
        public (bool Sucesso, string Mensagem) RealizarRequisicao(RequisicaoDTO dto)
        {
            try
            {
                _emprestimosRepository.RealizarRequisicao(dto.LeitorDoc, dto.ExemplarId);
                return (true, "Requisição realizada com sucesso.");
            }
            catch (SqlException ex)
            {
                // As SPs usam RAISERROR com mensagens descritivas — devolvemos directamente ao cliente
                return (false, ex.Message);
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        // -------------------------------------------------------------------
        // DEVOLUÇÃO
        // Delega ao repository. A SP não valida se o leitor é dono do exemplar,
        // por isso neste service poderia adicionar-se essa validação caso necessário.
        // -------------------------------------------------------------------
        public (bool Sucesso, string Mensagem) RealizarDevolucao(DevolucaoDTO dto)
        {
            try
            {
                _emprestimosRepository.RealizarDevolucao(dto.ExemplarId);
                return (true, "Devolução registada com sucesso.");
            }
            catch (SqlException ex)
            {
                return (false, ex.Message);
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        // -------------------------------------------------------------------
        // EMPRÉSTIMOS ATIVOS
        // -------------------------------------------------------------------
        public (bool Sucesso, string Mensagem, List<EmprestimoAtivoDTO> Dados) ObterEmprestimosAtivos(int leitorId)
        {
            try
            {
                var dados = _emprestimosRepository.ObterEmprestimosAtivos(leitorId);
                return (true, "OK", dados);
            }
            catch (SqlException ex)
            {
                return (false, ex.Message, null);
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}", null);
            }
        }
    }*/
}