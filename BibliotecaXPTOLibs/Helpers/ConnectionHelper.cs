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
            if (tagRepo == "DB_BibliotecaPazu")
            {
                _connectionstring = "Server=.\\SQLSERVER;Database=BibliotecaPazu;Trusted_Connection=True;TrustServerCertificate=True";
            }

            if (tagRepo == "DB_BibliotecaXPTO")
            {
                _connectionstring = "Server=.\\SQLSERVER;Database=BibliotecaXPTO;Trusted_Connection=True;TrustServerCertificate=True";
            }

            return _connectionstring;


        }
    }
}
