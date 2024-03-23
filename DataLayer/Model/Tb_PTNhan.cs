﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Tb_PTNhan
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
        public string Ten_PTNhan { get; set; }
        public string? GhiChu { get; set; }
        public bool TrangThai_Xoa { get; set; } = false;

    }
}
