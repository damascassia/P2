

using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Helpers.Interfaces;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using DalProLib;
using Microsoft.Data.SqlClient;

namespace BibliotecaXPTOLibs.Repositories
{
    public class ObrasRepository : IObrasRepository
    {
        private readonly IConnectionHelper _connectionHelper;
        public ObrasRepository(IConnectionHelper connection)
        {
            _connectionHelper = connection;
        }

        public int Insert(CreateObraDTO dto, string tagRepo)
        {
            string connection = _connectionHelper.getConnectionString(tagRepo);
            if (string.IsNullOrEmpty(connection))
                throw new Exception($"Erro de conexão com base de dados");

            DalPro.ConnectionString = connection;

            string sql = "EXEC sp_AdicionarObra @Autor, @Titulo, @AssuntoId";

            var param = new Dictionary<string, object>
             {
                 {"@Autor", dto.Autor},
                 {"@Titulo", dto.Titulo},
                 {"@AssuntoId", dto.Assunto_Id}
             };


            try
            {
                if (string.IsNullOrEmpty(connection))
                    throw new Exception($"Erro de Configuração: A tag '{tagRepo}' não possui uma ConnectionString válida.");

                DalPro.ConnectionString = connection;

                int id = Convert.ToInt32(DalPro.ExecuteScalar(sql, param));

                return id;
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
