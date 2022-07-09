using JWT.Class.GlobalClass;
using JWT.Class.Models.GlobalModels;
using Microsoft.EntityFrameworkCore;

namespace JWT.Class.Dal
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Mdl_User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Cls_Tools.DefaultConnectionString());
            //dotnet ef migrations add AVSCAT
            //dotnet ef database update            
        }
    }
}
