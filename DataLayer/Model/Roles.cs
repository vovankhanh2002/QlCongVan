using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Roles
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin."),StringLength(256)]
        public string Name { get; set; }
    }
}
