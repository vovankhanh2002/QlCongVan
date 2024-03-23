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
    public class Tb_DanhMucCV
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]

        public string Ma_HS { get; set; }
        public string? Ten_HS { get; set; }
        public bool TrangThai_Xoa { get; set; } = false;

        public int? ID_Kho { get; set; }
        [ForeignKey("ID_Kho")]
        [ValidateNever]
        public Tb_Kho Tb_Kho { get; set; }
        //
        public int? ID_Ke { get; set; }
        [ForeignKey("ID_Ke")]
        [ValidateNever]
        public Tb_Ke Tb_Ke { get; set; }
        // 
        public int? ID_Hop { get; set; }
        [ForeignKey("ID_Hop")]
        [ValidateNever]
        public Tb_Hop Tb_Hop { get; set; }
        //
    }
}
