using Microsoft.AspNetCore.Mvc;
using aznews.Models; // Namespace chứa viewHocVien và các Models
using System;
using System.Linq;

namespace aznews.Controllers
{
    public class HocVienController : Controller
    {
        private readonly DataContext _context;

        public HocVienController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult UpdateProfile([FromBody] viewHocVien hocVien)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
            }

            try
            {
                // Lấy học viên hiện tại từ cơ sở dữ liệu
                var currentHocVien = _context.HocViens.FirstOrDefault(h => h.IDHocVien == hocVien.IDHocVien);
                if (currentHocVien == null)
                {
                    return Json(new { success = false, message = "Học viên không tồn tại." });
                }

                // Cập nhật thông tin
                currentHocVien.HoTenHV = hocVien.HoTenHV;
                currentHocVien.NgaySinh = hocVien.NgaySinh;
                currentHocVien.GioiTinh = hocVien.GioiTinh;
                currentHocVien.SoDienThoai = hocVien.SoDienThoai;
                currentHocVien.Email = hocVien.Email;
                currentHocVien.DiaChi = hocVien.DiaChi;

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();

                return Json(new { success = true, message = "Cập nhật thông tin thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi: {ex.Message}" });
            }
        }
    }
}
