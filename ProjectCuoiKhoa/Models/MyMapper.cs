using AutoMapper;
using ProjectCuoiKhoa.Data;
using ProjectCuoiKhoa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCuoiKhoa.Models
{
    public class MyMapper:Profile
    {
        public MyMapper()
        {
            CreateMap<HangHoa, CartItem>();
            CreateMap<RegisterVM, KhachHang>();
        }
    }
}
