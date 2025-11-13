using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Components;
using aznews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace aznews.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class DiemController : Controller
    {
        private readonly DataContext _context;

        public DiemController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var diemList = _context.Diems
                        .Include(d => d.HocVien)
                        .Include(d => d.Mon)
                        .Include(d => d.GiaoVien)
                        .Include(d => d.BaiTap)
                        .Include(d => d.KhoaHoc)
                        .OrderByDescending(d => d.IDDiem)
                        .ToList();

            return View(diemList);
        }

        [HttpGet("SearchDiem")]
        public IActionResult Index(string input)
        {
            var diemList = _context.Diems
                            .Include(d => d.HocVien)
                            .Include(d => d.Mon)
                            .Include(d => d.GiaoVien)
                            .Include(d => d.BaiTap)
                            .Include(d => d.KhoaHoc)
                            .OrderByDescending(d => d.IDDiem)
                            .ToList();

            var searchResults = _context.Diems
                .Where(item => item.HocVien != null && item.HocVien.HoTenHV != null && item.HocVien.HoTenHV.Contains(input))
                .ToList();

            ViewData["SearchInput"] = input;

            if (input != null)
            {
                return View(searchResults);
            }
            else
            {
                return View(diemList);
            }
        }

        public IActionResult SuaXoa()
        {
            var diemList = _context.Diems
                        .Include(d => d.HocVien)
                        .Include(d => d.Mon)
                        .Include(d => d.GiaoVien)
                        .Include(d => d.BaiTap)
                        .Include(d => d.KhoaHoc)
                        .OrderByDescending(d => d.IDDiem)
                        .ToList();

            return View(diemList);
        }

        [HttpGet("SearchDiem1")]
        public IActionResult SuaXoa(string input)
        {
            var diemList = _context.Diems
                            .Include(d => d.HocVien)
                            .Include(d => d.Mon)
                            .Include(d => d.GiaoVien)
                            .Include(d => d.BaiTap)
                            .Include(d => d.KhoaHoc)
                            .OrderByDescending(d => d.IDDiem)
                            .ToList();

            var searchResults = _context.Diems
                .Where(item => item.HocVien != null && item.HocVien.HoTenHV != null && item.HocVien.HoTenHV.Contains(input))
                .ToList();

            ViewData["SearchInput1"] = input;

            if (input != null)
            {
                return View(searchResults);
            }
            else
            {
                return View(diemList);
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

            var BaiTap = _context.BaiTaps
                .Select(bt => new
                {
                    bt.IDBaiTap,
                    bt.IDKhoaHoc,
                    bt.TenBaiTap,
                }).ToList();

            var GiaoVien = _context.GiaoViens
                .Select(gv => new
                {
                    gv.GvienID,
                    gv.HoTen,
                }).ToList();

            var Mon = _context.Mons
                .Select(m => new
                {
                    m.IDMon,
                    m.Mon,
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

            var BTList = KhoaHoc.Join(
                BaiTap,
                kh => kh.IDKhoaHoc,
                bt => bt.IDKhoaHoc,
                (kh, bt) => new
                {
                    bt.IDBaiTap,
                    bt.TenBaiTap,
                    kh.IDKhoaHoc,
                }
            ).ToList();

            var monList = KhoaHoc.Join(
                Mon,
                kh => kh.IDMon,
                m => m.IDMon,
                (kh, m) => new
                {
                    m.IDMon,
                    m.Mon,
                    kh.IDKhoaHoc,
                }).ToList();

            ViewBag.mnList = mnList;
            ViewBag.khList = khList;
            ViewBag.HVList = HVList;
            ViewBag.BTList = BTList;
            ViewBag.gvList = gvList;
            ViewBag.monList = monList;

            return View();
        }

        [HttpPost]
        public IActionResult Create(tblDiem d)
        {
            if (ModelState.IsValid)
            {
                d.XepLoai = d.Diem >= 5 ? 1 : 0;
                _context.Diems.Add(d);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(d);
        }

        public IActionResult Edit(long? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var diem = _context.Diems.Find(id);
            if (diem == null)
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

            var BaiTap = _context.BaiTaps
                .Select(bt => new
                {
                    bt.IDBaiTap,
                    bt.IDKhoaHoc,
                    bt.TenBaiTap,
                }).ToList();

            var GiaoVien = _context.GiaoViens
                .Select(gv => new
                {
                    gv.GvienID,
                    gv.HoTen,
                }).ToList();

            var Mon = _context.Mons
                .Select(m => new
                {
                    m.IDMon,
                    m.Mon,
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

            var BTList = KhoaHoc.Join(
                BaiTap,
                kh => kh.IDKhoaHoc,
                bt => bt.IDKhoaHoc,
                (kh, bt) => new
                {
                    bt.IDBaiTap,
                    bt.TenBaiTap,
                    kh.IDKhoaHoc,
                }
            ).ToList();

            var monList = KhoaHoc.Join(
                Mon,
                kh => kh.IDMon,
                m => m.IDMon,
                (kh, m) => new
                {
                    m.IDMon,
                    m.Mon,
                    kh.IDKhoaHoc,
                }).ToList();

            ViewBag.mnList = mnList;
            ViewBag.khList = khList;
            ViewBag.HVList = HVList;
            ViewBag.BTList = BTList;
            ViewBag.gvList = gvList;
            ViewBag.monList = monList;
            return View(diem);
        }

        [HttpPost]
        public IActionResult Edit(tblDiem d)
        {
            if (ModelState.IsValid)
            {
                d.XepLoai = d.Diem >= 5 ? 1 : 0;
                _context.Diems.Update(d);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(d);
        }

        public IActionResult Delete(long? id)
        {
            var diemList = _context.Diems
                            .Include(d => d.KhoaHoc)
                            .Include(d => d.BaiTap)
                            .Include(d => d.Mon)
                            .Include(d => d.GiaoVien)
                            .Include(d => d.HocVien)
                            .OrderBy(d => d.IDDiem)
                            .ToList();
            if (id == null || id == 0)
                return NotFound();
            var diem = _context.Diems.Find(id);
            if (diem == null)
                return NotFound();
            return View(diem);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var delDiem = _context.Diems.Find(id);
            if (delDiem == null)
                return NotFound();
            _context.Diems.Remove(delDiem);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
