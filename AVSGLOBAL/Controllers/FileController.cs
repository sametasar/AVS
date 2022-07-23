using Microsoft.AspNetCore.Mvc;
using AVSGLOBAL.Interface;
using AVSGLOBAL.Class.GlobalClass;
using Microsoft.AspNetCore.Authorization;

namespace AVSGLOBAL.Controllers
{
    /// <summary>
    /// Dosyaları sunuculara yüklemek için oluşturulan class.
    /// </summary>
    public class FileController : Controller
    { 

            private readonly ILogger<FileController> _logger;   

            public FileController(ILogger<FileController> logger)
            {
                _logger = logger;             
            }

            public void OnGet()
            {

            }

        [Authorize]       
        [HttpPost]
        [Route("fileupload")]   
        /// <summary>
        /// Dosyaları sunucuya yüklemek için kullanılır.
        /// </summary> 
        public async Task<JsonResult> OnPostAsync(IFormFile file)
        {
            //Dosyamızın kaydedileceği Klasörün yolunu belirliyoruz.
            var folderPath = Path.Combine(Cls_Tools.Hosting.WebRootPath, "Upload","Files");
            //Klasörün var olup olmadığı kontrol ediyoruz yoksa eğer yeni bir tane oluştursun.
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            //Dosya yolumuz
            var filePath = Path.Combine(folderPath, file.FileName);
            if (file.Length > 0)
            {
                //dosyamızı kaydediyoruz.
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return Json("{ID:5}"); //Yukarıdaki kodu işlersin geriye json tipinde model dönebilirsin!
        }
    }
}
