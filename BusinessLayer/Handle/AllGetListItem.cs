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
    }
}
