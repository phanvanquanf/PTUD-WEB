using elFinder.NetCore;
using elFinder.NetCore.Drivers.FileSystem;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace aznews.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class FileManagerController : Controller
    {
        [Route("/Teacher/file-manager")]
        public IActionResult Index()
        {
            return View();
        }

        private readonly IWebHostEnvironment _env;
        public FileManagerController(IWebHostEnvironment env) => _env = env;

        // URL để client-side kết nối đến backend
        [Route("Teacher/connector")]
        public async Task<IActionResult> Connector()
        {
            var connector = GetConnector();
            return await connector.ProcessAsync(Request);
        }

        // Địa chỉ để truy vấn thumbnail
        [Route("Teacher/thumb/{hash}")]
        public async Task<IActionResult> Thumbs(string hash)
        {
            var connector = GetConnector();
            return await connector.GetThumbnailAsync(HttpContext.Request, HttpContext.Response, hash);
        }

        private Connector GetConnector()
        {
            var driver = new FileSystemDriver();

            // Đường dẫn gốc tuyệt đối cho ứng dụng
            string absoluteUrl = UriHelper.BuildAbsolute(Request.Scheme, Request.Host);
            var uri = new Uri(absoluteUrl);

            // Cấu hình thư mục `assets/img`
            string assetsPath = Path.Combine("wwwroot", "assets", "img");  // Đảm bảo sử dụng đúng dấu nháy kép và cách sắp xếp
            string assetsRequestUrl = "assets/img"; // Đây là đường dẫn tương ứng sẽ được sử dụng trong URL
            string assetsRootDirectory = Path.Combine(_env.ContentRootPath, assetsPath); // Cấu hình đường dẫn tuyệt đối

            // Tạo đối tượng RootVolume cho thư mục assets/img
            var assetsRoot = new RootVolume(assetsRootDirectory, $"/{assetsRequestUrl}/", $"{uri.Scheme}://{uri.Authority}/Admin/thumb/")
            {
                Alias = "Assets",  // Tên hiển thị trong elFinder
                IsReadOnly = false,  // Cho phép chỉnh sửa
                IsLocked = false,    // Cho phép thao tác với tệp
                ThumbnailSize = 100  // Kích thước thu nhỏ của ảnh
            };

            // Thêm thư mục gốc vào driver
            driver.AddRoot(assetsRoot);

            return new Connector(driver)
            {
                MimeDetect = MimeDetectOption.Internal  // Phát hiện MIME kiểu tệp
            };
        }
    }
}
