using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace DataLayer.Model
{
    public class Tb_MDKhan
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public string Ten_MDKhan { get; set; }
        public string? GhiChu { get; set; }
        public bool TrangThai_Xoa { get; set; } = false;

    }
}
