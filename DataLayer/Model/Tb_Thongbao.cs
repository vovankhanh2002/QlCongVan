using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Tb_Thongbao
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Skh { get; set; }
        public string UserID { get; set; }
        public bool Cvden_di { get; set; }
        public string UserHandelID { get; set; }
        public string Noidung { get; set; }
        public DateTime Thoigian { get; set; }
        public string Hinh { get; set; }
        public bool Trangthai { get; set; }
    }
}
