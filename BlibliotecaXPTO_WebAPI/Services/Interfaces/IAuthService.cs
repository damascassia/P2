namespace BlibliotecaXPTO_WebAPI.Services.Interfaces
{
    public interface IAuthService
    {
        public string Login(string username, string password);
        public string GenerateToken(string username, string userRole, string JWTKey);


    }
}
