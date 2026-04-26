using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Helpers;
using BibliotecaXPTOLibs.Helpers.Interfaces;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using DalProLib;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        }


        public List<SituacaoEmprestimoDTO> ObterSituacaoLeitor(int leitorId, string tagRepo)

        {
            string connection = _connectionHelper.getConnectionString(tagRepo);
            try
            {
                if (string.IsNullOrEmpty(connection))
                    throw new Exception($"Erro de Configuração: A tag {tagRepo} não possui uma ConnectionString válida.");

                var dt = DalPro.ExecuteSP("sp_StatusEmprestimo", new Dictionary<string, object>
        {
            { "@IdLeitor", leitorId }
        });

                var lista = new List<SituacaoEmprestimoDTO>();
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    lista.Add(new SituacaoEmprestimoDTO
                    {
                        NomeNucleo = row["NomeNucleo"].ToString(),
                        Titulo = row["Titulo"].ToString(),
                        Autor = row["Autor"].ToString(),
                        DataInicio = Convert.ToDateTime(row["DataInicio"]),
                        DiasRestantes = Convert.ToInt32(row["DiasRestantes"]),
                        StatusEmprestimo = row["StatusEmprestimo"].ToString()
                    });
                }
                return lista;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro de banco de dados: {ex}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado: {ex}");
            }
        }

        public void RealizarRequisicao(string leitorDoc, int exemplarId, string tagRepo)
        {
            string connection = _connectionHelper.getConnectionString(tagRepo);
            try
            {
                if (string.IsNullOrEmpty(connection))
                    throw new Exception($"Erro de Configuração: A tag {tagRepo} não possui uma ConnectionString válida.");
                DalPro.ExecuteSP("Emprestimo", new Dictionary<string, object>
        {
            { "@LeitorDoc", leitorDoc },
            { "@ExemplarID", exemplarId }
        });
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro de base de dados: {ex.Message}");
            }
            catch (Exception ex)
            { throw new Exception($"Erro inesperado:{ex.Message}"); }
        }

        public void RealizarDevolucao(int exemplarId, string tagRepo)
        {
            string connection = _connectionHelper.getConnectionString(tagRepo);
            try
            {
                if (string.IsNullOrEmpty(connection))
                    throw new Exception($"Erro de Configuração: A tag {tagRepo} não possui uma ConnectionString válida.");
               
                DalPro.ExecuteSP("Devolucao", new Dictionary<string, object>
        {
            { "@ExemplarID", exemplarId }
        });
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro de base de dados: {ex.Message}");

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado:{ex.Message}");
            }
        }
    }
}

