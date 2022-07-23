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
        [Route("fileuploadtext")]   
        /// <summary>
        /// Dosyaları sunucuya yüklemek için kullanılır.Aynı zamanda dosya bir text dosyası ise içeriğini satır satır okuyarak geriye her satırı bir array olarak döndürür.
        /// </summary> 
        public async Task<string[]> OnPostTextStringAsync(IFormFile file)
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

            return System.IO.File.ReadAllText(folderPath + "\\" + file.FileName).Split('\n');
        }

        
        [Authorize]       
        [HttpPost]
        [Route("fileuploadjson")]   
        /// <summary>
        /// Dosyaları sunucuya yüklemek için kullanılır.Aynı zamanda dosya bir text dosyası ise içeriğini satır satır okuyarak geriye json döndürür.
        /// </summary> 
        public async Task<JsonResult> OnPostTextJsonAsync(IFormFile file)
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

            return Json(System.IO.File.ReadAllText(folderPath + "\\" + file.FileName).Split('\n'));
        }

        
        [Authorize]       
        [HttpPost]
        [Route("fileuploadimage")]   
        /// <summary>
        /// Dosyaları sunucuya yüklemek için kullanılır.Aynı zamanda dosya bir imaj dosyası ise ve dosya adında bir değişiklik yapılmak istenirse işlem bittiğinde geriye dosya adını döndürür.
        /// </summary> 
        public async Task<string> OnPostImageAsync(IFormFile file)
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

            return file.Name;
        }

        
        [Authorize]       
        [HttpPost]
        [Route("fileuploadexcel")]   
        /// <summary>
        /// Dosyaları sunucuya yüklemek için kullanılır.Aynı zamanda dosya bir excel dosyası ise içeriğini satır satır okuyarak geriye tüm satırları bir json olarak döndürür.
        /// </summary> 
        public async Task<JsonResult> OnPostTextExcelAsync(IFormFile file)
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
            //todo buradaki geri dönüş yüklenen excel dosyasını satır satır okuyup geriye json olarak dönmesi şeklinde olmalıdır.
            return Json(System.IO.File.ReadAllText(folderPath + "\\" + file.FileName).Split('\n'));
        }
    }
}
