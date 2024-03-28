using BusinessLayer.Handle;
using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace WS_QuanLyCongVan.Controllers
{
    public class CVDENController : Controller
    {
        public IUnitOfWork UnitOfWork;

        public CVDENController(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> getAll()
        {
            var draw = Request.Form["draw"];
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            var searchVal = Request.Form["search[value]"];
            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortDirection = Request.Form["order[0][dir]"];
            var data = UnitOfWork.cVDEN.GetFlowRestore(i => i.TrangThai_Xoa == false, start, length, sortColumn, sortDirection, include: "Tb_LoaiVB,Tb_NhanVien,Tb_MDMat,Tb_MDKhan,tb_SoCV,Tb_LinhVuc,Tb_PhongBan");
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Skh_CVDEN.ToLower().Contains(searchVal) 
                || i.NgayBH_CVDEN.ToString().ToLower().Contains(searchVal) 
                || i.NgayNhan_CVDEN.ToString().ToString().ToLower().Contains(searchVal) 
                || i.SLTrang_CVDEN.ToString().ToLower().Contains(searchVal) 
                || i.SL_BPH.ToString().ToLower().Contains(searchVal) 
                || i.TrichYeu_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.HanTL_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.GhiChu_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.GhiChu_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.Tb_LoaiVB.Ten_LVB.ToString().ToLower().Contains(searchVal)
                || i.Tb_NhanVien.Hoten_NV.ToString().ToLower().Contains(searchVal)
                || i.Tb_MDMat.Ten_MDMat.ToString().ToLower().Contains(searchVal)
                || i.Tb_MDKhan.Ten_MDKhan.ToString().ToLower().Contains(searchVal)
                || i.tb_SoCV.Ten_SoCV.ToString().ToLower().Contains(searchVal)
                || i.Tb_LinhVuc.Ten_LV.ToString().ToLower().Contains(searchVal)
                || i.Tb_PhongBan.Ten_PB.ToString().ToLower().Contains(searchVal)
            );
            AllGetListItem getList = new AllGetListItem(UnitOfWork);
            var ite = getList.getNhanVien();
            var totalRecords = UnitOfWork.cVDEN.GetAllWhere(i => i.TrangThai_Xoa == false).Count();
            var totalFiltered = totalRecords;
            var jsonData = new
            {
                draw = draw,
                recordsFiltered = totalFiltered,
                recordsTotal = totalRecords,
                data
            };
            return Json(jsonData);
        }
        public async Task<IActionResult> getById(int id = 0)
        {
            if (id == 0)
            {
                return View(new Tb_CVDEN());
            }
            else
            {
                var loaiVB = UnitOfWork.cVDEN.GetById(i => i.ID == id);
                if (loaiVB == null)
                {
                    return NotFound();
                }
                return View(loaiVB);
            }
        }
        //[NoDirectAccess]
        public async Task<IActionResult> AddOfEdit(int id = 0)
        {
            //AllGetListItem getList = new AllGetListItem(UnitOfWork);
            //ViewBag.getListHop = getList.getHop();


            if (id == 0)
            {
                return View(new Tb_CVDEN());
            }
            else
            {
                var cVDEN = UnitOfWork.cVDEN.GetById(i => i.ID == id);
                if (cVDEN == null)
                {
                    return NotFound();
                }
                return View(cVDEN);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOfEdit(int id, Tb_CVDEN Tb_CVDEN)
        {
            //AllGetListItem getList = new AllGetListItem(UnitOfWork);
            //ViewBag.getListHop = getList.getHop();
            if (ModelState.IsValid)
            {
                if (Tb_CVDEN.ID == 0)
                {

                    UnitOfWork.cVDEN.Add(Tb_CVDEN);
                    UnitOfWork.Save();
                }
                else
                {
                    try
                    {
                        UnitOfWork.cVDEN.Update(Tb_CVDEN);
                        UnitOfWork.Save();
                        return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return Json(new { success = true, data = Tb_CVDEN, notify = "Bạn đã thêm thành công." });

            }
            return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "AddOfEdit", Tb_CVDEN) });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRangeConfirmed(List<int> lst)
        {
            try
            {
                var lstcVDEN = UnitOfWork.cVDEN.GetAllWhere(i => lst.Contains(i.ID));
                foreach (var item in lstcVDEN)
                {
                    item.TrangThai_Xoa = true;
                }
                UnitOfWork.cVDEN.UpdateRange(lstcVDEN);
                UnitOfWork.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(new { isValue = true, notify = "Bạn đã cho vào thùng rác thành công" });
        }

        public async Task<IActionResult> indexRestore()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> getRestore()
        {
            var draw = Request.Form["draw"];
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            var searchVal = Request.Form["search[value]"];
            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortDirection = Request.Form["order[0][dir]"];
            var totalRecords = UnitOfWork.cVDEN.GetAllWhere(i => i.TrangThai_Xoa == true).Count();
            var data = UnitOfWork.cVDEN.GetFlowRestore(i => i.TrangThai_Xoa == true, start, length, sortColumn, sortDirection, include: "Tb_Kho,Tb_Ke,Tb_Hop");
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Skh_CVDEN.ToLower().Contains(searchVal)
                || i.NgayBH_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.NgayNhan_CVDEN.ToString().ToString().ToLower().Contains(searchVal)
                || i.SLTrang_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.SL_BPH.ToString().ToLower().Contains(searchVal)
                || i.TrichYeu_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.HanTL_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.GhiChu_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.GhiChu_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.Tb_LoaiVB.Ten_LVB.ToString().ToLower().Contains(searchVal)
                || i.Tb_NhanVien.Hoten_NV.ToString().ToLower().Contains(searchVal)
                || i.Tb_MDMat.Ten_MDMat.ToString().ToLower().Contains(searchVal)
                || i.Tb_MDKhan.Ten_MDKhan.ToString().ToLower().Contains(searchVal)
                || i.tb_SoCV.Ten_SoCV.ToString().ToLower().Contains(searchVal)
                || i.Tb_LinhVuc.Ten_LV.ToString().ToLower().Contains(searchVal)
                || i.Tb_PhongBan.Ten_PB.ToString().ToLower().Contains(searchVal)
            );
            var totalFiltered = totalRecords;
            var jsonData = new
            {
                draw = draw,
                recordsFiltered = totalFiltered,
                recordsTotal = totalRecords,
                data
            };
            return Json(jsonData);
        }
        [HttpPost]
        public async Task<IActionResult> RestoreRangeConfirmed(List<int> lst)
        {
            try
            {
                if (lst.Count() > 1)
                {
                    var lstcVDEN = UnitOfWork.cVDEN.GetAllWhere(i => lst.Contains(i.ID));
                    foreach (var item in lstcVDEN)
                    {
                        item.TrangThai_Xoa = false;
                    }
                    UnitOfWork.cVDEN.UpdateRange(lstcVDEN);
                    UnitOfWork.Save();
                }
                else
                {
                    var lstcVDEN = UnitOfWork.cVDEN.GetById(i => i.ID == lst[0]);
                    lstcVDEN.TrangThai_Xoa = false;
                    UnitOfWork.cVDEN.Update(lstcVDEN);
                    UnitOfWork.Save();

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(new { isValue = true, notify = "Bạn đã phục hồi thành công" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRange(List<int> lst)
        {
            try
            {
                var lstcVDEN = UnitOfWork.cVDEN.GetAllWhere(i => lst.Contains(i.ID));
                UnitOfWork.cVDEN.DeleteRange(lstcVDEN);
                UnitOfWork.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(new { isValue = true, notify = "Bạn đã xóa thành công" });
        }
    }
}
