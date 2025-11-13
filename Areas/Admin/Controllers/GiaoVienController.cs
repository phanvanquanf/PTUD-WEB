using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace aznews.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GiaoVienController : Controller
    {
        private readonly DataContext _context;

        public GiaoVienController(DataContext context)
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
            var GVList = _context.GiaoViens
                         .Include(gv => gv.Mon)
                         .OrderByDescending(gv => gv.GvienID)
                         .ToList();

            return View(GVList);
        }

        [HttpGet("IndexSearch")]
        public IActionResult Index(string input)
        {
            var GVList = _context.GiaoViens
                         .Include(gv => gv.Mon)
                         .OrderByDescending(gv => gv.GvienID)
                         .ToList();
            var result = _context.GiaoViens.Include(gv => gv.Mon).Where(item => item.HoTen != null && item.HoTen.Contains(input)).ToList();
            ViewData["SearchInput"] = input;
            if (input != null)
            {
                return View(result);
            }
            else
            {
                return View(GVList);
            }
        }

        public IActionResult SuaXoa()
        {
            var GVList = _context.GiaoViens
                         .Include(gv => gv.Mon)
                         .OrderByDescending(gv => gv.GvienID)
                         .ToList();

            return View(GVList);
        }

        [HttpGet("IndexSearch1")]
        public IActionResult SuaXoa(string input)
        {
            var GVList = _context.GiaoViens
                         .Include(gv => gv.Mon)
                         .OrderByDescending(gv => gv.GvienID)
                         .ToList();
            var result = _context.GiaoViens.Include(gv => gv.Mon).Where(item => item.HoTen != null && item.HoTen.Contains(input)).ToList();
            ViewData["SearchInput"] = input;
            if (input != null)
            {
                return View(result);
            }
            else
            {
                return View(GVList);
            }
        }

        public IActionResult Details(long? id)
        {
            var GVList = _context.GiaoViens
                         .Include(gv => gv.Mon)
                         .Where(m => m.GvienID == id)
                         .ToList();
            return View(GVList);
        }

        public IActionResult Create()
        {
            var mnList = (from m in _context.Menus
                          select new SelectListItem()
                          {
                              Text = m.MenuName,
                              Value = m.MenuID.ToString(),
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "--- Select ---",
                Value = string.Empty,
            });

            var monhoc = (from m in _context.Mons
                          select new SelectListItem()
                          {
                              Text = m.Mon,
                              Value = m.IDMon.ToString(),
                          }).ToList();
            monhoc.Insert(0, new SelectListItem()
            {
                Text = "--- Select ---",
                Value = string.Empty,
            });
            ViewBag.monhoc = monhoc;
            ViewBag.mnList = mnList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(tblGiaoVien gv)
        {
            gv.MenuID = 4;

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(gv.PhongCach))
                {
                    gv.PhongCach = RemoveHtmlTags(gv.PhongCach);
                }

                if (!string.IsNullOrEmpty(gv.DoiNet))
                {
                    gv.DoiNet = RemoveHtmlTags(gv.DoiNet);
                }

                if (!string.IsNullOrEmpty(gv.ThanhTich))
                {
                    gv.ThanhTich = RemoveHtmlTags(gv.ThanhTich);
                }

                _context.GiaoViens.Add(gv);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(gv);
        }


        public IActionResult Edit(long? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var giaovien = _context.GiaoViens.Find(id);
            if (giaovien == null)
                return NotFound();

            var mnList = (from m in _context.Menus
                          select new SelectListItem()
                          {
                              Text = m.MenuName,
                              Value = m.MenuID.ToString(),
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "--- Select ---",
                Value = "0",
            });

            var monhoc = (from m in _context.Mons
                          select new SelectListItem()
                          {
                              Text = m.Mon,
                              Value = m.IDMon.ToString(),
                          }).ToList();
            monhoc.Insert(0, new SelectListItem()
            {
                Text = "--- Select ---",
                Value = "0",
            });
            ViewBag.monhoc = monhoc;
            ViewBag.mnList = mnList;
            return View(giaovien);
        }

        [HttpPost]
        public IActionResult Edit(tblGiaoVien gv)
        {
            gv.MenuID = 4;

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(gv.PhongCach))
                {
                    gv.PhongCach = RemoveHtmlTags(gv.PhongCach);
                }

                if (!string.IsNullOrEmpty(gv.DoiNet))
                {
                    gv.DoiNet = RemoveHtmlTags(gv.DoiNet);
                }

                if (!string.IsNullOrEmpty(gv.ThanhTich))
                {
                    gv.ThanhTich = RemoveHtmlTags(gv.ThanhTich);
                }

                _context.GiaoViens.Update(gv);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(gv);
        }


        public IActionResult Delete(long? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var delGiaoVien = _context.GiaoViens.Find(id);
            if (delGiaoVien == null)
                return NotFound();
            return View(delGiaoVien);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var delGiaoVien = _context.GiaoViens.Find(id);
            if (delGiaoVien == null)
                return NotFound();
            _context.GiaoViens.Remove(delGiaoVien);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
