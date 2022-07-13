using Microsoft.EntityFrameworkCore;
using AVSGLOBAL.Models.GlobalModel;
using AVSGLOBAL.Class.GlobalClass;

namespace AVSGLOBAL.Class.Dal
{
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Sistemelogin olan kullanıcılarıları temsil eder.
        /// </summary>
        public DbSet<Mdl_User> User { get; set; }

        /// <summary>
        /// Dil listesini temsil eder.
        /// </summary>
        public DbSet<Mdl_Language> Language { get; set; }
       
        /// <summary>
        /// Dillere ait sözlük veritabanını temsil eder.
        /// </summary>
        public DbSet<Mdl_LanguageDictionary> LanguageDictionary { get; set; }

        /// <summary>
        /// Entity freamwork code first yaklaşımının confügürasyon ayarlarını gerçekleştirir.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Cls_Tools.DefaultConnectionString());
            //dotnet ef migrations add AVSCAT
            //dotnet ef database update            
        }
    }
}