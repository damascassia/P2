
using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Helpers.Interfaces;
using DalProLib;
using BibliotecaXPTOLibs.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace BibliotecaXPTOLibs.Helpers
{
    public class LoginHelper : ILoginHelper
    {
        private readonly string _connectionString;

        public LoginHelper(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("BibliotecaPazu");
        }
        public void InitConnection()
        {
            DalPro.ConnectionString = _connectionString;
        }
        public UtilizadorAutenticadoDTO ValidarNoBanco(string username, string password)
        {
            InitConnection();
            string sql = "EXEC LoginConta @UserName, @Password";
            var parametros = new Dictionary<string, object>
        {
        { "@UserName", username },
        { "@Password", password }
        };
            var result = DalPro.Query<UtilizadorAutenticadoDTO>(sql, parametros);

            if (result.Count > 0)
            {
                return result[0];
            }
            return null;
        }

    }
}