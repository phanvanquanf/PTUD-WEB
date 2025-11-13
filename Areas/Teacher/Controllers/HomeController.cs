using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Utilities;
using Microsoft.AspNetCore.Mvc;
using aznews.Models;

namespace aznews.Areas.Teacher.Controllers
{


    [Area("Teacher")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
            Functions._MaNguoiDung = 0;
            Functions._TenNguoiDung = string.Empty;
            Functions._TenDangNhap = string.Empty;
            Functions._Email = string.Empty;
            Functions._Message = string.Empty;
            return RedirectToAction("Index", "Home");
        }
    }
}