using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class UpdateViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} phải dài ít nhất là {2} và tối đa {1} ký tự.", MinimumLength = 6)]
        [Display(Name = "Họ tên")]
        public string Hoten_NV { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập đầy đủ địa chỉ.")]
        public string? DiaChi_NV { get; set; }

        [Required]
        [Display(Name = "Số điện thoại")]
        public int? SDT_NV { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        [Display(Name = "Ngày sinh")]
        public DateTime NgaySinh_NV { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên người dùng")]
        public string UserName { get; set; }


    }
}
