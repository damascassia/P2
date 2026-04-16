using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Helpers.Interfaces;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using DalProLib;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BibliotecaXPTOLibs.Repositories
{
    public class UtilizadoresRepository : IUtilizadoresRepository
    {
        public void AlterarStatus(int Id, int StatusId, SqlTransaction trans = null)
        {
            string Sql = @"UPDATE Utilizadores SET ID_Status = @Status WHERE Id = @Id";

            var parametros = new Dictionary<string, object>
            {
                {"@Id", Id },
                {"@Status", StatusId }
            };
            DalPro.Execute(Sql, parametros, trans);
        }

        public int GetStatusID(int Id, SqlTransaction trans)
        {
            string sql = "SELECT Id_Status FROM Utilizadores WHERE Id = @id";
            object result = DalPro.ExecuteScalar(sql, new Dictionary<string, object> { { "@Id", Id } }, trans);
            return Convert.ToInt32(result);
        }
    }
}
