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
    [ViewComponent(Name = "TienTrinh")]
    public class TienTrinh : ViewComponent
    {
        private readonly DataContext _context;
        public TienTrinh(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listOfPost = (from p in _context.viewTienTrinhs
                              orderby p.IDTienTrinh descending
                              select p).Take(10).ToList();
            return await Task.FromResult((IViewComponentResult)View("TienTrinh", listOfPost));
        }
    }
}