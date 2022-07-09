using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWT.Class.Models.GlobalModels
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
        public string? Email { get; set; }


        /// <summary>
        /// Kullanıcı kayıtının oluşturulma tarihi.
        /// </summary>
        public DateTime? Create_Date { get; set; }

        /// <summary>
        /// Bu kayıtı oluşturan başka bir kullanıcı varsa onun ID değeridir.
        /// </summary>
        public int? UserID { get; set; }

        /// <summary>
        /// Son Token bilgisi ileriye dönük olarak loglamalarda kullanılabilir.
        /// </summary>
        public string? LastToken { get; set; }

        /// <summary>
        /// Login olan kullanıcının sisteme en son hangi ip adresinden giriş yaptığı bilgisi.
        /// </summary>
        public string LastIP { get; set; }

        /// <summary>
        /// Kullanıcının en son login olduğu tarih.
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// Kullanıcı kaydının silinme bilgisi.
        /// </summary>
        public bool? Deleted { get; set; }

        /// <summary>
        /// Bir kullanıcı objesi oluşturuldukdan sonra kullanımı bittiğinde bellekte yer tüketmemesi için bu metot ile bellekden kaldırılır.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

}
