using DataLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccsessLayer
{
    public class DBContextCV : IdentityDbContext<Tb_Nguoidung>
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
        public DbSet<Tb_Thongke> tb_Thongkes { get; set; }
        public DbSet<Tb_Thongbao> tb_Thongbaos { get; set; }
        public DbSet<Tb_Chat> tb_Chats { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Ánh xạ trường cờ soft delete
            builder.Entity<Tb_LoaiVB>().Property<bool>("TrangThai_Xoa");
            builder.Entity<Tb_LoaiSoCV>().Property<bool>("TrangThai_Xoa");
            builder.Entity<Tb_SoCV>().Property<bool>("TrangThai_Xoa");

            builder.Entity<Tb_Nguoidung>().ToTable("Users", "security");

            builder.Entity<IdentityRole>().ToTable("Roles", "security");

            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");

            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");

            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");

            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");

            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");

        }
    }
}
