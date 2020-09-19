using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCuoiKhoa.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCuoiKhoa.ViewComponents
{
    public class CategoryMenu:ViewComponent
    {
        private readonly MyDbContext _context;

        public CategoryMenu(MyDbContext ctx)
        {
            _context = ctx;
        }
        public async Task<IViewComponentResult> InvokeAsync() 
        {
            return View(await _context.Loais.ToListAsync());
        }
    }
}
