using AVSGLOBAL.Class.Models;
using System.IdentityModel.Tokens.Jwt;

namespace AVSGLOBAL.Interface
{
    public interface ITokenService
    {
        //string BuildToken(string key, string issuer, Mdl_UserDTO user, ConnectionInfo Connection);
        //string GenerateJSONWebToken(string key, string issuer, Mdl_UserDTO user);

        /// <summary>
        /// Tokenimizi bu client uygulamasının kendi backendi oluşturacaksa bu metot kullanılır!
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Connection"></param>
        /// <returns></returns>
        string BuildToken(Mdl_User user, ConnectionInfo Connection);

        /// <summary>
        /// Tokenimizi başka bir web servis oluşturacaksa bu metot kullanılır!
        /// </summary>
        /// <param name="TokenDescriptor"></param>
        /// <returns></returns>
        string BuildMainServiceToken(JwtSecurityToken TokenDescriptor);

        bool IsTokenValid(string token);
    }
}
