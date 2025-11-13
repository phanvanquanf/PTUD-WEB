using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Areas.Admin.Models;
using aznews.Models;
using aznews.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace aznews.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly DataContext _context;
        public LoginController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Index(AdminUser user)
        {
            if (user == null) return NotFound();
            string pw = Functions.MD5Password(user.MatKhau);
            var check = _context.AdminUsers?.Where(u => (u.TenDangNhap == user.TenDangNhap) && (u.MatKhau == pw)).FirstOrDefault();
            if (check == null)
            {
                Functions._Message = "Tên đăng nhập và mật khẩu không hợp lệ";
                return RedirectToAction("Index", "Login");
            }

            if (check.TrangThai == false) // Nếu TrangThai là false
            {
                Functions._Message = "Tài khoản của bạn chưa được kích hoạt";
                return View(user); // Hiển thị lại form đăng nhập với thông báo lỗi
            }

            if (check.VaiTro != user.VaiTro)
            {
                Functions._Message = "Vai trò được chọn không khớp với tài khoản.";
                return View(user); // Hiển thị lại form đăng nhập với thông báo lỗi
            }

            Functions._Message = string.Empty;
            Functions._MaNguoiDung = check.MaNguoiDung;
            Functions._TenDangNhap = string.IsNullOrEmpty(check.TenDangNhap) ? string.Empty : check.TenDangNhap;
            Functions._TenNguoiDung = string.IsNullOrEmpty(check.HoVaTen) ? string.Empty : check.HoVaTen;
            Functions._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;

            if (check.VaiTro == 0) // Vai trò 1: Quản trị viên
            {
                return RedirectToAction("Index", "", new { area = "Admin" });
            }
            else if (check.VaiTro == 1) // Vai trò 2: Giáo viên
            {
                return RedirectToAction("Index", "", new { area = "Teacher" });
            }
            else if (check.VaiTro == 2) // Vai trò 2: Người dùng
            {
                return RedirectToRoute("default", new { controller = "Home", action = "Index" }); ;
            }

            // Nếu vai trò không hợp lệ
            Functions._Message = "Vai trò không hợp lệ.";
            return View(user);
        }
    }
}