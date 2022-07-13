using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;

namespace AVSGLOBAL.Models.GlobalModel
{
    /// <summary>
    /// Dil listesinin bulunduğu veri modelidir.
    /// </summary>
    [Table("Language")]
    public class Mdl_Language : IDisposable
    {
        /// <summary>
        /// Benzersiz dil ID numarası.
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// Dil adı, Tüekçe, ingilizce,almanca vb. 
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Default tabloda sadece 1 tane olabilir bu yüzden tabloda bir update trigger yaptık her hangi bir kayıtda default değeri değiştiğinde diğer kayıtlarda ki default değerleri false edilir.
        /// Web sitesine kullanıcı yeni girdiğinde hiç bir seçimi yoksa bu durumda default devreye girer.
        /// </summary>
        public string? Default { get; set; }

        /// <summary>
        /// Dil seçimlerinde gösterilecek imaj genellikle bayrak resimleri olur.
        /// </summary>
        public string? Picture { get; set; }

        /// <summary>
        /// Kaydının silinme bilgisi.
        /// </summary>
        [DefaultValue("false")]
        public bool Deleted { get; set; }  

        /// <summary>
        /// Mdl_Language classından oluşan objeyi yok eder.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
    
    /// <summary>
    /// Tercüme edilecek kelimelerin bulunduğu sözlüktür.
    /// </summary>    
    [Table("LanguageDictionary")]
    public class Mdl_LanguageDictionary : IDisposable
    {
        /// <summary>
        /// Tercüme kelimesinin benzersiz ID değeri.
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// Tercüme dilecek kontrülün sayfa üzerinde tanımlanan ID değeri.
        /// </summary>
        public int ControlID { get; set; }  

        /// <summary>
        /// İlgili kontrole ait tercümenin hangi dilde yapılacağı.
        /// </summary>
        public int LanguageID { get; set; }

        /// <summary>
        /// Tercüme edilecek kelime
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// Kaydının silinme bilgisi.
        /// </summary>
        [DefaultValue("false")]
        public bool Deleted { get; set; } 

        /// <summary>
        /// Mdl_LanguageDictionary classından oluşan objeyi yok eder.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
