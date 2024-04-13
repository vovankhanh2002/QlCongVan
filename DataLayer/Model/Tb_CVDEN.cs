using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Tb_CVDEN
    {
        [Key]
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public string Skh_CVDEN { get; set; } //Số ký hiệu 
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public DateTime NgayBH_CVDEN { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public DateTime NgayNhan_CVDEN { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public string Nguoigui_CVDEN { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public string Noigui_CVDEN { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public int SLTrang_CVDEN { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public int SL_BPH { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public string TrichYeu_CVDEN { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public DateTime HanTL_CVDEN { get; set; }
        public string? GhiChu_CVDEN { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public string? PhanCongXLVB_CVDEN { get; set; }
        public string? File_CVDEN { get; set; }
        public bool TrangThai_CVDI { get; set; }
        public bool TrangThai_Xoa { get; set; } = false;
        public DateTime ngay { get; set; }
        //
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public int ID_LVB { get; set; }
        [ForeignKey("ID_LVB")]
        [ValidateNever]
        public Tb_LoaiVB Tb_LoaiVB { get; set; }
        //
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public string ID_ND { get; set; }
        [ForeignKey("ID_ND")]
        [ValidateNever]
        public Tb_Nguoidung Tb_Nguoidung { get; set; }
        //
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public int ID_MDMat { get; set; }
        [ForeignKey("ID_MDMat")]
        [ValidateNever]
        public Tb_MDMat Tb_MDMat { get; set; }
        //
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public int ID_MDKhan { get; set; }
        [ForeignKey("ID_MDKhan")]
        [ValidateNever]
        public Tb_MDKhan Tb_MDKhan { get; set; }
        //
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public int ID_PTNhan { get; set; } = 1;
        [ForeignKey("ID_PTNhan")]
        [ValidateNever]
        public Tb_PTNhan Tb_PTNhan { get; set; }
        //
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public int ID_SoCV { get; set; }
        [ForeignKey("ID_SoCV")]
        [ValidateNever]
        public Tb_SoCV tb_SoCV { get; set; }
        //
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public int ID_LV { get; set; }
        [ForeignKey("ID_LV")]
        [ValidateNever]
        public Tb_LinhVuc Tb_LinhVuc { get; set; }
        //
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public int ID_BP { get; set; }
        [ForeignKey("ID_BP")]
        [ValidateNever]
        public Tb_BoPhan Tb_BoPhan { get; set; }
        //
    }
}
