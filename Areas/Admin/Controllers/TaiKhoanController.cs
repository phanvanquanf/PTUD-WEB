using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Areas.Admin.Models;
using aznews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace aznews.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TaiKhoanController : Controller
    {
        private readonly DataContext _context;

        public TaiKhoanController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tkList = _context.AdminUsers.OrderBy(m => m.MaNguoiDung).ToList();
            return View(tkList);
        }

        [HttpGet("SearchTaiKhoan")]
        public IActionResult Index(string input)
        {
            var tkList = _context.AdminUsers.OrderBy(m => m.MaNguoiDung).ToList();

            var Search = _context.AdminUsers.Where(item => item.HoVaTen != null && item.HoVaTen.Contains(input)
                                              && item.TenDangNhap != null && item.TenDangNhap.Contains(input)).ToList();
            ViewData["TKInput"] = input;
            if (input != null)
            {
                return View(Search);
            }
            else
            {
                return View(tkList);
            }

        }

        public IActionResult SuaXoa()
        {
            var tkList = _context.AdminUsers.OrderBy(m => m.MaNguoiDung).ToList();
            return View(tkList);
        }

        [HttpGet("SearchTaiKhoan1")]
        public IActionResult SuaXoa(string input)
        {
            var tkList = _context.AdminUsers.OrderBy(m => m.MaNguoiDung).ToList();

            var Search = _context.AdminUsers.Where(item => item.HoVaTen != null && item.HoVaTen.Contains(input)
                                              && item.TenDangNhap != null && item.TenDangNhap.Contains(input)).ToList();
            ViewData["TKInput"] = input;
            if (input != null)
            {
                return View(Search);
            }
            else
            {
                return View(tkList);
            }
        }

        public static string MD5Hash(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text), "Input cannot be null or empty.");

            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(text);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Chuyển kết quả băm thành chuỗi hex
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static string MD5Password(string? text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            string str = MD5Hash(text);
            for (int i = 0; i <= 5; i++)
            {
                str = MD5Hash(str + str);
            }

            return str;
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var tk = _context.AdminUsers.Find(id);
            if (tk == null)
                return NotFound();
            return View(tk);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var delMenu = _context.AdminUsers.Find(id);
            if (delMenu == null)
                return NotFound();
            _context.AdminUsers.Remove(delMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AdminUser tk)
        {
            if (ModelState.IsValid)
            {
                // Băm mật khẩu trước khi lưu
                if (!string.IsNullOrEmpty(tk.MatKhau))
                {
                    tk.MatKhau = MD5Password(tk.MatKhau);
                }
                _context.AdminUsers.Add(tk);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tk);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var tk = _context.AdminUsers.Find(id);
            if (tk == null)
                return NotFound();
            return View(tk);
        }

        [HttpPost]
        public IActionResult Edit(AdminUser tk)
        {
            if (ModelState.IsValid)
            {   // Băm mật khẩu trước khi lưu
                if (!string.IsNullOrEmpty(tk.MatKhau))
                {
                    tk.MatKhau = MD5Password(tk.MatKhau);
                }
                _context.AdminUsers.Update(tk);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tk);
        }
    }
}