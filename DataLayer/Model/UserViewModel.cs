using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Hoten_NV { get; set; }
        public string? DiaChi_NV { get; set; }
        public int? SDT_NV { get; set; }
        public DateTime NgaySinh_NV { get; set; }
        public byte[] Hinh { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
