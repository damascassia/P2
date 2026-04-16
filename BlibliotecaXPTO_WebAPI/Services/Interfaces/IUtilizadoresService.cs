using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Models;
using BibliotecaXPTOLibs.Repositories;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using DalProLib;
using Microsoft.Data.SqlClient;


namespace BlibliotecaXPTO_WebAPI.Services.Interfaces
{
    public interface IUtilizadoresService 
    {
        public void AlterarStatus(int Id, AlterarStatusDTO dto, SqlTransaction trans = null);
    }
}

