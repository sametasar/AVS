using Microsoft.EntityFrameworkCore;
using AVSGLOBAL.Class.Models;
using AVSGLOBAL.Class.Global;

namespace AVSGLOBAL.Class.Dal
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Mdl_User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlServer(Cls_Tools.DefaultConnectionString());
            //dotnet ef migrations add AVSCAT
            //dotnet ef database update            
        }
    }  
}