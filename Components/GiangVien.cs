using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using Microsoft.AspNetCore.Mvc;

namespace aznews.Components
{
    [ViewComponent(Name = "GiangVien")]
    public class GiangVien : ViewComponent
    {
        private readonly DataContext _context;

        public GiangVien(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listOfPost = (from p in _context.viewGiangViens
                              where (p.HoatDong == true)
                              orderby p.GvienID descending
                              select p).Take(5).ToList();
            return await Task.FromResult((IViewComponentResult)View("GiangVien", listOfPost));
        }
    }
}