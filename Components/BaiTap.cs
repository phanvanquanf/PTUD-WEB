using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using Microsoft.AspNetCore.Mvc;

namespace aznews.Components
{
    [ViewComponent(Name = "BaiTap")]
    public class BaiTap : ViewComponent
    {
        private readonly DataContext _context;

        public BaiTap(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listOfPost = (from p in _context.viewBaiTaps
                              orderby p.IDBaiTap descending
                              select p).Take(12).ToList();
            return await Task.FromResult((IViewComponentResult)View("BaiTap", listOfPost));
        }
    }
}