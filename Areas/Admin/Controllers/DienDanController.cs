using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Core;
using System.Text.RegularExpressions;

namespace aznews.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DienDanController : Controller
    {
        private readonly DataContext _context;

        public DienDanController(DataContext context)
        {
            _context = context;
        }

        private string RemoveHtmlTags(string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            return Regex.Replace(html, "<.*?>", string.Empty);
        }

        [Route("/Admin/DienDan/Index/{page:int?}", Name = "PostIndex")]
        public IActionResult Index(int page = 1)
        {
            var posts = _context.DienDans.OrderByDescending(p => p.IDBaiViet);
            int pageSize = 5;
            var models = new PagedList<tblDienDan>(posts, page, pageSize);

            if (models == null)
                return NotFound();
            return View(models);
        }
        public IActionResult SuaXoa(int page = 1)
        {
            var posts = _context.DienDans.OrderByDescending(p => p.IDBaiViet);
            int pageSize = 5;
            var models = new PagedList<tblDienDan>(posts, page, pageSize);

            if (models == null)
                return NotFound();
            return View(models);
        }

        public IActionResult Details(int? id)
        {
            var diendan = _context.DienDans.Where(m => m.IDBaiViet == id).ToList();
            return View(diendan);
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
            ViewBag.mnList = mnList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(tblDienDan post)
        {
            post.MenuID = 6;

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(post.NoiDung))
                {
                    post.NoiDung = RemoveHtmlTags(post.NoiDung);
                }

                _context.DienDans.Add(post);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(post);
        }

        public IActionResult Edit(long? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var diendan = _context.DienDans.Find(id);
            if (diendan == null)
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
            ViewBag.mnList = mnList;
            return View(diendan);
        }

        [HttpPost]
        public IActionResult Edit(tblDienDan diendan)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(diendan.NoiDung))
                {
                    diendan.NoiDung = RemoveHtmlTags(diendan.NoiDung);
                }
                _context.DienDans.Update(diendan);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diendan);
        }

        public IActionResult Delete(long? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var delDienDan = _context.DienDans.Find(id);
            if (delDienDan == null)
                return NotFound();
            return View(delDienDan);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var delDienDan = _context.DienDans.Find(id);
            if (delDienDan == null)
                return NotFound();
            _context.DienDans.Remove(delDienDan);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
