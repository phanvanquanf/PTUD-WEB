using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using Microsoft.AspNetCore.Mvc;

namespace aznews.Components
{
    [ViewComponent(Name = "DienDan")]
    public class DienDan : ViewComponent
    {
        private readonly DataContext _context;

        public DienDan(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listOfPost = (from p in _context.viewDienDans
                              where (p.HoatDong == true)
                              orderby p.IDBaiViet descending
                              select p).Take(12).ToList();
            return await Task.FromResult((IViewComponentResult)View("DienDan", listOfPost));
        }
    }
}