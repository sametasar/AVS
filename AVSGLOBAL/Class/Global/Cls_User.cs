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
        DatabaseContext db = new DatabaseContext();

        //private readonly List<Mdl_UserDTO> users = new List<Mdl_UserDTO>();
        //public Cls_UserRepository()
        //{
        //    users.Add(new Mdl_UserDTO { UserName = "joydipkanjilal", Password = "joydip123", Role = "manager", Email="pantelga@gmail.com" });
        //    users.Add(new Mdl_UserDTO { UserName = "michaelsanders", Password = "michael321", Role = "developer", Email = "pantelga@gmail.com" });
        //    users.Add(new Mdl_UserDTO { UserName = "stephensmith", Password = "stephen123", Role = "tester", Email = "pantelga@gmail.com" });
        //    users.Add(new Mdl_UserDTO { UserName = "rodpaddock", Password = "rod123", Role = "admin", Email = "pantelga@gmail.com" });
        //    users.Add(new Mdl_UserDTO { UserName = "rexwills", Password = "rex321", Role = "admin", Email = "pantelga@gmail.com" });
        //}
        public async Task<Mdl_User> GetUser(Mdl_User userModel)
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
        
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6WyJzYW1ldCIsInNhbWV0Il0sInJvbGUiOlsiQWRtaW4iLCJBZG1pbiJdLCJlbWFpbCI6WyJzYW1ldGFzYXJAZ21haWwuY29tIiwic2FtZXRhc2FyQGdtYWlsLmNvbSJdLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoie1wiSVBBZGRyZXNzXCI6XCI6OjFcIixcIkJyb3dzZU5hbWVcIjpcIk1vemlsbGFcIixcIlRpbWVTdGFtcFwiOjE2NTczMzk3OTguNzcxNjgyNX0iLCJuYW1laWQiOiJmMzViMjQzNi1hYTdjLTRhZDAtYjI1NC01MTYzODU4YWUxYmEiLCJuYmYiOjE2NTczODAwODMsImV4cCI6MTY1NzM4MzY4MywiaWF0IjoxNjU3MzgwMDgzfQ.FSmgX-jd362DnsLUU2v08BLT-RJ7ISliNsv2pXZbMrM";
            string URI = Cls_Settings.MAIN_WEB_SERVICE;
            string myParameters = "";



            // var request = new RestRequest(Method.POST);
            // request.Headers.Add("Content-Type", "application/json");

           // Initialization.  
            string responseObj = string.Empty;  
            // HTTP GET.  
            using (var client = new HttpClient())  
            { 
            
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);                 
                client.BaseAddress = new Uri(Cls_Settings.MAIN_WEB_SERVICE);                 
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                  
                HttpResponseMessage response = new HttpResponseMessage();                 
                response = await client.GetAsync("/api/Get_Users").ConfigureAwait(false);  
                // Verification  
                if (response.IsSuccessStatusCode)  
                {  
                        Mdl_User t = null;
                        var str = await response.Content.ReadAsStringAsync();
                        t = JsonConvert.DeserializeObject<Mdl_User>(str);
                }  
            }

            return new Mdl_User();


            // try
            //     {                
            //         Console.Write("Parameters : " + myParameters + "\n");
            //         using (WebClient wc = new WebClient())
            //         {
            //             wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
            //             wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            //             wc.Headers[HttpRequestHeader.Accept] = "application/json";
            //             string responsebody = wc.UploadString(Cls_Settings.MAIN_WEB_SERVICE,"/api/Get_Users");
            //             //Console.Write("Output : " + responsebody + " ");
            //             dynamic dynObj = JsonConvert.DeserializeObject(responsebody);

            //             string json = responsebody;

            //             return new Mdl_User();
            //         }


            //     }
            //     catch (WebException exx)
            //     {
            //         string Hata = exx.ToString();
            //         return new Mdl_User();
            //     }  
        
        }
    }
}
