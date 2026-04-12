using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaXPTOLibs.Helpers.Interfaces;

namespace BibliotecaXPTOLibs.Helpers
{
    public class ConnectionHelper : IConnectionHelper
    {
        private string _connectionstring = "";

        public string getConnectionString(string tagRepo)
        {
            if (tagRepo == "DB_Carros")
            {
                _connectionstring = "Server=LAPTOP-09V2MV7K\\SQLSERVER;Database=CarrosDB;Trusted_Connection=True;TrustServerCertificate=True";
            }

            return _connectionstring;
        }
    }
}
