using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace aznews.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HocVienController : Controller
    {
        private readonly DataContext _context;

        public HocVienController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var HVList = _context.HocViens
                         .OrderByDescending(hv => hv.IDHocVien)
                         .ToList();

            return View(HVList);
        }

        [HttpGet("HVSearch")]
        public IActionResult Index(string input)
        {
            var HVList = _context.HocViens
                         .OrderByDescending(hv => hv.IDHocVien)
                         .ToList();
            var result = _context.HocViens.Where(item => item.HoTenHV != null && item.HoTenHV.Contains(input)).ToList();
            ViewData["HVInput"] = input;
            if (input != null)
            {
                return View(result);
            }
            else
            {
                return View(HVList);
            }
        }
        public IActionResult SuaXoa()
        {
            var HVList = _context.HocViens
                         .OrderByDescending(hv => hv.IDHocVien) // Sắp xếp theo GvienID
                         .ToList();

            return View(HVList);
        }

        [HttpGet("HVSearch1")]
        public IActionResult SuaXoa(string input)
        {
            var HVList = _context.HocViens
                         .OrderByDescending(hv => hv.IDHocVien) // Sắp xếp theo GvienID
                         .ToList();
            // Xử lý tìm kiếm
            var result = _context.HocViens.Where(item => item.HoTenHV != null && item.HoTenHV.Contains(input)).ToList();
            ViewData["HVInput"] = input; // Để giữ lại giá trị input sau khi tìm kiếm
            if (input != null)
            {
                return View(result);
            }
            else
            {
                return View(HVList);
            }
        }

        public IActionResult Details(long? id)
        {
            var HVList = _context.HocViens
                        .Where(hv => hv.IDHocVien == id)
                        .ToList();

            return View(HVList);
        }

        [HttpPost]
        public JsonResult CheckUnique(string field, string value, long? id)
        {
            bool isUnique = true;
            string message = "";

            // Thêm mới
            if (id == null)
            {
                // Kiểm tra tính duy nhất cho số điện thoại
                if (field == "SoDienThoai")
                {
                    if (_context.HocViens.Any(hv => hv.SoDienThoai == value))
                    {
                        isUnique = false;
                        message = "Số điện thoại này đã tồn tại.";
                    }
                }
                // Kiểm tra tính duy nhất cho email
                else if (field == "Email")
                {
                    if (_context.HocViens.Any(hv => hv.Email == value))
                    {
                        isUnique = false;
                        message = "Email này đã tồn tại.";
                    }
                }
            }
            // Cập nhật
            else
            {
                var currentHocVien = _context.HocViens.FirstOrDefault(hv => hv.IDHocVien == id);

                if (currentHocVien == null)
                {
                    isUnique = false;
                    message = "Học viên không tồn tại.";
                }
                else
                {
                    // Kiểm tra tính duy nhất cho số điện thoại
                    if (field == "SoDienThoai")
                    {
                        if (value != currentHocVien.SoDienThoai && _context.HocViens.Any(hv => hv.SoDienThoai == value))
                        {
                            isUnique = false;
                            message = "Số điện thoại này đã tồn tại.";
                        }
                    }
                    // Kiểm tra tính duy nhất cho email
                    else if (field == "Email")
                    {
                        if (value != currentHocVien.Email && _context.HocViens.Any(hv => hv.Email == value))
                        {
                            isUnique = false;
                            message = "Email này đã tồn tại.";
                        }
                    }
                }
            }

            return Json(new { isUnique, message });
        }



        public IActionResult Create()
        {
            var mnList = (from m in _context.Menus
                          select new SelectListItem()
                          {
                              Text = (m.Levels == 1) ? m.MenuName : "-- " + m.MenuName,
                              Value = m.MenuID.ToString()
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "--- select ---",
                Value = "0"
            });
            ViewBag.mnList = mnList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(tblHocVien hv)
        {
            if (ModelState.IsValid)
            {
                _context.HocViens.Add(hv);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hv);
        }

        public IActionResult Edit(long? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var hocvien = _context.HocViens.Find(id);
            if (hocvien == null)
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
            return View(hocvien);
        }

        [HttpPost]
        public IActionResult Edit(tblHocVien hv)
        {
            // Kiểm tra dữ liệu nhập vào có hợp lệ không
            if (ModelState.IsValid)
            {
                // Cập nhật giáo viên vào cơ sở dữ liệu
                _context.HocViens.Update(hv);
                _context.SaveChanges(); // Lưu vào cơ sở dữ liệu

                // Chuyển hướng về trang Index sau khi cập nhật thành công
                return RedirectToAction("Index");
            }

            // Nếu dữ liệu không hợp lệ, trả về View với Model hiện tại
            return View(hv);
        }


        public IActionResult Delete(long? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var hv = _context.HocViens.Find(id);
            if (hv == null)
                return NotFound();
            return View(hv);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var delHV = _context.HocViens.Find(id);
            if (delHV == null)
                return NotFound();
            _context.HocViens.Remove(delHV);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}