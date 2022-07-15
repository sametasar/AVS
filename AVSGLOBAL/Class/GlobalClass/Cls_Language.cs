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
        public async Task<string> GetLanguageDictionary()
        {
            Cls_WebRequestOption options = new Cls_WebRequestOption();
            options.ActionType = Enm_ActionTypes.Post;
            options.ApiUrl = "/language/";
            options.MethodName = "GetLanguageDictionary";
            options.Url = Cls_Settings.MAIN_WEB_SERVICE;
            string json = await Cls_WebRequest.SendRequest(options);
            return json;            
        }
    
    }
}