using JWT.Class.Dal;
using JWT.Class.Models.GlobalModels;
using Microsoft.EntityFrameworkCore;

namespace JWT.Interface
{
    /// <summary>
    /// Middleware da authentication işlemlerini yönetecek olan classın implement edeceği interface yapısı.
    /// </summary>
    public interface IDbContext
    {
        #region GLOBAL

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

        #endregion        

    }
}
