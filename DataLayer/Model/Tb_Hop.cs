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
    public class Tb_Hop
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]

        public string Ten_Hop { get; set; }
        public string? GhiChu { get; set; }
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
    }
}
