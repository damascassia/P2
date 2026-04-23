using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Helpers.Interfaces;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using DalProLib;
using Microsoft.Data.SqlClient;

namespace BibliotecaXPTOLibs.Repositories
{
    public class AssuntoRepository : IAssuntoRepository
    {
        private readonly IConnectionHelper _connectionHelper;
        public AssuntoRepository(IConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;   
        }
        public AssuntoDTO? GetById(int id, string tagRepo)
        {
            string sql = "SELECT * FROM Assuntos WHERE Id = @Id";

            var param = new Dictionary<string, object>
        {
            {"Id", id}
        };

            string connection = _connectionHelper.getConnectionString(tagRepo);

            try
            {
                if (string.IsNullOrEmpty(connection))
                    throw new Exception($"Erro de Configuração: A tag '{tagRepo}' não possui uma ConnectionString válida.");

                DalPro.ConnectionString = connection;

                return DalPro.Query<AssuntoDTO>(sql, param).FirstOrDefault();
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
    }
}
