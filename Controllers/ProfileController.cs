using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using aznews.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using aznews.Components;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace aznews.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public ProfileController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var hocViens = _context.viewHocViens.ToList(); // Hoặc bất kỳ phương thức nào lấy dữ liệu
            return View(hocViens);
        }

        public IActionResult Edit(long? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var hocvien = _context.HocViens.Find(id);
            if (hocvien == null)
                return NotFound();

            var mnList = (from m in _context.Menus
                          select new SelectListItem()
                          {
                              Text = m.MenuName,
                              Value = m.MenuID.ToString(),
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "--- Select ---",
                Value = "0",
            });
            ViewBag.mnList = mnList;
            return View(hocvien);
        }

        [HttpPost]
        public IActionResult Edit(tblHocVien hv)
        {
            // Kiểm tra dữ liệu nhập vào có hợp lệ không
            if (ModelState.IsValid)
            {
                // Cập nhật giáo viên vào cơ sở dữ liệu
                _context.HocViens.Update(hv);
                _context.SaveChanges(); // Lưu vào cơ sở dữ liệu

                // Chuyển hướng về trang Index sau khi cập nhật thành công
                return RedirectToAction("HocVien");
            }

            // Nếu dữ liệu không hợp lệ, trả về View với Model hiện tại
            return View(hv);
        }


    }
}