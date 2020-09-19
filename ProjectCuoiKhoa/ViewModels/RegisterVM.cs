using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCuoiKhoa.ViewModels
{
    public class RegisterVM
    {
        [MaxLength(100)]
        [Required]
        [Display(Name ="Họ Tên")]
        public string HoTen { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name ="Số điện thoại")]
        public string SoDienThoai { get; set; }
        [MaxLength(100)]
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
        [Compare("MatKhau",ErrorMessage ="Mật khẩu không khớp")]
        public string NhapLaiMatKhau { get; set; }
        public string DiaChi { get; set; }
       
    }
}
