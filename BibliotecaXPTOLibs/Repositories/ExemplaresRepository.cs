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
    public class ExemplaresRepository : IExemplaresRepository
    {
        private readonly IConnectionHelper _connectionHelper
    ;
        public ExemplaresRepository(IConnectionHelper connection)
        {
            _connectionHelper = connection;
        }
        public bool ChangeNucleo(ChangeNucleoDTO dto, string tagRepo)
        {
            var exemplar = GetById(dto.Exemplar_Id, tagRepo);

            if(exemplar == null)
                return false;

            string connection = _connectionHelper.getConnectionString(tagRepo);
            if (string.IsNullOrEmpty(connection))
                throw new Exception($"Erro de conexão com base de dados");

            DalPro.ConnectionString = connection;

            string sql = "EXEC sp_TransferirExemplarParaNucleo @Id_Exemplar, @Id_Nucleo";

            var param = new Dictionary<string, object>
             {
                 {"@Id_Exemplar", dto.Exemplar_Id },
                 {"@Id_Nucleo", dto.Nucleo_Id}
             };


            try
            {
                if (string.IsNullOrEmpty(connection))
                    throw new Exception($"Erro de Configuração: A tag '{tagRepo}' não possui uma ConnectionString válida.");

                DalPro.ConnectionString = connection;

                int rows = Convert.ToInt32(DalPro.Execute(sql, param));

                return rows > 0;
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

        public ExemplarDTO? GetById(int id, string tagRepo)
        {
            string sql = "SELECT * FROM Exemplares WHERE Id = @Id";

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

                return DalPro.Query<ExemplarDTO>(sql, param).FirstOrDefault();
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
