using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Helpers.Interfaces;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using DalProLib;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaXPTOLibs.Repositories
{
    public interface IUtilizadoresRepository
    {
        public void AlterarStatus(int Id, int StatusId, SqlTransaction trans = null);
        public void InitConnection();
        public int GetStatusID(int Id, SqlTransaction trans);
        public void DeleteLeitoresAntigos(SqlTransaction trans = null);
        public int RegistrarUtilizador(RegistrarUtilizadorDTO u, SqlTransaction trans);

    }
}
