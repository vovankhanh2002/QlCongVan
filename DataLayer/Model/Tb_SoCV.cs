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
    public class Tb_SoCV
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]

        public string Ten_SoCV { get; set; }
        public DateTime? Ngay_SoCV { get; set; }
        public string? Ghichu { get; set; }
        public bool TrangThai { get; set; }
        public bool TrangThai_Xoa { get; set; } = false;

        //
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public int ID_LSCV { get; set; }
        [ForeignKey("ID_LSCV")]
        [ValidateNever]
        public Tb_LoaiSoCV Tb_LoaiSoCV { get; set; }
        //
    }
}
