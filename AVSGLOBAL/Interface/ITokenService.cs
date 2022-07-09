using AVSGLOBAL.Class.Models;

namespace AVSGLOBAL.Interface
{
    public interface ITokenService
    {
        //string BuildToken(string key, string issuer, Mdl_UserDTO user, ConnectionInfo Connection);
        //string GenerateJSONWebToken(string key, string issuer, Mdl_UserDTO user);

        string BuildToken(string key, string issuer, Mdl_User user, ConnectionInfo Connection);

        bool IsTokenValid(string key, string issuer, string token);
    }
}
