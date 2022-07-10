using AVSGLOBAL.Class.Models;
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

namespace AVSGLOBAL.Class.Global
{
    public class Cls_User : IUser
    {        
        public async Task<Mdl_User> GetUser(string UserID)
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
            options.ApiUrl = "/user/";
            options.MethodName = "Get_User"; 
          
            Cls_KeyValue KeyValue1 = new Cls_KeyValue();
            KeyValue1.Key = "ID";
            KeyValue1.Value = UserID;
                      
            List<Cls_KeyValue> ParametreListem = new List<Cls_KeyValue>();
            ParametreListem.Add(KeyValue1);
          
            options.Parameters =  ParametreListem;

            options.Url = Cls_Settings.MAIN_WEB_SERVICE;

            string json = await Cls_WebRequest.SendRequest(options);

            Mdl_User user = JsonConvert.DeserializeObject<Mdl_User>(json);

            return user;        
        }


        public async Task<Mdl_User> Authenticate(Mdl_User userModel)
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
          
            Cls_KeyValue KeyValue1 = new Cls_KeyValue();
            KeyValue1.Key = "Email";
            KeyValue1.Value = userModel.Email;

            Cls_KeyValue KeyValue2 = new Cls_KeyValue();
            KeyValue2.Key = "Password";
            KeyValue2.Value = userModel.Password;
            
            List<Cls_KeyValue> ParametreListem = new List<Cls_KeyValue>();
            ParametreListem.Add(KeyValue1);
            ParametreListem.Add(KeyValue2);

            options.Parameters =  ParametreListem;

            options.Url = Cls_Settings.MAIN_WEB_SERVICE;

            string json = await Cls_WebRequest.SendRequest(options);

            Mdl_User user = JsonConvert.DeserializeObject<Mdl_User>(json);

            //user gel fakat tokenim olması gerektiği değerde değil onu tekrar Decrypt edeceğim ve olması gereken değerde olacak!
            //Taşıma işlemlerinde json dönüşümlerinde token içindeki json karakterleri json dönüşümlerini patlatıyor!

            user.LastToken =  new Cls_TokenService().TokenDeCrypt(user.LastToken);

            return user;        
        }

    }
}
