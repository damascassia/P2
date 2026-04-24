using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Models;
using BibliotecaXPTOLibs.Repositories;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using BlibliotecaXPTO_WebAPI.Services.Interfaces;
using DalProLib;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace BlibliotecaXPTO_WebAPI.Services
{
    public class UtilizadoresService : IUtilizadoresService
    {
        private readonly IUtilizadoresRepository _repo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UtilizadoresService(ILogger<UtilizadoresService> logger, IUtilizadoresRepository repo, IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _httpContextAccessor = httpContextAccessor;
        }
        private EnumRoles GetUserRole()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var roleString = httpContext?
                .User?
                .FindFirst("role")?
                .Value;

            if (string.IsNullOrEmpty(roleString))
                throw new Exception("Role não encontrada no token");

            return Enum.Parse<EnumRoles>(roleString);
        }
        public void AlterarStatus(int Id, AlterarStatusDTO dto)
        {
            _repo.InitConnection();
            
            var role = GetUserRole();

            if (role == EnumRoles.Leitor)
                throw new UnauthorizedAccessException();

            var trans = DalPro.BeginTransaction();
            try
            {
                _repo.AlterarStatus(Id, (int)dto.NovoStatusId, trans);
                DalPro.Commit(trans);
            }
            catch
            {
                 DalPro.Rollback(trans);
                 throw;
            }
        }

        public void DeleteLeitorAntigo()
        {
            _repo.InitConnection();
            var role = GetUserRole();

            if (role == EnumRoles.Leitor)
                throw new UnauthorizedAccessException();
            _repo.DeleteLeitoresAntigos();

        }
        public void RegistrarUtilizador(RegistrarUtilizadorDTO u)
        {
            var role = GetUserRole();
            if (role == EnumRoles.Leitor)
            {
                if (u.Id_TipoUser != (int)EnumRoles.Leitor)
                    throw new UnauthorizedAccessException();
            }
            if (role == EnumRoles.Bibli)
            {
                if (u.Id_TipoUser == (int)EnumRoles.Admin)
                    throw new UnauthorizedAccessException();
            }
            _repo.InitConnection();
            var trans = DalPro.BeginTransaction();
            try
            {
                _repo.RegistrarUtilizador(u, trans);
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