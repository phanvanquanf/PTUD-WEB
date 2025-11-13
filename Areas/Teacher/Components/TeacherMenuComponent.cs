using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using Microsoft.AspNetCore.Mvc;

namespace aznews.Areas.Teacher.Components
{
    [ViewComponent(Name = "TeacherMenu")]
    public class TeacherMenuComponent : ViewComponent
    {
        private readonly DataContext _context;

        public TeacherMenuComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mnList = (from mn in _context.TeacherMenus
                          where (mn.isActive == true)
                          select mn).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", mnList));
        }
    }
}