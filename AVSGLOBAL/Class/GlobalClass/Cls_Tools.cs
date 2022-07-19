using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Data.Sqlite;

namespace AVSGLOBAL.Class.GlobalClass
{
    /// <summary>
    /// Proje genelinde çağrılan global metotlar bu bölümde oluşturulmuştur.
    /// </summary>
    public static class Cls_Tools
    {
        /* #region  VARIABLES */
        public static IWebHostEnvironment Hosting { get; set; }

        public static bool ConnectionStatus { get; set; }

        public static string RootDirectory { get; set; }
        /* #endregion */

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }


        public static string DefaultSqlServerConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.UserID = Cls_DefaultMsSql.UserName;           
            builder.Password = Cls_DefaultMsSql.Password;
            builder.InitialCatalog = Cls_DefaultMsSql.Database;
            builder.DataSource = Cls_DefaultMsSql.Server;
            builder.PersistSecurityInfo = true;
            builder.TrustServerCertificate = true;
            return builder.ConnectionString;
        }


        public static string DefaultMySqlConnectionString()
        {
            throw new NotImplementedException();

            //MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            //builder.UserID = Cls_DefaultMsSql.UserName;
            //builder.Password = Cls_DefaultMsSql.Password;
            //builder.Database = Cls_DefaultMsSql.Database;
            //builder.Server = Cls_DefaultMsSql.Server; 
            //return builder.ConnectionString;
        }


        public static string DefaultSqliteConnectionString()
        {
            SqliteConnectionStringBuilder d = new SqliteConnectionStringBuilder();
            d.DataSource = System.IO.Directory.GetCurrentDirectory() + Cls_DefaultSqlLite.Directory + Cls_DefaultSqlLite.DataSource;
            d.DefaultTimeout = Cls_DefaultSqlLite.DefaultTimeout;
            if (Cls_DefaultSqlLite.Password != null && Cls_DefaultSqlLite.Password != "")
            {
                d.Password = Cls_DefaultSqlLite.Password;
            }

            return d.ConnectionString;
        }

        #region Timestamp Zaman Damgaları

        /// <summary>
        /// Timestamp formatında bir double değeri datetime değere dönüştürür.
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime TimeStamp_To_DateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        /// <summary>
        /// Datetime tipinde bir değeri double tipinde timestamp formatında bir değere dönüştürür.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static double DateTime_To_Timestamp(DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        #endregion

        #region public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        /// <summary>
        /// Aes formatında şifreleme yapar, Şifre metodu ilk parametresinde aldığı string i byte a dönüştürür. ikinci değer ise şifredir oda byte a dönüşür ve şifreleme işlemi Aes algoritmasına göre yapılır.
        /// </summary>
        /// <param name="bytesToBeEncrypted"></param>
        /// <param name="passwordBytes"></param>
        /// <returns>byte[]</returns>
        /// <seealso cref="Cls_Password.Crypt(string, string)"/>
        public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }
        #endregion

        #region public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        /// <summary>
        /// Aes formatında şifre çözer, Çöz metodu ilk parametresinde aldığı string i byte a dönüştürür. ikinci değer ise şifredir oda byte a dönüşür ve şifreleme işlemi Aes algoritmasına göre yapılır.
        /// </summary>
        /// <param name="bytesToBeDecrypted"></param>
        /// <param name="passwordBytes"></param>
        /// <returns>byte[]</returns>
        /// <seealso cref="Cls_Password.Encrypt(string, string)"/>
        public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
        #endregion

        #region public static string EnCrypt(string Value, string Key)
        /// <summary>
        /// SMT algoritması ile şifreleme yapar İlk parametre şifrelenecek kelime, ikinci parametre ise şifrenin anahtarıdır.
        /// </summary>
        /// <param name="Value">Şifrelenecek Kelime</param>
        /// <param name="Key">Şifreleme Anahtarı</param>
        /// <returns>string</returns>
        /// <seealso cref="Cls_Password.AES_Encrypt(byte[], byte[])"/>
        public static string EnCrypt(string Value, string Key)
        {
            if (Value == null || Value == "")
            {
                return Value;
            }
            else
            {
                // Get the bytes of the string
                byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(Value);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(Key);
                // Hash the password with SHA256
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
                byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);
                string result = Convert.ToBase64String(bytesEncrypted);
                return result;
            }
        }
        #endregion

        #region public static string Decrypt(string Value, string Key)
        /// <summary>
        /// SMT algoritması ile şifreyi çözer.İlk parametre şifresi çözülecek kelime, ikinci parametre ise belirnen şifreyi çözecek keydir.
        /// </summary>
        /// <param name="Value">Şifresi çözülecek kelime</param>
        /// <param name="Key">Kelimenin şifeleme anahtarı</param>
        /// <returns>string</returns>
        /// <seealso cref="Cls_Password.AES_Decrypt(byte[], byte[])"/>
        public static string Decrypt(string Value, string Key)
        {
            if (Value == null)
            {
                return Value;
            }
            else
            {
                // Get the bytes of the string
                byte[] bytesToBeDecrypted = Convert.FromBase64String(Value);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(Key);
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
                byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);
                string result = Encoding.UTF8.GetString(bytesDecrypted);
                return result;
            }
        }
        #endregion

        #region public static string EnCrypt(string Value)
        /// <summary>
        /// SMT algoritması ile şifreleme yapar  şifrelenecek kelime yazılır ve uygulamanın "Cls_Settings.DefaultPasswordKey" bilgisi ile text şifrelenir.
        /// </summary>
        /// <param name="Value">Şifrelenecek Kelime</param>       
        /// <returns>string</returns>
        /// <seealso cref="Cls_Password.AES_Encrypt(byte[], byte[])"/>
        public static string EnCrypt(string Value)
        {
            if (Value == null || Value == "")
            {
                return Value;
            }
            else
            {
                // Get the bytes of the string
                byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(Value);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(Cls_Settings.DefaultPasswordKey);
                // Hash the password with SHA256
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
                byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);
                string result = Convert.ToBase64String(bytesEncrypted);
                return result;
            }
        }
        #endregion

        #region public static string Decrypt(string Value)
        /// <summary>
        /// SMT algoritması ile şifreyi çözer.şifresi çözülecek kelime yazılır ve sistemin standart şifre anahtarı olan "Cls_Settings.DefaultPasswordKey" bilgisi ile şifre çözülür!
        /// </summary>
        /// <param name="Value">Şifresi çözülecek kelime</param>        
        /// <returns>string</returns>
        /// <seealso cref="Cls_Password.AES_Decrypt(byte[], byte[])"/>
        public static string Decrypt(string Value)
        {
            if (Value == null)
            {
                return Value;
            }
            else
            {
                // Get the bytes of the string
                byte[] bytesToBeDecrypted = Convert.FromBase64String(Value);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(Cls_Settings.DefaultPasswordKey);
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
                byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);
                string result = Encoding.UTF8.GetString(bytesDecrypted);
                return result;
            }
        }
        #endregion
    }

}
