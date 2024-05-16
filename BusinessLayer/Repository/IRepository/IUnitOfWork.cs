using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IBoPhan boPhan { get; set; }
        public IChucVu chucVu { get; set; }
        public ICoQuanBH coQuanBH { get; set; }
        public ICVDEN cVDEN { get; set; }
        public ICVDI cVDI { get; set; }
        public IDanhMucCV danhMucCV { get; set; }
        public IDMCV_CV dMCV_CV { get; set; }
        public IHop hop { get; set; }
        public IKe ke { get; set; }
        public IKho kho { get; set; }
        public ILinhVuc linhVuc { get; set; }
        public ILoaiSoCV loaiSoCV { get; set; }
        public ILoaiVB loaiVB { get; set; }
        public IMDKhan mDKhan { get; set; }
        public IMDMat mDMat { get; set; }
        public INhanVien nhanVien { get; set; }
        public IPhongBan phongBan { get; set; }
        public IPTNhan pTNhan { get; set; }
        public ISoCV soCV { get; set; }
        public INguoidung nguoidung { get; set; }
        public IThongke thongke { get; set; }
        public IThongbao thongbao { get; set; }
        public IChat chat { get; set; }
        void Save();
        Task BackupDatabase(string backupPath);
    }
}
