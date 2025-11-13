using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using aznews.Areas.Admin.Models;
using aznews.Models;
using aznews.Utilities;

namespace aznews.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RegisterController : Controller
    {
        private readonly DataContext _context;
        public RegisterController(DataContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Index(AdminUser auser)
        {
            if (auser == null) return NotFound();
            var check = _context.AdminUsers?.Where(u => u.TenDangNhap == auser.TenDangNhap).FirstOrDefault();
            if (check != null)
            {
                Functions._Message = "Tên đăng nhập đã tồn tại";
                return RedirectToAction("Index", "Register");
            }

            // Kiểm tra số điện thoại
            var checkSoDienThoai = _context.AdminUsers?.FirstOrDefault(u => u.SoDienThoai == auser.SoDienThoai);
            if (checkSoDienThoai != null)
            {
                Functions._Message = "Số điện thoại đã tồn tại";
                return RedirectToAction("Index", "Register");
            }

            // Kiểm tra email
            var checkEmail = _context.AdminUsers?.FirstOrDefault(u => u.Email == auser.Email);
            if (checkEmail != null)
            {
                Functions._Message = "Email đã tồn tại";
                return RedirectToAction("Index", "Register");
            }

            auser.TrangThai = true;
            Functions._Message = string.Empty;
            auser.MatKhau = Functions.MD5Password(auser.MatKhau);
            _context.AdminUsers?.Add(auser);
            _context.SaveChanges();
            return RedirectToAction("Index", "Login");
        }
    }
}