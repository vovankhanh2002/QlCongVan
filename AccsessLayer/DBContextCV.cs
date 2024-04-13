using DataLayer.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccsessLayer
{
    public class DBContextCV: IdentityDbContext
    {
        public DBContextCV(DbContextOptions<DBContextCV> options) : base(options)
        {

        }
        public DbSet<Tb_BoPhan> tb_BoPhans { get; set; }
        public DbSet<Tb_ChucVu> tb_ChucVus { get; set; }
        public DbSet<Tb_CVDEN> tb_CVDENs { get; set; }
        public DbSet<Tb_CVDI> tb_CVDIs { get; set; }
        public DbSet<Tb_DanhMucCV> tb_DanhMucCVs { get; set; }
        public DbSet<Tb_DMCV_CV> tb_DMCV_CVs { get; set; }
        public DbSet<Tb_Hop> tb_Hops { get; set; }
        public DbSet<Tb_Ke> tb_Kes { get; set; }
        public DbSet<Tb_Kho> tb_Khos { get; set; }
        public DbSet<Tb_LinhVuc> tb_LinhVuc { get; set; }
        public DbSet<Tb_LoaiSoCV> tb_LoaiSoCVs { get; set; }
        public DbSet<Tb_LoaiVB> tb_LoaiVBs { get; set; }
        public DbSet<Tb_MDKhan> tb_MDKhans { get; set; }
        public DbSet<Tb_MDMat> tb_MDMats { get; set; }
        public DbSet<Tb_NhanVien> tb_NhanViens { get; set; }
        public DbSet<Tb_PTNhan> tb_PTNhans { get; set; }
        public DbSet<Tb_SoCV> tb_SoCVs { get; set; }
        public DbSet<Tb_Nguoidung> tb_Nguoidungs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ánh xạ trường cờ soft delete
            modelBuilder.Entity<Tb_LoaiVB>().Property<bool>("TrangThai_Xoa");
            modelBuilder.Entity<Tb_LoaiSoCV>().Property<bool>("TrangThai_Xoa");
            modelBuilder.Entity<Tb_SoCV>().Property<bool>("TrangThai_Xoa");

            // Lọc mặc định để loại bỏ các bản ghi đã bị xóa logic
            //modelBuilder.Entity<Tb_LoaiVB>().HasQueryFilter(m => !EF.Property<bool>(m, "TrangThai_Xoa"));
        }
    }
}
