﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectCuoiKhoa.Data;
using ProjectCuoiKhoa.Helpers;
using ProjectCuoiKhoa.ViewModels;

namespace ProjectCuoiKhoa.Controllers
{
    public class GioHangController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public GioHangController(MyDbContext ctx, IMapper mapper)
        {
            _context = ctx;
            _mapper = mapper;
        }
        public List<CartItem> Carts
        {
            get
            {
                var carts = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (carts == null)
                {
                    carts = new List<CartItem>();
                }
                return carts;
            }
        }
        public IActionResult Index()
        {
            return View(Carts);
        }
        public IActionResult ThemVaoGio(Guid id, string addType, int qty = 1)
        {
            //lấy giỏ hàng hiện tại
            var myCart = Carts;

            //kiểm tra hàng đã có trong giỏ
            var item = myCart.SingleOrDefault(it => it.MaHangHoa == id);
            if (item != null)//đã có
            {
                item.SoLuong += qty;
            }
            else
            {
                var hh = _context.HangHoas.FirstOrDefault(p => p.MaHangHoa == id);
                item = _mapper.Map<CartItem>(hh);
                item.SoLuong = qty;
                myCart.Add(item);
            }
            HttpContext.Session.Set("GioHang", myCart);
            if (addType == "ajax")
            {
                return PartialView("_CartView");
            }
            return RedirectToAction("Index");
        }
        public IActionResult RemoveCartItem(Guid id, bool isAjaxCall = false)
        {
            //lấy giỏ hàng hiện tại
            var myCart = Carts;

            //kiểm tra hàng đã có trong giỏ
            var item = myCart.SingleOrDefault(it => it.MaHangHoa == id);
            if (item != null)
            {
                myCart.Remove(item);
                HttpContext.Session.Set("GioHang", myCart);
            }
            if (isAjaxCall)
            {

            }
            return RedirectToAction("Index");
        }
    }
}
