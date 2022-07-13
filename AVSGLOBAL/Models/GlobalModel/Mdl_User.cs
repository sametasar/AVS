using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.ComponentModel;

namespace AVSGLOBAL.Models.GlobalModel
{
    /// <summary>
    /// Bu veri modeli Login işlemlerin de kullanılmak üzere tasarlanmıştır. Aynı zamanda veri tabanında User adlı tablonun modelidir.
    /// </summary>
    [Table("User")]
    public class Mdl_User : IDisposable
    {
        /// <summary>
        /// Login Kullanıcısının benzersiz ID numarası.
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// Kullanıcının adı
        /// </summary>
        [MaxLength(25,ErrorMessage="İsim en fazla 25 karakter olabilir!"), Required(ErrorMessage="Bir başlık girilmelidir")] 
        public string? Name { get; set; }

        /// <summary>
        /// Soyadı
        /// </summary>
        public string? Surname { get; set; }

        /// <summary>
        /// Şifresi , Şifreler SHA256 algoritmasına göre şifrelenerek KVKK Kişisel verilerin korunma kanununa uygun bir şekilde saklanacaktır.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Kullanıcı Eposta adresi
        /// </summary>
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Geçersiz posta adresi!")]
        public string? Email { get; set; }

          /// <summary>
        /// Telefon numarası
        /// </summary>
        [MaxLength(17, ErrorMessage = "Telefon no en fazla 17 karakter olabilir!")]
        public string? Phone { get; set; }

        /// <summary>
        /// Kullanıcı kayıtının oluşturulma tarihi.
        /// </summary>
        public DateTime? Create_Date { get; set; }

        /// <summary>
        /// Bu kayıtı oluşturan başka bir kullanıcı varsa onun ID değeridir.
        /// </summary>
        //[Required(ErrorMessage="Maximum oyuncu kapasitesini girmelisiniz") , Range(1,100,ErrorMessage="En az 5 en fazla 100 oyuncu olabilir")] 
        public int? UserID { get; set; }

        /// <summary>
        /// Son Token bilgisi ileriye dönük olarak loglamalarda kullanılabilir.
        /// </summary>
        public string? LastToken { get; set; }

        /// <summary>
        /// Web service tarafında oluşturulan token için kullanılır
        /// </summary>
        public string? LastServiceToken { get; set; }

        /// <summary>
        /// Login olan kullanıcının sisteme en son hangi ip adresinden giriş yaptığı bilgisi.
        /// </summary>
        public string LastIP { get; set; }

        /// <summary>
        /// Kullanıcının en son login olduğu tarih.
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// Role bazlı yetkilendirme, Microsoft identity nin basit yetkilendirmesini kullanacaksanız bu alan bize yardımcı olacaktır. Daha spesifik işlemler için kendi yetkilendirme sistemimizi kullanmalıyız!
        /// </summary>
        public string IdentityRole { get; set; }

         /// <summary>
        /// Login olan kullanıcının uygulamada kullandığı dil seçimi.
        /// </summary>
        /// <seealso cref="Mdl_Language.ID"/>
        [ForeignKey("Mdl_Language")]
        public int LanguageID { get; set; }

        /// <summary>
        /// Kullanıcı kaydının silinme bilgisi.
        /// </summary>
        [DefaultValue("false")]
        public bool Deleted { get; set; }

        public virtual Mdl_Language Mdl_Language { get; set; }

        /// <summary>
        /// Bir kullanıcı objesi oluşturuldukdan sonra kullanımı bittiğinde bellekte yer tüketmemesi için bu metot ile bellekden kaldırılır.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
           GC.SuppressFinalize(this);
        }
    }

}
