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
using Microsoft.CodeAnalysis.Elfie.Extensions;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;


namespace aznews.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class BaiGiangController : Controller
    {
        private readonly DataContext _context;

        public BaiGiangController(DataContext context)
        {
            _context = context;
        }

        private string RemoveHtmlTags(string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            return Regex.Replace(html, "<.*?>", string.Empty);
        }

        public IActionResult Index()
        {
            var GVList = _context.BaiGiangs
                            .Include(bg => bg.GiaoVien)
                            .Include(bg => bg.KhoaHoc)
                            .Include(bg => bg.Chuong)
                         .OrderByDescending(bg => bg.IDBaiGiang)
                         .ToList();

            return View(GVList);
        }

        [HttpGet("SearchBaiGiang")]

        public IActionResult Index(string input, string classSelect)
        {
            var GVList = _context.BaiGiangs
                                 .Include(bg => bg.GiaoVien)
                                 .Include(bg => bg.KhoaHoc)
                                 .Include(bg => bg.Chuong)
                                 .OrderByDescending(bg => bg.IDBaiGiang)
                                 .ToList();

            var result = _context.BaiGiangs
                                 .Include(bg => bg.GiaoVien)
                                 .Include(bg => bg.KhoaHoc)
                                 .Include(bg => bg.Chuong)
                                 .Where(item => item.TenBaiHoc != null && item.TenBaiHoc.Contains(input))
                                 .ToList();

            ViewData["SearchInput"] = input;

            var result1 = _context.BaiGiangs
                                  .Include(bg => bg.GiaoVien)
                                  .Include(bg => bg.KhoaHoc)
                                  .Include(bg => bg.Chuong)
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
                return View(GVList);
            }
        }

        public IActionResult SuaXoa()
        {
            var GVList = _context.BaiGiangs
                            .Include(bg => bg.GiaoVien)
                            .Include(bg => bg.KhoaHoc)
                            .Include(bg => bg.Chuong)
                         .OrderByDescending(bg => bg.IDBaiGiang)
                         .ToList();

            return View(GVList);
        }

        [HttpGet("SearchBaiGiang1")]
        public IActionResult SuaXoa(string input, string classSelect)
        {
            var GVList = _context.BaiGiangs
                                 .Include(bg => bg.GiaoVien)
                                 .Include(bg => bg.KhoaHoc)
                                 .Include(bg => bg.Chuong)
                                 .OrderByDescending(bg => bg.IDBaiGiang)
                                 .ToList();

            var result = _context.BaiGiangs
                                 .Include(bg => bg.GiaoVien)
                                 .Include(bg => bg.KhoaHoc)
                                 .Include(bg => bg.Chuong)
                                 .Where(item => item.TenBaiHoc != null && item.TenBaiHoc.Contains(input))
                                 .ToList();

            ViewData["SearchInput"] = input;
            // Lọc theo lớp học nếu có
            var result1 = _context.BaiGiangs
                                  .Include(bg => bg.GiaoVien)
                                  .Include(bg => bg.KhoaHoc)
                                  .Include(bg => bg.Chuong)
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
                return View(GVList);
            }
        }

        public IActionResult Details(long? id)
        {

            var baiGiang = _context.BaiGiangs
                                   .Include(bg => bg.GiaoVien)
                                   .Include(bg => bg.KhoaHoc)
                                   .Include(bg => bg.Chuong)
                                   .Include(bg => bg.Menu)
                                   .Where(bg => bg.IDBaiGiang == id).ToList();

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
            ViewBag.khList = khList;
            ViewBag.mnList = mnList;
            ViewBag.gvWithCourseList = gvWithCourseList;
            ViewBag.chuongList = chuongList;

            return View();
        }

        [HttpPost]
        public IActionResult Create(tblBaiGiang bg)
        {

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(bg.NDung))
                {
                    bg.NDung = RemoveHtmlTags(bg.NDung);
                }

                if (!string.IsNullOrEmpty(bg.MoTa))
                {
                    bg.MoTa = RemoveHtmlTags(bg.MoTa);
                }
                if (!string.IsNullOrEmpty(bg.Link))
                {
                    bg.Link = ExtractYouTubeVideoId(bg.Link);
                }
                _context.BaiGiangs.Add(bg);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(bg);
        }

        public IActionResult Index2(long id)
        {
            var baiGiangs = _context.BaiGiangs
                            .Include(bg => bg.GiaoVien)
                            .Include(bg => bg.KhoaHoc)
                            .Include(bg => bg.Chuong)
                            .OrderByDescending(bg => bg.IDBaiGiang)
                            .Where(b => b.Chuong != null && b.Chuong.IDChuong == id).ToList();
            return View(baiGiangs);
        }

        public IActionResult Create2(long id)
        {
            var chuongList = _context.Chuongs
                .Where(ch => ch.IDChuong == id)
                .Select(ch => new SelectListItem
                {
                    Text = ch.TenChuong,
                    Value = ch.IDChuong.ToString()
                })
                .ToList();

            var khoaHocId = _context.Chuongs
                .Where(ch => ch.IDChuong == id)
                .Select(ch => ch.IDKhoaHoc)
                .FirstOrDefault();

            var tenkh = _context.KhoaHocs
                .Where(ch => ch.IDKhoaHoc == khoaHocId)
                .Select(ch => new SelectListItem
                {
                    Text = ch.TenKH,
                    Value = ch.IDKhoaHoc.ToString()
                })
                .ToList();

            var mnList = _context.Menus
                .Select(menu => new SelectListItem
                {
                    Text = menu.MenuName,
                    Value = menu.MenuID.ToString()
                })
                .ToList();

            var gvList = _context.KhoaHocs
                .Where(ch => ch.IDKhoaHoc == khoaHocId)
                .Select(ch => ch.GvienID)
                .FirstOrDefault();

            var nameGV = _context.GiaoViens
                .Where(ch => ch.GvienID == gvList)
                .Select(menu => new SelectListItem
                {
                    Text = menu.HoTen,
                    Value = menu.GvienID.ToString()
                })
                .ToList();

            ViewBag.nameGV = nameGV;
            ViewBag.tenkh = tenkh;
            ViewBag.mnList = mnList;
            ViewBag.chuongList = chuongList;

            var model = new tblBaiGiang { IDChuong = id };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create2(tblBaiGiang model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.NDung))
                {
                    model.NDung = RemoveHtmlTags(model.NDung);
                }

                if (!string.IsNullOrEmpty(model.MoTa))
                {
                    model.MoTa = RemoveHtmlTags(model.MoTa);
                }

                if (!string.IsNullOrEmpty(model.Link))
                {
                    model.Link = ExtractYouTubeVideoId(model.Link);
                }

                _context.BaiGiangs.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index2", new { id = model.IDChuong });
            }

            return View(model);
        }
        public IActionResult Edit(long? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var baiGiang = _context.BaiGiangs.Find(id);


            if (baiGiang == null)
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

            var gvList = khoaHocs.Join(
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

            ViewBag.khList = khList;
            ViewBag.mnList = mnList;
            ViewBag.gvList = gvList;
            ViewBag.chuongList = chuongList;

            return View(baiGiang);
        }

        [HttpPost]
        public IActionResult Edit(tblBaiGiang bg)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(bg.NDung))
                {
                    bg.NDung = RemoveHtmlTags(bg.NDung);
                }

                if (!string.IsNullOrEmpty(bg.MoTa))
                {
                    bg.MoTa = RemoveHtmlTags(bg.MoTa);
                }
                if (!string.IsNullOrEmpty(bg.Link))
                {
                    bg.Link = ExtractYouTubeVideoId(bg.Link);
                }

                _context.BaiGiangs.Update(bg);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(bg);
        }


        public IActionResult Delete(long? id)
        {
            var GVList = _context.BaiGiangs
                            .Include(bg => bg.GiaoVien)
                            .Include(bg => bg.KhoaHoc)
                            .Include(bg => bg.Chuong)
                         .OrderByDescending(bg => bg.IDBaiGiang)
                         .ToList();
            if (id == null || id == 0)
                return NotFound();
            var delBG = _context.BaiGiangs.Find(id);
            if (delBG == null)
                return NotFound();
            return View(delBG);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var delBG = _context.BaiGiangs.Find(id);
            if (delBG == null)
                return NotFound();
            _context.BaiGiangs.Remove(delBG);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private string? ExtractYouTubeVideoId(string url)
        {
            if (string.IsNullOrEmpty(url)) return null;

            var regex = new Regex(@"(?:https?:\/\/(?:www\.)?(?:youtube\.com\/(?:watch\?v=|(?:[^\/]+\/)[^\/]+$)|shorts\/|youtu\.be\/))([^""&?\/\s]+)");
            var match = regex.Match(url);

            return match.Success ? match.Groups[1].Value : null;
        }
    }
}

