using JWT.Class.Models.GlobalModels;
using JWT.Interface;
using Microsoft.EntityFrameworkCore;
using JWT.Class.GlobalClass;


namespace JWT.Class.Dal
{
    public enum Enm_Database_Type
    {
        SqlServer =1,

        SqlLite =2,

        Oracle =3,

        PostgreSql =4,

        MongoDb =5,

        MySql =6

    }
    public class DatabaseContext : DbContext, IDbContext,IDisposable
    {       
        private Enm_Database_Type DatabaseType;

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            ///Veritabanı tipleri arasında enum içinde dönüyorum! Hashcode u eşit olan databsae default databsedir!   appsetting.json da "SelectDatabseEngine" değişken ile istediğim zaman default database i değştirebilirim!
            foreach (Enm_Database_Type foo in Enum.GetValues(typeof(Enm_Database_Type)))
            {
                if(foo.GetHashCode().ToString() == Cls_Settings.SelectDatabseEngine)
                {
                    this.DatabaseType = foo;
                }
            }            
        }

        public DatabaseContext()
        {
            ///Veritabanı tipleri arasında enum içinde dönüyorum! Hashcode u eşit olan databsae default databsedir!   appsetting.json da "SelectDatabseEngine" değişken ile istediğim zaman default database i değştirebilirim!
            foreach (Enm_Database_Type foo in Enum.GetValues(typeof(Enm_Database_Type)))
            {
                if (foo.GetHashCode().ToString() == Cls_Settings.SelectDatabseEngine)
                {
                    this.DatabaseType = foo;
                }
            }
        }

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

            //dotnet ef migrations add AVSAPI
            //dotnet ef database update
            //dotnet ef migrations add InitialCreate --context DataContext
            //Add - Migration MyMigration - context DataContextName

            switch (DatabaseType)
            {
                case Enm_Database_Type.SqlServer:
                    optionsBuilder.UseSqlServer(Cls_Tools.DefaultSqlServerConnectionString());
                    break;

                case Enm_Database_Type.SqlLite:
                    optionsBuilder.UseSqlite(Cls_Tools.DefaultSqliteConnectionString());
                    break;

                case Enm_Database_Type.Oracle:
                    optionsBuilder.UseSqlServer(Cls_Tools.DefaultSqlServerConnectionString());
                    break;

                case Enm_Database_Type.PostgreSql:
                    optionsBuilder.UseSqlServer(Cls_Tools.DefaultSqlServerConnectionString());
                    break;

                case Enm_Database_Type.MongoDb:
                    optionsBuilder.UseSqlServer(Cls_Tools.DefaultSqlServerConnectionString());
                    break;

                case Enm_Database_Type.MySql:
                    optionsBuilder.UseSqlServer(Cls_Tools.DefaultSqlServerConnectionString());
                    break;

                default:
                    optionsBuilder.UseSqlite(Cls_Tools.DefaultSqliteConnectionString());
                    break;

            }
        }




        }
}
