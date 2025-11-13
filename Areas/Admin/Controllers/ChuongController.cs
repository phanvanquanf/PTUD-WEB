using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;


namespace aznews.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChuongController : Controller
    {
        private readonly DataContext _context;

        public ChuongController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index2(long id)
        {
            ViewBag.IDKhoaHoc = id;

            var BTList = _context.Chuongs
                        .Include(bt => bt.KhoaHoc)
                        .OrderByDescending(bt => bt.IDChuong) // Sắp xếp theo GvienID
                        .Where(b => b.KhoaHoc != null && b.KhoaHoc.IDKhoaHoc == id).ToList();
            return View(BTList);
        }

        public IActionResult Index()
        {
            var BTList = _context.Chuongs
                        .Include(bt => bt.KhoaHoc)
                        .OrderByDescending(bt => bt.IDChuong) // Sắp xếp theo GvienID
                        .ToList();

            return View(BTList);
        }

        [HttpGet("SearchChuong")]
        public IActionResult Index(string input)
        {
            var khList = _context.Chuongs
                        .Include(bt => bt.KhoaHoc).
                        OrderByDescending(m => m.IDChuong).ToList();

            var Search = _context.Chuongs.Where(item => item.TenChuong != null && item.TenChuong.Contains(input)).ToList();
            ViewData["SearchInput"] = input; // Để giữ lại giá trị input sau khi tìm kiếm
            if (input != null)
            {
                return View(Search);
            }
            else
            {
                return View(khList);
            }
        }

        public IActionResult Create()
        {
            var khList = (from m in _context.KhoaHocs
                          select new SelectListItem()
                          {
                              Text = m.TenKH,
                              Value = m.IDKhoaHoc.ToString()
                          }).ToList();
            khList.Insert(0, new SelectListItem()
            {
                Text = "--- select ---",
                Value = "0"
            });
            ViewBag.khList = khList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(tblChuong ch)
        {
            if (ModelState.IsValid)
            {
                _context.Chuongs.Add(ch);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ch);
        }

        public IActionResult Create2(long id)
        {
            var khList = _context.KhoaHocs
                .Where(kh => kh.IDKhoaHoc == id)
                .Select(kh => new SelectListItem
                {
                    Text = kh.TenKH,
                    Value = kh.IDKhoaHoc.ToString()
                })
                .ToList();

            var model = new tblChuong { IDKhoaHoc = id };

            ViewBag.khList = khList;

            return View(model);
        }


        [HttpPost]
        public IActionResult Create2(tblChuong ch)
        {
            if (ModelState.IsValid)
            {
                _context.Chuongs.Add(ch);
                _context.SaveChanges();

                return RedirectToAction("Index2", new { id = ch.IDKhoaHoc });
            }

            return View(ch);
        }

        public IActionResult Edit(long? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var kh = _context.Chuongs.Find(id);
            if (kh == null)
                return NotFound();

            var khList = (from m in _context.KhoaHocs
                          select new SelectListItem()
                          {
                              Text = m.TenKH,
                              Value = m.IDKhoaHoc.ToString()
                          }).ToList();
            khList.Insert(0, new SelectListItem()
            {
                Text = "--- select ---",
                Value = "0"
            });
            ViewBag.khList = khList;
            return View(kh);
        }

        [HttpPost]
        public IActionResult Edit(tblChuong kh)
        {
            if (ModelState.IsValid)
            {
                _context.Chuongs.Update(kh);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kh);
        }

        public IActionResult Delete(long? id)
        {
            var BTList = _context.Chuongs
                        .Include(bt => bt.KhoaHoc)
                         .OrderByDescending(bt => bt.IDChuong)
                         .ToList();
            if (id == null || id == 0)
                return NotFound();
            var kh = _context.Chuongs.Find(id);
            if (kh == null)
                return NotFound();
            return View(kh);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var delchuong = _context.Chuongs.Find(id);
            if (delchuong == null)
                return NotFound();
            _context.Chuongs.Remove(delchuong);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}