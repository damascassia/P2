using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Helpers;
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
    public class EmprestimosRepository :IEmprestimosRepository
    {
        private readonly IConnectionHelper _connectionHelper;
        public EmprestimosRepository(IConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
           
            DalPro.ConnectionString = _connectionHelper.getConnectionString("DB_Biblioteca");
        }
      
        
            public List<SituacaoEmprestimoDTO> ObterSituacaoLeitor(int leitorId)
            {
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
        public void RealizarRequisicao(string leitorDoc, int exemplarId)
        {
            DalPro.ExecuteSP("Emprestimo", new Dictionary<string, object>
        {
            { "@LeitorDoc", leitorDoc },
            { "@ExemplarID", exemplarId }
        });
        }

        public void RealizarDevolucao(int exemplarId)
        {
            DalPro.ExecuteSP("Devolucao", new Dictionary<string, object>
        {
            { "@ExemplarID", exemplarId }
        });
        }
    }
    }

