using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectCuoiKhoa.Data;
using ProjectCuoiKhoa.Helpers;
using ProjectCuoiKhoa.ViewModels;

namespace ProjectCuoiKhoa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HangHoaController : Controller
    {
        private readonly ILogger _logger;
        private readonly MyDbContext _context;

        public HangHoaController(MyDbContext context, ILogger<HangHoaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Admin/HangHoa
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.HangHoas.Include(h => h.Loai);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Admin/HangHoa/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoas
                .Include(h => h.Loai)
                .FirstOrDefaultAsync(m => m.MaHangHoa == id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
        }

        // GET: Admin/HangHoa/Create
        public IActionResult Create()
        {
            ViewBag.DanhsachLoai = new LoaiDropDownVM(_context.Loais, "MaLoai", "TenLoai", "MaLoai");
            return View();
        }

        // POST: Admin/HangHoa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHangHoa,TenHh,DonGia,GiamGia,SoLuong,Hinh,ChiTiet,MoTa,MaLoai,DiemReview")] HangHoa hangHoa, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var urlHinh = FileHelper.UploadFileToFolder(Hinh, "HangHoa");
                    hangHoa.Hinh = urlHinh;
                    _context.Add(hangHoa);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Loi:{ex.Message}");
                }
            }
            ViewBag.DanhsachLoai = new LoaiDropDownVM(_context.Loais, "MaLoai", "TenLoai", "MaLoai");
            return View();
        }

        // GET: Admin/HangHoa/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }
            ViewBag.DanhsachLoai = new LoaiDropDownVM(_context.Loais, "MaLoai", "TenLoai", "MaLoai",hangHoa.MaLoai);
            return View(hangHoa);
        }

        // POST: Admin/HangHoa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MaHangHoa,TenHh,DonGia,GiamGia,SoLuong,Hinh,ChiTiet,MoTa,MaLoai,DiemReview")] HangHoa hangHoa)
        {
            if (id != hangHoa.MaHangHoa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hangHoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HangHoaExists(hangHoa.MaHangHoa))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "MaLoai", hangHoa.MaLoai);
            return View(hangHoa);
        }

        // GET: Admin/HangHoa/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoas
                .Include(h => h.Loai)
                .FirstOrDefaultAsync(m => m.MaHangHoa == id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
        }

        // POST: Admin/HangHoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var hangHoa = await _context.HangHoas.FindAsync(id);
            _context.HangHoas.Remove(hangHoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HangHoaExists(Guid id)
        {
            return _context.HangHoas.Any(e => e.MaHangHoa == id);
        }
    }
}
