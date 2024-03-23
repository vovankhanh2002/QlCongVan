using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Tb_NhanVien
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public string Hoten_NV { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public string? DiaChi_NV { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public int? SDT_NV { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public DateTime NgaySinh_NV { get; set; }
        public bool TrangThai_Xoa { get; set; } = false;
        public string GhiChu { get; set; }
        //
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public int ID_ChucVu { get; set; }
        [ForeignKey("ID_ChucVu")]
        [ValidateNever]
        public Tb_ChucVu Tb_ChucVu { get; set; }
        //
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public int ID_PhongBan { get; set; }
        [ForeignKey("ID_PhongBan")]
        [ValidateNever]
        public Tb_PhongBan Tb_PhongBan { get; set; }
    }
}
