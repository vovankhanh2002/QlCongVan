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
    public class Tb_CVDI
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public string Skh_CVDI { get; set; } //Số ký hiệu 
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public DateTime NgayBH_CVDI { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public string TrichYeu_CVDI { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public int SL_BPH { get; set; } //Số lượng bản phát hành
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public string Noigui_CVDI { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public string Nguoinhan_CVDI { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public int SLTrang_CVDI { get; set; }
        public byte[]? File_CVDI { get; set; }
        public bool TrangThai_CVDI { get; set; }
        public bool TrangThai_Xoa { get; set; } = false;
        public DateTime ngay { get; set; }
        public string? GhiChu_CVDI { get; set; }

        //
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public int ID_LVB { get; set; }
        [ForeignKey("ID_LVB")]
        [ValidateNever]
        public Tb_LoaiVB Tb_LoaiVB { get; set; }
        //
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public int ID_NV { get; set; }
        [ForeignKey("ID_NV")]
        [ValidateNever]
        public Tb_NhanVien Tb_NhanVien { get; set; }
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
