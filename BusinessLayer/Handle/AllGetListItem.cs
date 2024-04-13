using BusinessLayer.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Handle
{
    public class AllGetListItem
    {
        public IUnitOfWork UnitOfWork;

        public AllGetListItem(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }
        public IEnumerable<SelectListItem> getLoaiSoCV()
        {
            var listItem = UnitOfWork.loaiSoCV.GetAll().Select(i => new SelectListItem
            {
                Value = i.ID.ToString(),
                Text = i.Ten_LSCV
            });
            return listItem;
        }
        public IEnumerable<SelectListItem> getSoCV()
        {
            var listItem = UnitOfWork.soCV.GetAll().Select(i => new SelectListItem
            {
                Value = i.ID.ToString(),
                Text = i.Ten_SoCV
            });
            return listItem;
        }
        public IEnumerable<SelectListItem> getLoaiCV()
        {
            var listItem = UnitOfWork.loaiVB.GetAll().Select(i => new SelectListItem
            {
                Value = i.ID.ToString(),
                Text = i.Ten_LVB
            });
            return listItem;
        }
        public IEnumerable<SelectListItem> getKho()
        {
            var listItem = UnitOfWork.kho.GetAll().Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Ten_Kho
            });
            return listItem;
        }
        public IEnumerable<SelectListItem> getKe()
        {
            var listItem = UnitOfWork.ke.GetAll().Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Ten_Ke
            });
            return listItem;
        }
        public IEnumerable<SelectListItem> getHop()
        {
            var listItem = UnitOfWork.hop.GetAll().Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Ten_Hop
            });
            return listItem;
        }

        public IEnumerable<SelectListItem> getPhongban()
        {
            var listItem = UnitOfWork.phongBan.GetAll().Select(i => new SelectListItem
            {
                Value = i.ID.ToString(),
                Text = i.Ten_PB
            });
            return listItem;
        }
        public IEnumerable<SelectListItem> getChucvu()
        {
            var listItem = UnitOfWork.chucVu.GetAll().Select(i => new SelectListItem
            {
                Value = i.ID.ToString(),
                Text = i.Ten_CV
            });
            return listItem;
        }
        public IEnumerable<SelectListItem> getNhanVien()
        {
            var listItem = UnitOfWork.nhanVien.GetAll().Select(i => new SelectListItem
            {
                Value = i.Email_NV.ToString(),
                Text = i.Hoten_NV
            });
            return listItem;
        }
        public IEnumerable<SelectListItem> getNguoiDung()
        {
            var listItem = UnitOfWork.nguoidung.GetAll().Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Hoten_NV
            });
            return listItem;
        }

        public IEnumerable<SelectListItem> getBoPhan()
        {
            var listItem = UnitOfWork.boPhan.GetAll().Select(i => new SelectListItem
            {
                Value = i.ID.ToString(),
                Text = i.Ten_BP
            });
            return listItem;
        }
        public IEnumerable<SelectListItem> getBoPhanGui()
        {
            var listItem = UnitOfWork.boPhan.GetAll().Select(i => new SelectListItem
            {
                Value = i.Ten_BP.ToString(),
                Text = i.Ten_BP
            });
            return listItem;
        }

        public IEnumerable<SelectListItem> getDoMat()
        {
            var listItem = UnitOfWork.mDMat.GetAll().Select(i => new SelectListItem
            {
                Value = i.ID.ToString(),
                Text = i.Ten_MDMat
            });
            return listItem;
        }
        public IEnumerable<SelectListItem> getDoKhan()
        {
            var listItem = UnitOfWork.mDKhan.GetAll().Select(i => new SelectListItem
            {
                Value = i.ID.ToString(),
                Text = i.Ten_MDKhan
            });
            return listItem;
        }
        public IEnumerable<SelectListItem> getLinhVuc()
        {
            var listItem = UnitOfWork.linhVuc.GetAll().Select(i => new SelectListItem
            {
                Value = i.ID.ToString(),
                Text = i.Ten_LV
            });
            return listItem;
        }
    }
}
