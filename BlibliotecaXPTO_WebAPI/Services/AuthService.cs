using BibliotecaXPTOLibs.Helpers.Interfaces;
using BibliotecaXPTOLibs.Models;
using BlibliotecaXPTO_WebAPI.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public string Login(string username, string password)
        {
            var utilizadorValido = _loginHelper.ValidarNoBanco(username, password);
            if (utilizadorValido == null)
            {
                return null;
            }
            string chave = _config["App:JWT:SECRET_KEY"];
            return GenerateToken(utilizadorValido.UserName, utilizadorValido.Id_TipoUser.ToString(), chave);
        }
        public string GenerateToken(string username, string userRole, string JWTKey)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTKey));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, ((EnumRoles)int.Parse(userRole)).ToString()),
                    new Claim("Plataforma", "BibliotecaXPTO"),
                    new Claim("Plataforma2", "BibliotecaPaz"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: "BibliotecaXPTO",
                    audience: "BibliotecaXPTO",
                    claims: claims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gerar token: " + ex.Message);
            }
        }
    }
}
