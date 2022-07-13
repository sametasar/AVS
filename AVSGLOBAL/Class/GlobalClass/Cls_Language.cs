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
using Microsoft.AspNetCore.Http;


namespace AVSGLOBAL.Class.GlobalClass
{
    /// <summary>
    /// Uygulamanın çoklu dil desteği için geliştirilmiştir.
    /// </summary>
    public class Cls_Language
    {        
        /// <summary>
        /// Çoklu dil için geliştirildi. Bu uygulamada kullanılan tüm sözlük bilgisini döndürür.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetLanguageDictionary(Mdl_User User,string UserInfo)
        {
            Cls_WebRequestOption options = new Cls_WebRequestOption();
            options.ActionType = Enm_ActionTypes.Post;
            options.ApiUrl = "/language/";
            options.MethodName = "Translate";
            options.Token = User.LastServiceToken; //Servise gidecek requestlerde servisdeki token kullanılmalı!
            options.TokenType = Enm_TokenTypes.Jwt;

            Mdl_KeyValue KeyValue1 = new Mdl_KeyValue();
            KeyValue1.Key = "UserInfo";

            //Servisler arasındaki haberleşmelerde Kullanıcı bilgileri gibi kritik öneme sahip tüm bilgileri her zaman şifreli yapıyorum!
            KeyValue1.Value = Cls_Tools.EnCrypt(UserInfo);        
            
            List<Mdl_KeyValue> ParametreListem = new List<Mdl_KeyValue>();
            ParametreListem.Add(KeyValue1);           

            options.Parameters =  ParametreListem;

            options.Url = Cls_Settings.MAIN_WEB_SERVICE;

            string str = await Cls_WebRequest.SendRequest(options);

            return str;
            
        }
    
    }
}