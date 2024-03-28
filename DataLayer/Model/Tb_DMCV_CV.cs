using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Tb_DMCV_CV
    {
        public int ID { get; set; }
        public int? ID_DanhMucCV { get; set; }
        public bool TrangThai_Xoa { get; set; } = false;
        public string? GhiChu { get; set; }
        [ForeignKey("ID_DanhMucCV")]
        [ValidateNever]
        public Tb_DanhMucCV Tb_DanhMucCV { get; set; }
        //
        public int? ID_CVDEN { get; set; }
        [ForeignKey("ID_CVDEN")]
        [ValidateNever]
        public Tb_CVDEN Tb_CVDEN { get; set; }
        //
        public int? ID_CVDi { get; set; }
        [ForeignKey("ID_CVDi")]
        [ValidateNever]
        public Tb_CVDI Tb_CVDI { get; set; }
        //
    }
}
