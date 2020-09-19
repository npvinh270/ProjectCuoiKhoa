using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectCuoiKhoa.Data;
using ProjectCuoiKhoa.ViewModels;

namespace ProjectCuoiKhoa.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public KhachHangController(MyDbContext ctx, IMapper mapper)
        {
            _context = ctx;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DangKy(RegisterVM model)
        {
            return View();
        }
    }
}
