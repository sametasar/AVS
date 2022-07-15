using AVSGLOBAL.Models.GlobalModel;
using Microsoft.EntityFrameworkCore;

namespace AVSGLOBAL.Interface
{
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