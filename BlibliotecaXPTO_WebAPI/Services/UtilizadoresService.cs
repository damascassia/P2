using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Models;
using BibliotecaXPTOLibs.Repositories;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using BlibliotecaXPTO_WebAPI.Services.Interfaces;
using DalProLib;
using Microsoft.Data.SqlClient;


namespace BlibliotecaXPTO_WebAPI.Services
{
    public class UtilizadoresService : IUtilizadoresService
    {
        private readonly IUtilizadoresRepository _repo;

        public UtilizadoresService(ILogger<UtilizadoresService> logger, IUtilizadoresRepository repo)
        {
            _repo = repo;
        }
        public void AlterarStatus(int Id, AlterarStatusDTO dto, SqlTransaction trans = null)
        {
            trans = DalPro.BeginTransaction();
            try 
            {
                Utilizadores u = new Utilizadores
                {
                    Id = Id,
                    Id_Status = (int)dto.NovoStatusId
                };
                _repo.AlterarStatus(u.Id, u.Id_Status, trans);
                DalPro.Commit(trans);
            }
            catch 
            {
                DalPro.Rollback(trans);
                throw;
            }
        }
    }
}
