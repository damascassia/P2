using BibliotecaXPTOLibs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaXPTOLibs.Helpers.Interfaces
{
    public interface ILoginHelper
    {
        public void InitConnection();
        public UtilizadorAutenticadoDTO ValidarNoBanco(string username, string password);


    }
}
