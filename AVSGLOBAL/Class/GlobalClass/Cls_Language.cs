namespace AVSGLOBAL.Class.GlobalClass
{
    /* #region  LANGUAGE */
    /// <summary>
    /// Uygulamanın çoklu dil desteği için geliştirilmiştir.
    /// </summary>
    public class Cls_Language
    {
        /* #region GetLanguageDictionary  */
        /// <summary>
        /// Çoklu dil için geliştirildi. Bu uygulamada kullanılan tüm sözlük bilgisini döndürür.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetLanguageDictionary()
        {
            Cls_WebRequestOption options = new Cls_WebRequestOption();
            options.ActionType = Enm_ActionTypes.Post;
            options.ApiUrl = "/language/";
            options.MethodName = "GetLanguageDictionary";
            options.Url = Cls_Settings.MAIN_WEB_SERVICE;
            string json = await Cls_WebRequest.SendRequest(options);
            return json;
        }
        /* #endregion */

    }
    /* #endregion */
}