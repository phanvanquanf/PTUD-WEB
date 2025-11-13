using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using Microsoft.AspNetCore.Mvc;

namespace aznews.Components
{
    [ViewComponent(Name = "HocVien")]
    public class HocVien : ViewComponent
    {
        private readonly DataContext _context;

        public HocVien(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listOfPost = (from p in _context.viewHocViens
                              orderby p.IDHocVien descending
                              select p).Take(12).ToList();
            return await Task.FromResult((IViewComponentResult)View("HocVien", listOfPost));
        }
    }
}