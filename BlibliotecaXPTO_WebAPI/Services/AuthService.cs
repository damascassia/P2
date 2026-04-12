using BibliotecaXPTOLibs.Helpers.Interfaces;
using BlibliotecaXPTO_WebAPI.Services.Interfaces;

namespace BlibliotecaXPTO_WebAPI.Services
{
    public class AuthService : IAuthService //Classe para token e login, fiz DI para helpers necessários
    {
        private readonly ILoginHelper _loginHelper;
        private readonly IConfiguration _config;

        public AuthService(ILoginHelper loginHelper, IConfiguration config)
        {
            _config = config;
            _loginHelper = loginHelper;
        }
    }
}
