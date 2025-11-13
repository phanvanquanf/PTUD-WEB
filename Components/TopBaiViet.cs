using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace aznews.Components
{
    [ViewComponent(Name = "TopBaiViet")]
    public class TopBaiViet : ViewComponent
    {
        private readonly DataContext _context;

        public TopBaiViet(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listOfPost = (from p in _context.viewTopBaiViets
                              where (p.HoatDong == true)
                              orderby p.IDBaiViet descending
                              select p).Take(5).ToList();
            return await Task.FromResult((IViewComponentResult)View("TopBaiViet", listOfPost));

        }
    }
}