using AVSGLOBAL.Models.GlobalModel;
using AVSGLOBAL.Interface;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AVSGLOBAL.Class.Dal;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Http.Headers;
using System.Dynamic;

namespace AVSGLOBAL.Class.GlobalClass
{
    public class Cls_User : IUser
    {        
        /// <summary>
        /// Kullanıcı ID değerini web servise gönderdiğinizde o ID değerine sahip kullanıcı bilgilerini getirir.
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<Mdl_User> GetUser(string UserID)
        {   
            //todo GetUser metodu geliştirilecek.
            throw new InvalidOperationException("Logfile cannot be read-only");
        }

        /// <summary>
        /// Kullancı eposta ve şifre bilgileri doğrultusunda web servise login kontrolü talebinde bulunur. /api/Authenticate
        /// Authenticate ise AVSCATERING veri tabanında bulunan User adlı tabloda kuyllanıcı var mı yokmu kontrolünü sağlar.
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<Mdl_User> Authenticate(Mdl_User User)
        {   

            /* #region  kullanıcıyı veritabanından kontrol etmek için! */
            // List<Mdl_User> UserList = new List<Mdl_User>();
            // UserList = db.User.Where(x => x.Name == userModel.Name && x.Password == userModel.Password).ToList<Mdl_User>();

            // if (UserList.Count > 0)
            // {
            //     return UserList[0];
            // }
            // else
            // {
            //     return new Mdl_User();
            // }
            /* #endregion */

            Cls_WebRequestOption options = new Cls_WebRequestOption();
            options.ActionType = Enm_ActionTypes.Get;
            options.ApiUrl = "/api/";
            options.MethodName = "Authenticate";       
          
            Mdl_KeyValue KeyValue1 = new Mdl_KeyValue();
            KeyValue1.Key = "Email";
            KeyValue1.Value = User.Email;

            Mdl_KeyValue KeyValue2 = new Mdl_KeyValue();
            KeyValue2.Key = "Password";
            KeyValue2.Value = User.Password;
            
            List<Mdl_KeyValue> ParametreListem = new List<Mdl_KeyValue>();
            ParametreListem.Add(KeyValue1);
            ParametreListem.Add(KeyValue2);

            options.Parameters =  ParametreListem;

            options.Url = Cls_Settings.MAIN_WEB_SERVICE;

            string json = await Cls_WebRequest.SendRequest(options);

            Mdl_User user = JsonConvert.DeserializeObject<Mdl_User>(json);

            //user gel fakat tokenim olması gerektiği değerde değil onu tekrar Decrypt edeceğim ve olması gereken değerde olacak!
            //Taşıma işlemlerinde json dönüşümlerinde token içindeki json karakterleri json dönüşümlerini patlatıyor!
            
            return user;        
        }

    
        /// <summary>
        /// Kullanıcının ID değerini ve Client tarafında oluşan tokenini alır ve servise yollar servisde kullanıcının veritabanında bulunan kayıtında LastToken adlı kolonunu günceller.
        /// </summary>
        /// <param name="UserID">LastToken bilgisi güncellenecek olan kullanıcı ID değeri.</param>
        /// <param name="ClientToken">Kullanıcının son token bilgisi.</param>
        /// <returns></returns>
        public async Task<string> UpdateClientTokenAndIP(Mdl_User User)
        {   
            Cls_WebRequestOption options = new Cls_WebRequestOption();
            options.ActionType = Enm_ActionTypes.Post;
            options.ApiUrl = "/user/";
            options.MethodName = "UpdateClientTokenAndIP";
            options.Token = User.LastServiceToken; //Servise gidecek requestlerde servisdeki token kullanılmalı!
            options.TokenType = Enm_TokenTypes.Jwt;

            Mdl_KeyValue KeyValue1 = new Mdl_KeyValue();
            KeyValue1.Key = "UserID";
            KeyValue1.Value = User.ID.ToString();

            Mdl_KeyValue KeyValue2 = new Mdl_KeyValue();
            KeyValue2.Key = "ClientToken";
            KeyValue2.Value = User.LastToken; //Burada Clientın tokenı sadece bir parametre olarak kullanılacak amacı veritabanında clienttoken değerini güncellemek. Client Token yalnızca client sisteminde güvenliği sağlamakta.

            Mdl_KeyValue KeyValue3 = new Mdl_KeyValue();
            KeyValue3.Key = "LastIP";
            KeyValue3.Value = User.LastIP;
            
            List<Mdl_KeyValue> ParametreListem = new List<Mdl_KeyValue>();
            ParametreListem.Add(KeyValue1);
            ParametreListem.Add(KeyValue2);
            ParametreListem.Add(KeyValue3);

            options.Parameters =  ParametreListem;

            options.Url = Cls_Settings.MAIN_WEB_SERVICE;

            string str = await Cls_WebRequest.SendRequest(options);

            return str;
            
        }

        public async Task<List<Mdl_User>> Get_Users(Mdl_User User)
        {
            Cls_WebRequestOption options = new Cls_WebRequestOption();
            options.ActionType = Enm_ActionTypes.Get;
            options.ApiUrl = "/user/";
            options.MethodName = "Get_Users";
            options.Token = User.LastServiceToken; //Servise gidecek requestlerde servisdeki token kullanılmalı!
            options.TokenType = Enm_TokenTypes.Jwt;
            
            options.Url = Cls_Settings.MAIN_WEB_SERVICE;

            string json = await Cls_WebRequest.SendRequest(options);

            List<Mdl_User> UserList = JsonConvert.DeserializeObject<List<Mdl_User>>(json);

            return UserList;
        }
    
    }
}