using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using aznews.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using aznews.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using aznews.Utilities;
using aznews.Areas.Admin.Models;

namespace aznews.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login", new { area = "Admin" });
            return View();
        }

        [Route("/post-{slug}-{id:long}.vn", Name = "Details")]
        public IActionResult Details(long? id)
        {
            if (id == null) return NotFound();
            var post = _context.viewPostMenus.FirstOrDefault(m => m.PostID == id && m.IsActive == true);
            if (post == null) return NotFound();
            return View(post);
        }

        [Route("/diendan-{slug}-{id:long}.vn", Name = "TrangDienDan")]
        public IActionResult TrangDienDan(long? id)
        {
            var danhSachDienDan = _context.viewDienDans
                                .Where(m => m.IDBaiViet == id && m.HoatDong == true)
                                .ToList();
            if (danhSachDienDan == null) return NotFound();
            return View(danhSachDienDan);
        }

        [Route("/giaovien-{slug}-{id:long}.vn", Name = "GiaoVien")]

        public IActionResult GiaoVien(long? id)
        {
            var GiaoVien = _context.viewGiangViens
                                .Where(m => m.GvienID == id && m.HoatDong == true)
                                .ToList();
            return View(GiaoVien);
        }

        [Route("/baigiang-{slug}-{id:long}.vn", Name = "BaiGiang")]

        public IActionResult BaiGiang(long? id)
        {
            var BaiGiang = _context.viewBaiGiangs
                                .Where(m => m.IDKhoaHoc == id && m.HoatDong == true)
                                .ToList();
            return View(BaiGiang);
        }


        [Route("/baitap-{slug}-{id:long}.vn", Name = "BaiTap")]

        public IActionResult LamBaiTap(long? id)
        {
            var LamBaiTap = _context.viewBaiTaps
                                .Where(m => m.IDBaiTap == id)
                                .ToList();
            return View(LamBaiTap);
        }

        [HttpPost]
        public IActionResult EditHV(tblHocVien hv)
        {
            if (ModelState.IsValid)
            {
                _context.HocViens.Update(hv);
                _context.SaveChanges();

                return RedirectToAction("HocVien");
            }

            return View(hv);
        }


        [Route("/menu-{slug}-{id:long}.vn", Name = "Menu")]
        public IActionResult Menu(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id == 1)
            {
                return View("Index");
            }

            if (id == 2)
            {
                var menu = _context.Menus.FirstOrDefault(m => m.MenuID == id && m.IsActive == true);

                if (menu != null)
                {
                    var khoahoc = _context.viewKhoaHocs
                    .Where(m => m.MenuID == 2 && m.HoatDong == true)
                    .OrderBy(m => m.IDKhoaHoc)
                    .ToList();

                    if (khoahoc != null)
                    {
                        return View("TrangKhoaHoc", khoahoc);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }

            if (id == 3)
            {
                return View("GioiThieu");
            }

            if (id == 4)
            {
                var menu = _context.Menus.FirstOrDefault(m => m.MenuID == id && m.IsActive == true);

                if (menu != null)
                {
                    var giaovien = _context.viewGiangViens
                    .Where(m => m.MenuID == 4 && m.HoatDong == true)
                    .OrderBy(m => m.GvienID)
                    .ToList();

                    if (giaovien != null)
                    {
                        return View("TrangGiaoVien", giaovien);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            if (id == 5)
            {
                var menu = _context.Menus.FirstOrDefault(m => m.MenuID == id && m.IsActive == true);

                if (menu != null)
                {
                    var hocvien = _context.viewHocViens
                    .Where(m => m.MenuID == 5 && m.TrangThai == 0)
                    .OrderBy(m => m.IDHocVien)
                    .ToList();

                    if (hocvien != null)
                    {
                        return View("~/Views/Profile/Index.cshtml", hocvien);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }

            if (id == 6)
            {
                var menu = _context.Menus.FirstOrDefault(m => m.MenuID == id && m.IsActive == true);

                if (menu != null)
                {
                    var diendan = _context.viewDienDans
                    .Where(m => m.MenuID == 6 && m.HoatDong == true)
                    .OrderBy(m => m.IDBaiViet)
                    .ToList();

                    if (diendan != null)
                    {
                        return View("DienDan", diendan);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }

            if (id == 7)
            {
                return View("LienHe");
            }

            if (id == 8)
            {
                var menu = _context.Menus.FirstOrDefault(m => m.MenuID == id && m.IsActive == true);

                if (menu != null)
                {
                    var khoahoc = _context.viewKhoaHocs
                    .Where(m => m.MenuID == 2 && m.HoatDong == true)
                    .OrderBy(m => m.IDKhoaHoc)
                    .ToList();

                    if (khoahoc != null)
                    {
                        return View("KhoaHoc10", khoahoc);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }

            if (id == 9)
            {
                var menu = _context.Menus.FirstOrDefault(m => m.MenuID == id && m.IsActive == true);

                if (menu != null)
                {
                    var khoahoc = _context.viewKhoaHocs
                    .Where(m => m.MenuID == 2 && m.HoatDong == true)
                    .OrderBy(m => m.IDKhoaHoc)
                    .ToList();

                    if (khoahoc != null)
                    {
                        return View("KhoaHoc11", khoahoc);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }

            if (id == 10)
            {
                var menu = _context.Menus.FirstOrDefault(m => m.MenuID == id && m.IsActive == true);

                if (menu != null)
                {
                    var khoahoc = _context.viewKhoaHocs
                    .Where(m => m.MenuID == 2 && m.HoatDong == true)
                    .OrderBy(m => m.IDKhoaHoc)
                    .ToList();

                    if (khoahoc != null)
                    {
                        return View("KhoaHoc12", khoahoc);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }

            if (id == 11)
            {
                var menu = _context.Menus.FirstOrDefault(m => m.MenuID == id && m.IsActive == true);

                if (menu != null)
                {
                    var baitap = _context.viewBaiTaps
                        .Where(m => m.MenuID == 11)
                        .OrderBy(m => m.IDBaiTap)
                        .ToList();
                    if (baitap.Any())
                    {
                        return View("BaiTap", baitap);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }

            if (id == 12)
            {
                var menu = _context.Menus.FirstOrDefault(m => m.MenuID == id && m.IsActive == true);

                if (menu != null)
                {
                    var tientrinh = _context.viewTienTrinhs
                        .Where(m => m.MenuID == 12)
                        .OrderBy(m => m.IDTienTrinh)
                        .ToList();

                    if (tientrinh.Any())
                    {
                        return View("TienTrinh", tientrinh);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }

            if (id == 13)
            {
                var menu = _context.Menus.FirstOrDefault(m => m.MenuID == id && m.IsActive == true);

                if (menu != null)
                {
                    var diem = _context.viewDiems
                        .Where(m => m.MenuID == 13)
                        .OrderBy(m => m.IDDiem)
                        .ToList();

                    if (diem.Any())
                    {

                        return View("XemDiem", diem);
                    }
                    else
                    {

                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }


            var normalMenu = _context.Menus.FirstOrDefault(m => m.MenuID == id && m.IsActive == true);
            if (normalMenu == null) return NotFound();

            return View(normalMenu);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
