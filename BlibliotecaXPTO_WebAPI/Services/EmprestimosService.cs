using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using BlibliotecaXPTO_WebAPI.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace BlibliotecaXPTO_WebAPI.Services
{
   

    public class EmprestimosService : IEmprestimosService
    {
      

        private readonly IEmprestimosRepository _repo;
        private readonly ILogger _logger;

        public EmprestimosService(ILogger<EmprestimosService> logger, IEmprestimosRepository repo)
        {
            _repo = repo;
            _logger = logger;
        }

        public void RealizarRequisicao(EmprestimoDTO dto)
        {
            _logger.LogInformation($"Requisição: leitor={dto.LeitorDoc} exemplar={dto.ExemplarId}");
            _repo.RealizarRequisicao(dto.LeitorDoc, dto.ExemplarId);
        }

        public void RealizarDevolucao(DevolucaoDTO dto)
        {
            _logger.LogInformation($"Devolução: exemplar={dto.ExemplarId}");
            _repo.RealizarDevolucao(dto.ExemplarId);
        }

        public List<SituacaoEmprestimoDTO> ObterSituacaoLeitor(int leitorId)
        {
            try
            {
                var dados = _repo.ObterSituacaoLeitor(leitorId);
                return dados;
            }
            catch (SqlException)
            {
           
                return new List<SituacaoEmprestimoDTO>();
            }
            catch (Exception)
            {
              
                return new List<SituacaoEmprestimoDTO>();
            }
        }
    }
}