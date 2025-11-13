using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;

namespace aznews.Components
{
    [ViewComponent(Name = "BaiGiang")]
    public class BaiGiang : ViewComponent
    {
        private readonly DataContext _context;
        public BaiGiang(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listOfPost = (from p in _context.viewBaiGiangs
                              where (p.HoatDong == true)
                              orderby p.IDBaiGiang descending
                              select p).Take(5).ToList();
            return await Task.FromResult((IViewComponentResult)View("BaiGiang", listOfPost));
        }
    }
}