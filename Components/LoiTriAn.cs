using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using Microsoft.AspNetCore.Mvc;

namespace aznews.Components
{
    [ViewComponent(Name = "LoiTriAn")]
    public class LoiTriAn : ViewComponent
    {
        private readonly DataContext _context;

        public LoiTriAn(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listOfPost = (from p in _context.viewLoiTriAns
                              where (p.IsActive == true)
                              orderby p.PostID descending
                              select p).Take(4).ToList();
            return await Task.FromResult((IViewComponentResult)View("LoiTriAn", listOfPost));
        }
    }
}