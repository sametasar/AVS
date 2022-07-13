using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Http.Headers;
using System.Dynamic;
using AVSGLOBAL.Models.GlobalModel;


namespace AVSGLOBAL.Class.GlobalClass
{


/// <summary>
/// Token kullanılan requestlerde atılacak requestde hangi token kullanılacak seçimi bu enumlarla yapılır.
/// </summary>
public enum Enm_TokenTypes{

    Jwt = 0,

    MicrosoftIdentity = 1,

    OpenAuth = 2

}  

/// <summary>
/// Atılacak requesting aksiyon tipini belirtmek için kullanılır, hangi yöntem post yada get tarzı seçim yapılmalıdır. 
/// </summary>
public enum Enm_ActionTypes{

    Post = 0,

    Get = 1,

    Put = 2,

    Delete =3

} 

public class Mdl_KeyValue{
    public string Key{get; set;}

    public string Value{get; set;}
}



    public class Cls_WebRequestOption{

    /// <summary>
    /// Bu Url değeri eğer boş bırakılırsa default olarak değeri "Cls_Settings.MAIN_WEB_SERVICE" den alır.
    /// </summary>
    /// <value></value>
    public string Url{get; set;}

    /// <summary>
    /// Url in ham kısmından sonra geriye kalan url yolu burada tanımlanır. Api yada Controller isimleri gibi. Sonuna dikkat edersen "/" işareti ile biter. Örnek olarak /Api/User/.
    /// </summary>
    /// <value></value>
    public string ApiUrl{get; set;}


    /// <summary>
    /// Web servislerde yada Web apilerde controllar içindeki metoda karşılık gelir. Sonuna dikkat edersen "/" işareti ile bitmez  /Api/User/GetUser
    /// </summary>
    /// <value></value>
    public string MethodName{get; set;}

    
    /// <summary>
    /// Web apide erişilecek metot eğer parametrelere sahipse bu durumda parametreler burada yazılır. Parametreler Liste tipinde keyvalu şeklinde yazılır
    /// </summary>
    /// <value></value>
    public List<Mdl_KeyValue> Parameters{get; set;}


    /// <summary>
    /// Atılacak requesting aksiyon tipini belirtmek için kullanılır, hangi yöntem post yada get tarzı seçim yapılmalıdır. 
    /// </summary>
    /// <value></value>
    public Enm_ActionTypes ActionType{get; set;}


    /// <summary>
    /// Atılacak requestde token değeri varsa buradan set edilebilir.
    /// </summary>
    /// <value></value>
    public string Token{get; set;}


    /// <summary>
    /// Token kullanılıyorsa bu durumda hangi token türü kullanılıyor buradan seçilebilir.Bu seçim yapılmazsa Token varsa default olarak JWT aktif olur. Token yoksa Default olarak Null olur ve kullanılmaz!
    /// </summary>
    /// <value></value>
    public Enm_TokenTypes TokenType{get; set;}


    public string GetUrl()
    {
        string Urlx = Url+ApiUrl+MethodName;

        for(int i=0; i<Parameters.Count; i++)
        {
            if(i==0)
            {
                Urlx+="?"+Parameters[i].Key+"="+Parameters[i].Value;
            }
            else
            {
                Urlx+="&"+Parameters[i].Key+"="+Parameters[i].Value;
            }
        }
        return Urlx;
    }
}


/// <summary>
/// Bu class daha kolay web requestlerin yönetilmesi için geliştirilmiştir.
/// </summary>
public static class Cls_WebRequest
{   
    
/// <summary>
/// Web servislere , apilere yada web sitelerine request atmak için kullanılır.
/// </summary>
/// <param name="options">Atılacak request hangi url, hangi controller hangi parametreler, hangi yöntem kullanılacak tarzda bilgiler barındırır.</param>
/// <returns></returns>
public static async Task<string> SendRequest(Cls_WebRequestOption options)
{
    string Response = "";

    Mdl_User User = new Mdl_User();
            
            string URI = Cls_Settings.MAIN_WEB_SERVICE;

            if(options.Url!=null)
            {
                URI = options.Url;
            }
            
            string myParameters = "";

            if(options.Parameters.Count>0) //Get Url oluştururken post tipinde başka bir şey yaparız!
            {
                for(int i=0; i<options.Parameters.Count; i++)
                {
                    if(i==0)
                    {
                        myParameters+="?"+options.Parameters[i].Key+"="+options.Parameters[i].Value;
                    }
                    else
                    {
                        myParameters+="&"+options.Parameters[i].Key+"="+options.Parameters[i].Value;
                    }
                }
            }

            string responseObj = string.Empty;  
            // HTTP GET.  
            using (var client = new HttpClient())  
            { 
            
                options.TokenType = Enm_TokenTypes.Jwt; //Gelecekde burada Token Tipine göre ek kodlar yazabilirsin. Şu an "JWT Token sadece kullanılmakta."  Microsoft.Identit, Yada OAuth 2 de kullanılabilir.!


            switch(options.ActionType)
            {
                case Enm_ActionTypes.Get:
                    if(options.Token==null)
                    {
                        Response =await GetRequest(options.ApiUrl+options.MethodName+myParameters); 
                    }
                    else
                    {
                        Response =await GetRequest(options.ApiUrl+options.MethodName+myParameters,options.Token);
                    }
                    
                break;

                case Enm_ActionTypes.Post: 
                    if(options.Token==null)
                    {
                        Response = await PostRequest(options.ApiUrl+options.MethodName,options.Parameters); 
                    }
                    else
                    {
                        Response = await PostRequest(options.ApiUrl+options.MethodName+myParameters,options.Parameters,options.Token);
                    }
                break;
            }            
            
            }

            return Response;
}


    /// <summary>
    /// Get aksiyon yöntemiyle verilen url e request atar. Geriye json tipinde string döndürür.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static async Task<string> GetRequest(string url)
    {
        HttpClient client = new HttpClient();
        var json ="";           
                    
                    client.BaseAddress = new Uri(Cls_Settings.MAIN_WEB_SERVICE);                 
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                  
                    HttpResponseMessage response = new HttpResponseMessage();                 
                    response = await client.GetAsync(url).ConfigureAwait(false);  
                    // Verification  
                    if (response.IsSuccessStatusCode)  
                    {                              
                            json = await response.Content.ReadAsStringAsync();
                    }
                    client.Dispose();
        return json;  
    }


       /// <summary>
    /// Get aksiyon yöntemiyle verilen url e request atar. Geriye json tipinde string döndürür.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static async Task<string> GetRequest(string url,string token)
    {
         HttpClient client = new HttpClient();
        var json ="";
                    
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); 
                    client.BaseAddress = new Uri(Cls_Settings.MAIN_WEB_SERVICE);                 
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                  
                    HttpResponseMessage response = new HttpResponseMessage();                 
                    response = await client.GetAsync(url).ConfigureAwait(false);  
                    // Verification  
                    if (response.IsSuccessStatusCode)  
                    {                              
                            json = await response.Content.ReadAsStringAsync();
                    }
                    client.Dispose();
        return json;  
    }


 /// <summary>
 /// Post aksiyon yöntemiyle verilen url e request atar. Geriye json tipinde string döndürür.
 /// </summary>
 /// <param name="url"></param>
 /// <returns></returns>
 public static async Task<string> PostRequest(string url,  List<Mdl_KeyValue> Parameters)
    {
         HttpClient client = new HttpClient();
                    var json = JsonConvert.SerializeObject(Parameters);                     
                    client.BaseAddress = new Uri(Cls_Settings.MAIN_WEB_SERVICE);                 
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                  
                                        
                    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); 
                    
                    HttpResponseMessage response = new HttpResponseMessage();                 
                    response = await client.PostAsync(url,stringContent).ConfigureAwait(false);  
                    // Verification  
                    if (response.IsSuccessStatusCode)  
                    {                              
                            json = await response.Content.ReadAsStringAsync();
                    }
                    client.Dispose();
        return json;  
    }




    
 /// <summary>
 /// Post aksiyon yöntemiyle verilen url e request atar. Geriye json tipinde string döndürür. Tokenli versiyonudur.
 /// </summary>
 /// <param name="url"></param>
 /// <returns></returns>
 public static async Task<string> PostRequest(string url,  List<Mdl_KeyValue> Parameters, string token)
    {
                    HttpClient client = new HttpClient();
                    var json = JsonConvert.SerializeObject(Parameters);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);                     
                    client.BaseAddress = new Uri(Cls_Settings.MAIN_WEB_SERVICE);                 
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                  
                                        
                    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); 
                    
                    HttpResponseMessage response = new HttpResponseMessage();                 
                    response = await client.PostAsync(url,stringContent).ConfigureAwait(false);  
                    // Verification  
                    if (response.IsSuccessStatusCode)  
                    {                              
                            json = await response.Content.ReadAsStringAsync();
                    }
                    client.Dispose();
        return json;  

    }


















    
    
    // /// <summary>
    // /// Bu metot başka web servislerine request atmak için oluşturulmuştur.
    // /// </summary>
    // /// <param name="method">Web adresindeki hangi metot olduğu bilgisi. Örnek method  : Get_User</param>
    // /// <param name="page">Web adresindeki controllar a karşılık gelen api adı. Örnek Page : /api/personel/</param>
    // /// <returns></returns>
    // public static async Task<string> RequestGet(string method,string page)
    // {
    //     serviceUrl = $"{Cls_Settings.MAIN_WEB_SERVICE}{page}{method}";
    //     using (HttpResponseMessage response = await client.GetAsync(serviceUrl))
    //         return await response.Content.ReadAsStringAsync();
    // }

    
    // /// <summary>
    // /// Bu metot başka web servislerine request atmak için oluşturulmuştur.
    // /// </summary>
    // /// <param name="method">Web adresindeki hangi metot olduğu bilgisi. Örnek method  : Get_User</param>
    // /// <param name="page">Web adresindeki controllar a karşılık gelen api adı. Örnek Page : /api/personel/</param>
    // /// <param name="parameter">Çalıştırılacak web api metodunada parametre bilgisi varsa onlar burada belirtilir. Örnek Parameter :?ID=2&Age=18</param>
    // /// <returns></returns>
    // public static async Task<string> RequestGet(string method,string page, string parameter)
    // { 
    //     serviceUrl = $"{Cls_Settings.MAIN_WEB_SERVICE}{page}{method}{parameter}";
    //     using (HttpResponseMessage response = await client.GetAsync(serviceUrl))
    //         return await response.Content.ReadAsStringAsync();
    // }



    // public static async Task<string> GetSingle(string method, int id)
    // {
    //     serviceUrl = $"{Cls_Settings.MAIN_WEB_SERVICE}/api/personel/{method}/{id}";
    //     using (HttpResponseMessage response = await client.GetAsync(serviceUrl))
    //         return await response.Content.ReadAsStringAsync();
    // }
    // public static async Task<string> Post<T>(string method, T instance) where T : class, new()
    // {
    //     serviceUrl = $"{Cls_Settings.MAIN_WEB_SERVICE}/api/personel/{method}";
    //     StringContent httpContent = new StringContent(JsonConvert.SerializeObject(instance), Encoding.UTF8, "application/json");
    //     using (HttpResponseMessage response = await client.PostAsync(serviceUrl, httpContent))
    //     {
    //         response.EnsureSuccessStatusCode();
    //         return await response.Content.ReadAsStringAsync();
    //     }
    // }
    // public static async Task<string> Put<T>(string method, T instance) where T : class, new()
    // {
    //     serviceUrl = $"{Cls_Settings.MAIN_WEB_SERVICE}/api/personel/{method}";
    //     StringContent httpContent = new StringContent(JsonConvert.SerializeObject(instance), Encoding.UTF8, "application/json");
    //     using (HttpResponseMessage response = await client.PutAsync(serviceUrl, httpContent))
    //     {
    //         response.EnsureSuccessStatusCode();
    //         return await response.Content.ReadAsStringAsync();
    //     }
    // }
    // public static async Task<string> Delete(string method, int id)
    // {
    //     serviceUrl = $"{Cls_Settings.MAIN_WEB_SERVICE}/api/personel/{method}/{id}";
    //     using (HttpResponseMessage response = await client.DeleteAsync(serviceUrl))
    //         return await response.Content.ReadAsStringAsync();
    // }
}

}