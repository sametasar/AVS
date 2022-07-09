using AVSGLOBAL.Class.Models;
using AVSGLOBAL.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;

namespace AVSGLOBAL.Class.Global
{
    public class Cls_TokenService : ITokenService
    {
        private const double EXPIRY_DURATION_MINUTES = 30;

        public string BuildToken(string key, string issuer, Mdl_User user, ConnectionInfo Connection)
        {

            #region OTANTİKE OLAN KULLANICININ KİMLİK KARTI YAPILANDIRILMASI - CLAIM OLUŞTURULUYOR
            //Claimler otantike olan kullanıcının kimlik bilgisinde bulunacak özellikleri yapılandırıyor.Her eklenen claim aslında sisteme giriş yapan kullanıcı için oluşturulmuş bir özellik.
            //Kimlik yapısını oluşturuyoruz.
            List<Claim> ClaimListesi = new List<Claim>();
            Claim ClaimA = new Claim(ClaimTypes.Name, user.Name);
            Claim ClaimB = new Claim(ClaimTypes.Role, "Admin"); //todo Veritabanından gelen rolü burada tanımla! ileride yapılacak.
            Claim ClaimC = new Claim(ClaimTypes.Email, user.Email);

            ///Doğrudan tanımlayamadığım özellikleri claime dolaylı olarak tanımlıyorum!
            Cls_ClaimUserData UserData = new Cls_ClaimUserData();
            UserData.IPAddress = Connection.RemoteIpAddress.ToString();
            UserData.TimeStamp =  Cls_Tools.DateTime_To_Timestamp(DateTime.Now);
            UserData.BrowseName = "Mozilla";
            UserData.TimeStamp = Cls_Tools.DateTime_To_Timestamp(DateTime.Now);
            Claim ClaimD = new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(UserData));
            Claim ClaimE = new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString());

            ClaimListesi.Add(ClaimA);
            ClaimListesi.Add(ClaimB);
            ClaimListesi.Add(ClaimC);
            ClaimListesi.Add(ClaimD);
            ClaimListesi.Add(ClaimE);

            #endregion

            SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            SigningCredentials Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken TokenDescriptor = new JwtSecurityToken(issuer, issuer, ClaimListesi,
            expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: Credentials);

            return new JwtSecurityTokenHandler().WriteToken(TokenDescriptor);
        }

        //public string GenerateJSONWebToken(string key, string issuer, Mdl_UserDTO user)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(issuer, issuer,
        //      null,
        //      expires: DateTime.Now.AddMinutes(120),
        //      signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
        public bool IsTokenValid(string key, string issuer, string token)
        {
            var mySecret = Encoding.ASCII.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
