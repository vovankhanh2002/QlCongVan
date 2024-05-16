using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Tb_Nguoidung : IdentityUser
    {
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin."),MaxLength(100)]
        public string Hoten_NV { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public string? DiaChi_NV { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin."), MaxLength(10)]
        public int? SDT_NV { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public DateTime NgaySinh_NV { get; set; }
        public byte[]? Hinh { get; set; }
    }
}
