using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;

namespace AVSGLOBAL.Class.Global
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

        public static string DefaultConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.UserID = "sa";
            // builder.Password = "test";
            // builder.InitialCatalog = "AVSCAT";
            // builder.DataSource = ".";
            builder.Password = "test";
            builder.InitialCatalog = "AVSCATERING";
            builder.DataSource = ".";
            builder.PersistSecurityInfo = true;
            builder.TrustServerCertificate = true;


            //SqlConnection baglan = new SqlConnection(builder.ConnectionString);

            //baglan.Open();
            //ConnectionStatus = true;
            //baglan.Close();

            return builder.ConnectionString;
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

    }

}
