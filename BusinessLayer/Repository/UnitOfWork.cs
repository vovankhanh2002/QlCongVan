using AccsessLayer;
using BusinessLayer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
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

        private readonly DBContextCV dBContextCV;
        public UnitOfWork(DBContextCV dBContextCV)
        {
            this.dBContextCV = dBContextCV;
            boPhan = new ReposBoPhan(dBContextCV);
            chucVu = new ReposChucVu(dBContextCV);
            coQuanBH = new ReposCoQuanBH(dBContextCV);
            cVDEN = new ReposCVDEN(dBContextCV);
            cVDI = new ReposCVDI(dBContextCV);
            danhMucCV = new ReposDanhMucCV(dBContextCV);
            dMCV_CV = new ReposDMCV_CV(dBContextCV);
            hop = new ReposHop(dBContextCV);
            ke = new ReposKe(dBContextCV);
            kho = new ReposKho(dBContextCV);
            linhVuc = new ReposLinhVuc(dBContextCV);
            loaiSoCV = new ReposLoaiSoCV(dBContextCV);
            loaiVB = new ReposLoaiVB(dBContextCV);
            mDKhan = new ReposMDKhan(dBContextCV);
            mDMat = new ReposMDMat(dBContextCV);
            nhanVien = new ReposNhanVien(dBContextCV);
            phongBan = new ReposPhongBan(dBContextCV);
            pTNhan = new ReposPTNhan(dBContextCV);
            soCV = new ReposSoCV(dBContextCV);
        }

        public void Save()
        {
            dBContextCV.SaveChanges();
        }
    }
}
