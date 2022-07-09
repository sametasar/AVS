using AVSGLOBAL.Class.Models;
using AVSGLOBAL.Interface;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AVSGLOBAL.Class.Dal;

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
        public Mdl_User GetUser(Mdl_User userModel)
        {

            // Mdl_User Kullanici = new Mdl_User();        
            // Kullanici.Password = "asar";
            // Kullanici.Name = "samet";
            // Kullanici.Email = "sametasar@gmail.com";
            // Kullanici.Create_Date = DateTime.Now;
            // Kullanici.LastIP = "127.0.0.1";
            // Kullanici.LastLoginTime = DateTime.Now;
            // Kullanici.LastToken = "432823094329048093284";
            // db.User.Add(Kullanici);
            // db.SaveChanges();
            
            List<Mdl_User> UserList = new List<Mdl_User>();
            UserList = db.User.Where(x => x.Name == userModel.Name && x.Password == userModel.Password).ToList<Mdl_User>();
            
            if(UserList.Count>0)
            {
                return UserList[0];
            }   
            else
            {
               return new Mdl_User();
            }
            
        }
    }
}
