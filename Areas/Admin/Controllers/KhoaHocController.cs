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


namespace aznews.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KhoaHocController : Controller
    {
        private readonly DataContext _context;

        public KhoaHocController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var KHList = _context.KhoaHocs
                            .Include(bg => bg.GiaoVien)
                         .OrderByDescending(bg => bg.IDKhoaHoc)
                         .ToList();
            return View(KHList);
        }

        public IActionResult Details(long? id)
        {
            var KHList = _context.KhoaHocs
                            .Include(bg => bg.GiaoVien)
                             .Include(bg => bg.Mon)
                         .OrderByDescending(bg => bg.IDKhoaHoc == id)
                         .ToList();
            return View(KHList);
        }

        public IActionResult SuaXoa()
        {
            var KHList = _context.KhoaHocs
                            .Include(bg => bg.GiaoVien)
                         .OrderByDescending(bg => bg.IDKhoaHoc)
                         .ToList();
            return View(KHList);
        }

        [HttpGet("SearchKhoaHoc1")]
        public IActionResult SuaXoa(string input, string classSelect)
        {
            var KHList = _context.KhoaHocs
                                 .Include(bg => bg.GiaoVien)
                                 .OrderByDescending(bg => bg.IDKhoaHoc)
                                 .ToList();

            var result = _context.KhoaHocs
                                 .Include(bg => bg.GiaoVien)
                                 .Where(item => item.TenKH != null && item.TenKH.Contains(input))
                                 .ToList();

            ViewData["SearchInput"] = input;


            var result1 = _context.KhoaHocs
                                  .Where(item => item.Lop.ToString() == classSelect)
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
                return View(KHList);
            }
        }


        [HttpGet("SearchKhoaHoc")]
        public IActionResult Index(string input, string classSelect)
        {
            var KHList = _context.KhoaHocs
                                 .Include(bg => bg.GiaoVien)
                                 .OrderByDescending(bg => bg.IDKhoaHoc)
                                 .ToList();

            var result = _context.KhoaHocs
                                 .Include(bg => bg.GiaoVien)
                                 .Where(item => item.TenKH != null && item.TenKH.Contains(input))
                                 .ToList();

            ViewData["SearchInput"] = input;

            var result1 = _context.KhoaHocs
                                  .Where(item => item.Lop.ToString() == classSelect)
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
                return View(KHList);
            }
        }
        public IActionResult Create()
        {
            var mnList = _context.Menus
                .Select(menu => new SelectListItem
                {
                    Text = menu.MenuName,
                    Value = menu.MenuID.ToString()
                })
                .ToList();
            mnList.Insert(0, new SelectListItem { Text = "--- Select ---", Value = string.Empty });

            var gvList = _context.GiaoViens
                .Select(gv => new SelectListItem
                {
                    Text = gv.HoTen,
                    Value = gv.GvienID.ToString()
                })
                .ToList();
            gvList.Insert(0, new SelectListItem { Text = "--- Select ---", Value = string.Empty });

            var monList = _context.Mons
                .Select(m => new SelectListItem
                {
                    Text = m.Mon,
                    Value = m.IDMon.ToString()
                })
                .ToList();
            monList.Insert(0, new SelectListItem { Text = "--- Select ---", Value = string.Empty });

            ViewBag.mnList = mnList;
            ViewBag.gvList = gvList;
            ViewBag.monList = monList;

            return View();
        }

        [HttpPost]
        public IActionResult Create(tblKhoaHoc bg)
        {
            if (ModelState.IsValid)
            {
                _context.KhoaHocs.Add(bg);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(bg);
        }


        public IActionResult Edit(long? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var khoaHoc = _context.KhoaHocs.Find(id);


            if (khoaHoc == null)
                return NotFound();

            var mnList = _context.Menus
                .Select(menu => new SelectListItem
                {
                    Text = menu.MenuName,
                    Value = menu.MenuID.ToString()
                })
                .ToList();
            mnList.Insert(0, new SelectListItem { Text = "--- Select ---", Value = string.Empty });

            var gvList = _context.GiaoViens
                .Select(gv => new SelectListItem
                {
                    Text = gv.HoTen,
                    Value = gv.GvienID.ToString()
                })
                .ToList();
            gvList.Insert(0, new SelectListItem { Text = "--- Select ---", Value = string.Empty });

            var monList = _context.Mons
                .Select(m => new SelectListItem
                {
                    Text = m.Mon,
                    Value = m.IDMon.ToString()
                })
                .ToList();
            monList.Insert(0, new SelectListItem { Text = "--- Select ---", Value = string.Empty });

            ViewBag.mnList = mnList;
            ViewBag.gvList = gvList;
            ViewBag.monList = monList;

            return View(khoaHoc);
        }

        [HttpPost]
        public IActionResult Edit(tblKhoaHoc bg)
        {
            if (ModelState.IsValid)
            {
                _context.KhoaHocs.Update(bg);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(bg);
        }


        public IActionResult Delete(long? id)
        {
            var GVList = _context.KhoaHocs
                        .Include(bg => bg.GiaoVien)
                         .OrderByDescending(bg => bg.IDKhoaHoc)
                         .ToList();
            if (id == null || id == 0)
                return NotFound();
            var delBG = _context.KhoaHocs.Find(id);
            if (delBG == null)
                return NotFound();
            return View(delBG);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var delBG = _context.KhoaHocs.Find(id);
            if (delBG == null)
                return NotFound();
            _context.KhoaHocs.Remove(delBG);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

