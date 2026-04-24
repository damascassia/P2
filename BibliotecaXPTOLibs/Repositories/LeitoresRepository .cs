using BibliotecaXPTOLibs.Helpers.Interfaces;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using DalProLib;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaXPTOLibs.Repositories
{
    public class LeitoresRepository : ILeitoresRepository
    {
        private readonly IConnectionHelper _connectionHelper;

        private readonly ILeitoresRepository _leitoresRepository;
        public LeitoresRepository(IConnectionHelper connection)
        {
            _connectionHelper = connection;
            
        }
        public void CancelarInscricao(int leitorId, string tagRepo)
        {
            string connection = _connectionHelper.getConnectionString(tagRepo);

            try {
                    if (string.IsNullOrEmpty(connection))
                        throw new Exception($"Erro de Configuração: A tag {tagRepo} não possui uma ConnectionString válida.");
                  
                    DalPro.ExecuteSP("SP_CancelarInscricao", new Dictionary<string, object>
            {
                { "@IdLeitor", leitorId }
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
    } }

