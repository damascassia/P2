using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Helpers.Interfaces;
using BibliotecaXPTOLibs.Models;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using DalProLib;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BibliotecaXPTOLibs.Repositories
{
    public class UtilizadoresRepository : IUtilizadoresRepository

    {
        private readonly string _connectionString;
        public UtilizadoresRepository (IConnectionHelper connectionHelper, IConfiguration configuration )
        {
            var tagGlobal = configuration["Settings:ActiveTag"] ?? "DB_BibliotecaPazu";
            _connectionString = connectionHelper.getConnectionString ( tagGlobal ); 
        }
        public void InitConnection()
        {
            DalPro.ConnectionString = _connectionString;
        }
        public void AlterarStatus(int Id, int StatusId, SqlTransaction trans = null)
        {
            InitConnection();
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
            InitConnection();
            string sql = "SELECT Id_Status FROM Utilizadores WHERE Id = @Id";
            object result = DalPro.ExecuteScalar(sql, new Dictionary<string, object> 
            { 
                { "@Id", Id } 
            }, trans);
            return Convert.ToInt32(result);
        }

        public void DeleteLeitoresAntigos(SqlTransaction trans = null)
        {
            string sql = "EXEC SP_EliminarAntigos";
            DalPro.Execute(sql, null, trans);
        }

        public int RegistrarUtilizador(RegistrarUtilizadorDTO u, SqlTransaction trans)
        {
            InitConnection();
            string sql = "EXEC SP_RegistarUtilizador @NomeCompleto, @NumeroDocumento, @TipoDocumento, @Email, @Morada, @Telefone, @UserName, @UserPassword, @Id_TipoUser";
            var parametros = new Dictionary<string, object>
            {
                {"@NomeCompleto", u.NomeCompleto },
                {"@NumeroDocumento", u.NumeroDocumento },
                {"@TipoDocumento", u.TipoDocumento },
                {"@Email", u.Email },
                {"@Morada", u.Morada },
                {"@Telefone", u.Telefone },
                {"@UserName", u.UserName },
                {"@UserPassword", u.UserPassword },
                {"@Id_TipoUser", u.Id_TipoUser }

            };
            DalPro.Execute(sql, parametros, trans);
            return 1;
        }
    }
}
