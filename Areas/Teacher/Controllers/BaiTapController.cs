using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using PagedList.Core;
using aznews.Components;
using Microsoft.EntityFrameworkCore;
using Azure.Core.GeoJson;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SixLabors.ImageSharp.PixelFormats;
using System.Text.RegularExpressions;


namespace aznews.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class BaiTapController : Controller
    {
        private readonly DataContext _context;

        public BaiTapController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var BTList = _context.BaiTaps
                            .Include(bt => bt.GiaoVien)
                            .Include(bt => bt.KhoaHoc)
                            .Include(bt => bt.BaiGiang)
                            .Include(bt => bt.Chuong)
                         .OrderByDescending(bt => bt.IDBaiTap)
                         .ToList();

            return View(BTList);
        }

        [HttpGet("SearchBaiTap")]
        public IActionResult Index(string input, string classSelect)
        {
            var BTList = _context.BaiTaps
                                 .Include(bt => bt.GiaoVien)
                                 .Include(bt => bt.KhoaHoc)
                                 .Include(bt => bt.BaiGiang)
                                 .Include(bt => bt.Chuong)
                                 .OrderByDescending(bt => bt.IDBaiTap)
                                 .ToList();

            var result = _context.BaiTaps
                                 .Include(bt => bt.GiaoVien)
                                 .Include(bt => bt.KhoaHoc)
                                 .Include(bt => bt.BaiGiang)
                                 .Include(bt => bt.Chuong)
                                 .Where(item => item.TenBaiTap != null && item.TenBaiTap.Contains(input))
                                 .ToList();

            ViewData["SearchInput"] = input;


            var result1 = _context.BaiTaps
                                  .Include(bt => bt.GiaoVien)
                                  .Include(bt => bt.KhoaHoc)
                                  .Include(bt => bt.BaiGiang)
                                  .Include(bt => bt.Chuong)
                                  .Where(item => item.KhoaHoc != null && item.KhoaHoc.Lop.ToString() == classSelect)
                                  .ToList();

            ViewData["SearchLop"] = classSelect;

            if (!string.IsNullOrEmpty(input))
            {
                return View(result);
            }
            else if (!string.IsNullOrEmpty(classSelect))
            {
                return View(result1);
            }
            else
            {
                return View(BTList);
            }
        }

        public IActionResult SuaXoa()
        {
            var BTList = _context.BaiTaps
                            .Include(bt => bt.GiaoVien)
                            .Include(bt => bt.KhoaHoc)
                            .Include(bt => bt.BaiGiang)
                            .Include(bt => bt.Chuong)
                         .OrderByDescending(bt => bt.IDBaiTap)
                         .ToList();

            return View(BTList);
        }

        [HttpGet("SearchBaiTap1")]
        public IActionResult SuaXoa(string input, string classSelect)
        {
            var BTList = _context.BaiTaps
                                 .Include(bt => bt.GiaoVien)
                                 .Include(bt => bt.KhoaHoc)
                                 .Include(bt => bt.BaiGiang)
                                 .Include(bt => bt.Chuong)
                                 .OrderByDescending(bt => bt.IDBaiTap)
                                 .ToList();

            var result = _context.BaiTaps
                                 .Include(bt => bt.GiaoVien)
                                 .Include(bt => bt.KhoaHoc)
                                 .Include(bt => bt.BaiGiang)
                                 .Include(bt => bt.Chuong)
                                 .Where(item => item.TenBaiTap != null && item.TenBaiTap.Contains(input))
                                 .ToList();

            ViewData["SearchInput"] = input;

            var result1 = _context.BaiTaps
                                  .Include(bt => bt.GiaoVien)
                                  .Include(bt => bt.KhoaHoc)
                                  .Include(bt => bt.BaiGiang)
                                  .Include(bt => bt.Chuong)
                                  .Where(item => item.KhoaHoc != null && item.KhoaHoc.Lop.ToString() == classSelect)
                                  .ToList();

            ViewData["SearchLop"] = classSelect;

            if (!string.IsNullOrEmpty(input))
            {
                return View(result);
            }
            else if (!string.IsNullOrEmpty(classSelect))
            {
                return View(result1);
            }
            else
            {
                return View(BTList);
            }
        }

        public IActionResult Details(long? id)
        {

            var baiGiang = _context.BaiTaps
                                   .Include(bt => bt.GiaoVien)
                                   .Include(bt => bt.KhoaHoc)
                                   .Include(bt => bt.BaiGiang)
                                   .Include(bt => bt.Menu)
                                   .Include(bt => bt.Chuong)
                                   .Where(bt => bt.IDBaiTap == id).ToList();

            return View(baiGiang);
        }


        public IActionResult Create()
        {
            var khList = _context.KhoaHocs
                .Select(kh => new SelectListItem
                {
                    Text = kh.TenKH,
                    Value = kh.IDKhoaHoc.ToString()
                }).ToList();
            khList.Insert(0, new SelectListItem { Text = "--- Select ---", Value = string.Empty });

            var mnList = _context.Menus
               .Select(menu => new SelectListItem
               {
                   Text = menu.MenuName,
                   Value = menu.MenuID.ToString()
               }).ToList();
            mnList.Insert(0, new SelectListItem { Text = "--- Select ---", Value = string.Empty });

            var khoaHocs = _context.KhoaHocs
                .Select(kh => new
                {
                    kh.IDKhoaHoc,
                    kh.GvienID
                }).ToList();

            var giaoViens = _context.GiaoViens
                .Select(gv => new
                {
                    gv.GvienID,
                    gv.HoTen,
                }).ToList();

            var Chuongs = _context.Chuongs
            .Select(chuong => new
            {
                chuong.IDKhoaHoc,
                chuong.IDChuong,
                chuong.TenChuong,
            }).ToList();

            var BaiGiangs = _context.BaiGiangs
            .Select(bg => new
            {
                bg.IDBaiGiang,
                bg.TenBaiHoc,
                bg.IDKhoaHoc,
            }).ToList();

            var gvWithCourseList = khoaHocs.Join(
                giaoViens,
                kh => kh.GvienID,
                gv => gv.GvienID,
                (kh, gv) => new
                {
                    kh.IDKhoaHoc,
                    gv.GvienID,
                    gv.HoTen
                }).ToList();

            var chuongList = khoaHocs.Join(
                 Chuongs,
                 kh => kh.IDKhoaHoc,
                 chuong => chuong.IDKhoaHoc,
                 (kh, chuong) => new
                 {
                     kh.IDKhoaHoc,
                     chuong.IDChuong,
                     chuong.TenChuong
                 }).ToList();

            var BGList = khoaHocs.Join(
                BaiGiangs,
                kh => kh.IDKhoaHoc,
                bg => bg.IDKhoaHoc,
                (kh, bg) => new
                {
                    kh.IDKhoaHoc,
                    bg.IDBaiGiang,
                    bg.TenBaiHoc,
                }).ToList();


            ViewBag.khList = khList;
            ViewBag.mnList = mnList;
            ViewBag.gvWithCourseList = gvWithCourseList;
            ViewBag.chuongList = chuongList;
            ViewBag.BGList = BGList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(tblBaiTap bt)
        {

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(bt.Link))
                {
                    bt.Link = ConvertToPreviewLink(bt.Link);
                }

                _context.BaiTaps.Add(bt);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(bt);
        }

        public IActionResult Edit(long? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var baiTap = _context.BaiTaps.Find(id);
            if (baiTap == null)
                return NotFound();

            var khList = _context.KhoaHocs
                .Select(kh => new SelectListItem
                {
                    Text = kh.TenKH,
                    Value = kh.IDKhoaHoc.ToString()
                }).ToList();
            khList.Insert(0, new SelectListItem { Text = "--- Select ---", Value = string.Empty });

            var mnList = _context.Menus
               .Select(menu => new SelectListItem
               {
                   Text = menu.MenuName,
                   Value = menu.MenuID.ToString()
               }).ToList();
            mnList.Insert(0, new SelectListItem { Text = "--- Select ---", Value = string.Empty });

            var khoaHocs = _context.KhoaHocs
                .Select(kh => new
                {
                    kh.IDKhoaHoc,
                    kh.GvienID
                }).ToList();

            var giaoViens = _context.GiaoViens
                .Select(gv => new
                {
                    gv.GvienID,
                    gv.HoTen,
                }).ToList();

            var Chuongs = _context.Chuongs
            .Select(chuong => new
            {
                chuong.IDKhoaHoc,
                chuong.IDChuong,
                chuong.TenChuong,
            }).ToList();

            var BaiGiangs = _context.BaiGiangs
            .Select(bg => new
            {
                bg.IDBaiGiang,
                bg.TenBaiHoc,
                bg.IDKhoaHoc,
            }).ToList();

            var gvWithCourseList = khoaHocs.Join(
                giaoViens,
                kh => kh.GvienID,
                gv => gv.GvienID,
                (kh, gv) => new
                {
                    kh.IDKhoaHoc,
                    gv.GvienID,
                    gv.HoTen
                }).ToList();

            var chuongList = khoaHocs.Join(
                 Chuongs,
                 kh => kh.IDKhoaHoc,
                 chuong => chuong.IDKhoaHoc,
                 (kh, chuong) => new
                 {
                     kh.IDKhoaHoc,
                     chuong.IDChuong,
                     chuong.TenChuong
                 }).ToList();

            var BGList = khoaHocs.Join(
                BaiGiangs,
                kh => kh.IDKhoaHoc,
                bg => bg.IDKhoaHoc,
                (kh, bg) => new
                {
                    kh.IDKhoaHoc,
                    bg.IDBaiGiang,
                    bg.TenBaiHoc,
                }).ToList();


            ViewBag.khList = khList;
            ViewBag.mnList = mnList;
            ViewBag.gvWithCourseList = gvWithCourseList;
            ViewBag.chuongList = chuongList;
            ViewBag.BGList = BGList;
            return View(baiTap);
        }

        [HttpPost]
        public IActionResult Edit(tblBaiTap bt)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(bt.Link))
                {
                    bt.Link = ConvertToPreviewLink(bt.Link);
                }
                _context.BaiTaps.Update(bt);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(bt);
        }

        public IActionResult Delete(long? id)
        {
            var BTList = _context.BaiTaps
                            .Include(bt => bt.GiaoVien)
                            .Include(bt => bt.KhoaHoc)
                            .Include(bt => bt.BaiGiang)
                            .Include(bt => bt.Chuong)
                         .OrderByDescending(bt => bt.IDBaiTap)
                         .ToList();
            if (id == null || id == 0)
                return NotFound();
            var delBG = _context.BaiTaps.Find(id);
            if (delBG == null)
                return NotFound();
            return View(delBG);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var delBG = _context.BaiTaps.Find(id);
            if (delBG == null)
                return NotFound();
            _context.BaiTaps.Remove(delBG);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public static string? ConvertToPreviewLink(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return null;

            // Cập nhật Regex để tìm Google Docs
            var regex = new Regex(@"https:\/\/(?:drive\.google\.com|docs\.google\.com)\/(file|document)\/d\/([^\/]+)\/(?:view|edit).*");
            var match = regex.Match(url);

            if (match.Success)
            {
                var fileId = match.Groups[2].Value;
                return $"https://drive.google.com/file/d/{fileId}/preview";
            }

            return null;
        }

    }
}

