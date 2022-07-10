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

         /// <summary>
        /// Bu metodun yapılmasının amacı token değerini bir user objesinin Token propertysine veriyorum.
        /// Fakat user objesini taşıma esnasında json a çevirdiğimde içinde bulunan tone karakterleri süslü parantezler ,tek tırnaklar, virgüller , büyük parantezler ve json içinde olabilecek diğer karakterler
        /// Json ı tekrar objeye çevirmemi engelliyor. Bu yüzden özel karakterler ile bu token içinde olabilecek diğer özel karakterleri
        /// değiştiriyorum bu karakterleri tekrar eski haline çevirerek tokenımı doğruluyorum.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string TokenCrypt(string token)
        {
            //json içinde bulunan karakter listesi
            //"{},:'\"[]()"
            //  { = ȶ
            //  } = ȸ
            //  , = Ⱥ
            //  : = Ƚ
            //  ' = Ʉ
            // \" = Ɋ
            //  [ = Ɏ
            //  ] = ɓ
            //  ( = ɗ
            //  ) = ɷ

            token = token.Replace("{", "ȶ").Replace("}", "ȸ").Replace(",", "Ⱥ").Replace(":", "Ƚ").Replace("'", "Ʉ").Replace("\"", "Ɋ").Replace("[", "Ɏ").Replace("]", "ɓ").Replace("(", "ɗ").Replace(")", "ɷ");

            return token;
        }


        /// <summary>
        /// Crypt edilen Tokenimi tekrar eski haline getirmek için kullanıyorum.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string TokenDeCrypt(string token)
        {
            token = token.Replace("ȶ", "{").Replace("ȸ", "}").Replace("Ⱥ", ",").Replace("Ƚ", ":").Replace("Ʉ", "'").Replace("Ɋ", "\"").Replace("Ɏ", "[").Replace("ɓ", "]").Replace("ɗ", "(").Replace("ɷ", ")");

            return token;
        }
        
        /// <summary>
        /// Yeni bir JWT Token oluşturmak üzere geliştirilmiştir.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="issuer"></param>
        /// <param name="user"></param>
        /// <param name="Connection"></param>
        /// <returns></returns>
        public string BuildToken(Mdl_User user, ConnectionInfo Connection)
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


            #region Token Oluşturma Yöntem1

            SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Cls_Settings.JWTKEY));
            SigningCredentials Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken TokenDescriptor = new JwtSecurityToken(Cls_Settings.JWTISSUER, Cls_Settings.JWTISSUER, ClaimListesi,
            expires: DateTime.Now.AddMinutes(Cls_Settings.TokenExpireMinute), signingCredentials: Credentials);          
            //Tokeni Şifreliyorum yada diğer bir değişle filtreliyorum Obje İçinde Taşırken json dönüşümlerinde patlıyor, patlamaması için json karakterlerini değiştiriyorum DeCrypt ederek eski haline getirebilirsin!
            return TokenCrypt(new JwtSecurityTokenHandler().WriteToken(TokenDescriptor));

            #endregion          

        }


        /// <summary>
        /// Main web serviceden gelen token ayarlarını tokena dönüştüren metot.
        /// </summary>
        /// <param name="TokenDescriptor"></param>
        /// <returns></returns>
        public string BuildMainServiceToken(JwtSecurityToken TokenDescriptor)
        {
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
        public bool IsTokenValid(string token)
        {
            var mySecret = Encoding.ASCII.GetBytes(Cls_Settings.JWTKEY);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = Cls_Settings.JWTISSUER,
                    ValidAudience = Cls_Settings.JWTISSUER,
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
