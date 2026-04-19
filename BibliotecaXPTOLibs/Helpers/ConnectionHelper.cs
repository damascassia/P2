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
            if (tagRepo == "DB_Biblioteca")
            {
                _connectionstring = "Server=localhost;Database=BibliotecaPazu;Trusted_Connection=True;TrustServerCertificate=True";
            }

            return _connectionstring;
        }
    }
}
