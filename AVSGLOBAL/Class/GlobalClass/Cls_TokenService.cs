using AVSGLOBAL.Models.GlobalModel;
using AVSGLOBAL.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace AVSGLOBAL.Class.GlobalClass
{
    
    /// <summary>
    /// Normal Authorize attribute e ek özellikler kazandırıyorum. Bunların başında token çalındığında farklı bir konumda o token kullanılırsa.
    /// Token ip değişikliğini algılayacak ve çalışmaz hale gelecek.
    /// </summary>
    public class AuthorizeAction: IAuthorizationFilter {
    private readonly string _actionName;
    private readonly string _roleType;
            
    public AuthorizeAction(string actionName, string roleType) {
        _actionName = actionName;
        _roleType = roleType;
    }
    public void OnAuthorization(AuthorizationFilterContext context) {
        string _roleType = context.HttpContext.Request?.Headers["role"].ToString();
        switch (_actionName) {
            case "MainWindow":
                if (!_roleType.Contains("Admin")) context.Result = new JsonResult("Permission denined!");
            break;
        }
    }
}

    public class AuthorizeAVS: TypeFilterAttribute {
    public AuthorizeAVS(string actionName, string roleType): base(typeof(AuthorizeAction)) {
        Arguments = new object[] {
            actionName,
            roleType
        };
    }
}


    public class Cls_TokenService : ITokenService
    {              
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
            Claim ClaimB = new Claim(ClaimTypes.Role, user.IdentityRole);
            Claim ClaimC = new Claim(ClaimTypes.Email, user.Email);

            ClaimListesi.Add(ClaimA);
            ClaimListesi.Add(ClaimB);
            ClaimListesi.Add(ClaimC);

            /*   ŞU AN İÇİN EXTRA AYARLARI İPTAL EDİYORUM TOKEN ÇOK BÜYÜYOR DECRYPT EDERKEN PROBLEMLER YAŞADIM İLERİDE TEKRAR BAKACAM!

            ///Doğrudan tanımlayamadığım özellikleri claime dolaylı olarak tanımlıyorum!
            Mdl_ClaimUserData UserData = new Mdl_ClaimUserData();
            UserData.IPAddress = Connection.RemoteIpAddress.ToString();
            UserData.TimeStamp =  Cls_Tools.DateTime_To_Timestamp(DateTime.Now);
            UserData.BrowseName = "Mozilla";
            UserData.TimeStamp = Cls_Tools.DateTime_To_Timestamp(DateTime.Now);
            
            Mdl_Claim ClaimUserInfo = new Mdl_Claim();
            ClaimUserInfo.UserExtraData = UserData;
            ClaimUserInfo.User = user;            
            
            Claim ClaimD = new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(ClaimUserInfo));
            Claim ClaimE = new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString());

           
            ClaimListesi.Add(ClaimD);
            ClaimListesi.Add(ClaimE);

            */

            #endregion


            #region Token Oluşturma Yöntem1

            SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Cls_Settings.JWTKEY));
            SigningCredentials Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken TokenDescriptor = new JwtSecurityToken(Cls_Settings.JWTISSUER, Cls_Settings.JWTISSUER, ClaimListesi,
            expires: DateTime.Now.AddMinutes(Cls_Settings.TokenExpireMinute), signingCredentials: Credentials);          
            //Tokeni Şifreliyorum yada diğer bir değişle filtreliyorum Obje İçinde Taşırken json dönüşümlerinde patlıyor, patlamaması için json karakterlerini değiştiriyorum DeCrypt ederek eski haline getirebilirsin!
            return new JwtSecurityTokenHandler().WriteToken(TokenDescriptor);

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
