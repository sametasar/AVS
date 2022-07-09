using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace JWT.Class.Models.GlobalModels
{

    /// <summary>
    /// Web servisden gelen gava deerleri tahmini 2 objeden oluşmakta bu parent obje, bunun içinde Mdl_Weather tipinde değerlerin olduğu objeye değerler gelmektedir.
    /// </summary>
    public class Mdl_WeatherObject
    {
        public bool success { get; set; }

        public string city { get; set; }

        public List<Mdl_Weather> result = new List<Mdl_Weather>();

        public Mdl_WeatherObject GetWeather(string city)
        {
            Mdl_WeatherObject response = new Mdl_WeatherObject();

            if (city == null || city == "")
            {
                city = "istanbul";
            }
            else
            {
                city = city.ToLower();
            }

            var request = (HttpWebRequest)WebRequest.Create("https://api.collectapi.com/weather/getWeather?data.lang=tr&data.city=" + city + "");
            request.Headers.Add("authorization", "apikey 7139dASyxj33KFuTNipdt6:4kGc3vzOvbA1BjbTAe8OrQ");
            request.Headers.Add("content-type", "application/json");
            request.Method = "GET";

            var result = (HttpWebResponse)request.GetResponse();

            string resultString = new StreamReader(result.GetResponseStream()).ReadToEnd();

            response = JsonConvert.DeserializeObject<Mdl_WeatherObject>(resultString);

            return response;

        }

    }


        /// <summary>
        /// Hava durumuna ait bilgileri verir.
        /// </summary>
        public class Mdl_Weather
    {                     

        /// <summary>
        /// "24.09.2018" örnekde olduğu gibi tarih bilgisi verir.
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// "Pazartesi" Örnekde olduğu gibi gün bilgisi verir.
        /// </summary>
        public string day { get; set; }

        /// <summary>
        /// "https://image.flaticon.com/icons/svg/143/143769.svg" Örnekde görüldüğü gibi havadurumuna ait görsel ikon verisi url bilgisini verir.
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// "açık" örneğinde olduğu gibi bir bilgi verir.
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// "Clear" Örnekde olduğu gibi havanın açık kapalı bilgisiniverir.
        /// </summary>
        public string status { get; set; }

        /// <summary>
        ///  "31" deki gibi derece cinsinden havanın sıcaklığını verir.
        /// </summary>
        public string degree { get; set; }

        /// <summary>
        /// Bu gün en düşük sıcaklığı derece cinsinden verir.
        /// </summary>
        public string min { get; set; }

        /// <summary>
        /// Bu gün en yüksek sıcaklığı derece cinsinden verir.
        /// </summary>
        public string max { get; set; }

        /// <summary>
        /// Bu akşam  ortalama sıcaklığı derece cinsinden verir.
        /// </summary>
        public string night { get; set; }

        /// <summary>
        /// "17" = %17  Nem bilgisini verir. Bilgi Yüzde şeklinde okunur.
        /// </summary>
        public string humidity { get; set; }
       
    }
}
