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


namespace aznews.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class TienTrinhController : Controller
    {
        private readonly DataContext _context;

        public TienTrinhController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tienTrinhList = _context.TienTrinhs
                                .Include(t => t.HocVien)
                                .Include(t => t.KhoaHoc)
                                .Include(t => t.BaiGiang)
                                .Include(t => t.GiaoVien)
                                .OrderByDescending(t => t.IDTienTrinh)
                                .ToList();
            return View(tienTrinhList);
        }

        [HttpGet("SearchTienTrinh")]
        public IActionResult Index(string input)
        {
            var tienTrinhList = _context.TienTrinhs
                                .Include(t => t.HocVien)
                                .Include(t => t.KhoaHoc)
                                .Include(t => t.BaiGiang)
                                .Include(t => t.GiaoVien)
                                .OrderByDescending(t => t.IDTienTrinh)
                                .ToList(); ;

            var result = _context.TienTrinhs
                                 .Include(bg => bg.HocVien)
                                 .Where(item => item.HocVien != null && item.HocVien.HoTenHV != null && item.HocVien.HoTenHV.Contains(input))
                                 .ToList();

            ViewData["SearchInput"] = input;

            if (!string.IsNullOrEmpty(input))
            {
                return View(result);
            }
            else
            {
                return View(tienTrinhList);
            }
        }
        public IActionResult SuaXoa()
        {
            var tienTrinhList = _context.TienTrinhs
                                .Include(t => t.HocVien)
                                .Include(t => t.KhoaHoc)
                                .Include(t => t.BaiGiang)
                                .Include(t => t.GiaoVien)
                                .OrderByDescending(t => t.IDTienTrinh)
                                .ToList();
            return View(tienTrinhList);
        }

        [HttpGet("SearchTienTrinh1")]
        public IActionResult SuaXoa(string input)
        {
            var tienTrinhList = _context.TienTrinhs
                                .Include(t => t.HocVien)
                                .Include(t => t.KhoaHoc)
                                .Include(t => t.BaiGiang)
                                .Include(t => t.GiaoVien)
                                .OrderByDescending(t => t.IDTienTrinh)
                                .ToList();

            var result = _context.TienTrinhs
                                 .Include(bg => bg.HocVien)
                                 .Where(item => item.HocVien != null && item.HocVien.HoTenHV != null && item.HocVien.HoTenHV.Contains(input))
                                 .ToList();

            ViewData["SearchInput"] = input;

            if (!string.IsNullOrEmpty(input))
            {
                return View(result);
            }
            else
            {
                return View(tienTrinhList);
            }
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
               .Select(h => new SelectListItem
               {
                   Text = h.MenuName,
                   Value = h.MenuID.ToString(),
               }).ToList();
            mnList.Insert(0, new SelectListItem { Text = "--- Select ---", Value = string.Empty });

            var HVList = _context.HocViens
                .Select(h => new SelectListItem
                {
                    Text = h.HoTenHV,
                    Value = h.IDHocVien.ToString(),
                }).ToList();
            HVList.Insert(0, new SelectListItem { Text = "--- Select ---", Value = string.Empty });

            var KhoaHoc = _context.KhoaHocs
                .Select(kh => new
                {
                    kh.IDKhoaHoc,
                    kh.GvienID,
                    kh.IDMon,
                }).ToList();

            var BaiGiang = _context.BaiGiangs
                .Select(bg => new
                {
                    bg.IDBaiGiang,
                    bg.IDKhoaHoc,
                    bg.TenBaiHoc,
                }).ToList();

            var GiaoVien = _context.GiaoViens
                .Select(gv => new
                {
                    gv.GvienID,
                    gv.HoTen,
                }).ToList();

            var gvList = KhoaHoc.Join(
                GiaoVien,
                kh => kh.GvienID,
                gv => gv.GvienID,
                (kh, gv) => new
                {
                    gv.GvienID,
                    gv.HoTen,
                    kh.IDKhoaHoc
                }).ToList();

            var bgList = KhoaHoc.Join(
               BaiGiang,
               kh => kh.IDKhoaHoc,
               bg => bg.IDKhoaHoc,
               (kh, bg) => new
               {
                   bg.IDBaiGiang,
                   bg.TenBaiHoc,
                   kh.IDKhoaHoc
               }).ToList();

            ViewBag.mnList = mnList;
            ViewBag.khList = khList;
            ViewBag.HVList = HVList;
            ViewBag.gvList = gvList;
            ViewBag.bgList = bgList;

            return View();
        }

        [HttpPost]
        public IActionResult Create(tblTienTrinh tt)
        {
            if (ModelState.IsValid)
            {
                _context.TienTrinhs.Add(tt);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(tt);
        }


        public IActionResult Edit(long? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var tientrinh = _context.TienTrinhs.Find(id);


            if (tientrinh == null)
                return NotFound();

            var khList = _context.KhoaHocs
                .Select(kh => new SelectListItem
                {
                    Text = kh.TenKH,
                    Value = kh.IDKhoaHoc.ToString()
                }).ToList();
            khList.Insert(0, new SelectListItem { Text = "--- Select ---", Value = string.Empty });

            var mnList = _context.Menus
               .Select(h => new SelectListItem
               {
                   Text = h.MenuName,
                   Value = h.MenuID.ToString(),
               }).ToList();
            mnList.Insert(0, new SelectListItem { Text = "--- Select ---", Value = string.Empty });

            var HVList = _context.HocViens
                .Select(h => new SelectListItem
                {
                    Text = h.HoTenHV,
                    Value = h.IDHocVien.ToString(),
                }).ToList();
            HVList.Insert(0, new SelectListItem { Text = "--- Select ---", Value = string.Empty });

            var KhoaHoc = _context.KhoaHocs
                .Select(kh => new
                {
                    kh.IDKhoaHoc,
                    kh.GvienID,
                    kh.IDMon,
                }).ToList();

            var BaiGiang = _context.BaiGiangs
                .Select(bg => new
                {
                    bg.IDBaiGiang,
                    bg.IDKhoaHoc,
                    bg.TenBaiHoc,
                }).ToList();

            var GiaoVien = _context.GiaoViens
                .Select(gv => new
                {
                    gv.GvienID,
                    gv.HoTen,
                }).ToList();

            var gvList = KhoaHoc.Join(
                GiaoVien,
                kh => kh.GvienID,
                gv => gv.GvienID,
                (kh, gv) => new
                {
                    gv.GvienID,
                    gv.HoTen,
                    kh.IDKhoaHoc
                }).ToList();

            var bgList = KhoaHoc.Join(
               BaiGiang,
               kh => kh.IDKhoaHoc,
               bg => bg.IDKhoaHoc,
               (kh, bg) => new
               {
                   bg.IDBaiGiang,
                   bg.TenBaiHoc,
                   kh.IDKhoaHoc
               }).ToList();

            ViewBag.mnList = mnList;
            ViewBag.khList = khList;
            ViewBag.HVList = HVList;
            ViewBag.gvList = gvList;
            ViewBag.bgList = bgList;

            return View(tientrinh);
        }

        [HttpPost]
        public IActionResult Edit(tblTienTrinh tt)
        {
            if (ModelState.IsValid)
            {
                _context.TienTrinhs.Update(tt);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(tt);
        }

        public IActionResult Delete(long? id)
        {
            var tienTrinhList = _context.TienTrinhs
                                .Include(t => t.HocVien)
                                .Include(t => t.KhoaHoc)
                                .Include(t => t.BaiGiang)
                                .Include(t => t.GiaoVien)
                                .OrderByDescending(t => t.IDTienTrinh)
                                .ToList();
            if (id == null || id == 0)
                return NotFound();
            var delTienTrinh = _context.TienTrinhs.Find(id);
            if (delTienTrinh == null)
                return NotFound();
            return View(delTienTrinh);

        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var delTienTrinh = _context.TienTrinhs.Find(id);
            if (delTienTrinh == null)
                return NotFound();
            _context.TienTrinhs.Remove(delTienTrinh);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
