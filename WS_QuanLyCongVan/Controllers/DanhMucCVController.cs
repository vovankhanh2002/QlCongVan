using BusinessLayer.Handle;
using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace WS_QuanLyCongVan.Controllers
{
    public class DanhMucCVController : Controller
    {
        public IUnitOfWork UnitOfWork;

        public DanhMucCVController(IUnitOfWork UnitOfWork)
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
            var data = UnitOfWork.danhMucCV.GetFlowRestore(i => i.TrangThai_Xoa == false, start, length, sortColumn, sortDirection, include: "Tb_Kho,Tb_Ke,Tb_Hop");
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ma_HS.ToLower().Contains(searchVal) || i.Ten_HS.ToLower().Contains(searchVal) || i.Tb_Kho.Ten_Kho.ToString().ToLower().Contains(searchVal) || i.Tb_Ke.Ten_Ke.ToString().ToLower().Contains(searchVal) || i.Tb_Hop.Ten_Hop.ToString().ToLower().Contains(searchVal) || i.GhiChu.ToString().ToLower().Contains(searchVal));
            var totalRecords = data.Count();
            var totalFiltered = totalRecords;
            data = data.Skip(start).Take(length).ToList();
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
                return View(new Tb_DanhMucCV());
            }
            else
            {
                var loaiVB = UnitOfWork.danhMucCV.GetById(i => i.ID == id);
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
            AllGetListItem getList = new AllGetListItem(UnitOfWork);
            ViewBag.getListHop = getList.getHop();


            if (id == 0)
            {
                return View(new Tb_DanhMucCV());
            }
            else
            {
                var danhMucCV = UnitOfWork.danhMucCV.GetById(i => i.ID == id);
                if (danhMucCV == null)
                {
                    return NotFound();
                }
                return View(danhMucCV);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOfEdit(int id, Tb_DanhMucCV Tb_DanhMucCV)
        {
            AllGetListItem getList = new AllGetListItem(UnitOfWork);
            ViewBag.getListHop = getList.getHop();
            if (ModelState.IsValid)
            {
                if (Tb_DanhMucCV.ID == 0)
                {

                    UnitOfWork.danhMucCV.Add(Tb_DanhMucCV);
                    UnitOfWork.Save();
                }
                else
                {
                    try
                    {
                        UnitOfWork.danhMucCV.Update(Tb_DanhMucCV);
                        UnitOfWork.Save();
                        return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return Json(new { success = true, data = Tb_DanhMucCV, notify = "Bạn đã thêm thành công." });

            }
            return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "AddOfEdit", Tb_DanhMucCV) });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRangeConfirmed(List<int> lst)
        {
            try
            {
                var lstdanhMucCV = UnitOfWork.danhMucCV.GetAllWhere(i => lst.Contains(i.ID));
                foreach (var item in lstdanhMucCV)
                {
                    item.TrangThai_Xoa = true;
                }
                UnitOfWork.danhMucCV.UpdateRange(lstdanhMucCV);
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
            var data = UnitOfWork.danhMucCV.GetFlowRestore(i => i.TrangThai_Xoa == true, start, length, sortColumn, sortDirection, include: "Tb_Kho,Tb_Ke,Tb_Hop");
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ma_HS.ToLower().Contains(searchVal) || i.Ten_HS.ToLower().Contains(searchVal) || i.Tb_Kho.Ten_Kho.ToString().ToLower().Contains(searchVal) || i.Tb_Ke.Ten_Ke.ToString().ToLower().Contains(searchVal) || i.Tb_Hop.Ten_Hop.ToString().ToLower().Contains(searchVal) || i.GhiChu.ToString().ToLower().Contains(searchVal));
            var totalRecords = data.Count();
            var totalFiltered = totalRecords;
            data = data.Skip(start).Take(length).ToList();
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
                    var lstdanhMucCV = UnitOfWork.danhMucCV.GetAllWhere(i => lst.Contains(i.ID));
                    foreach (var item in lstdanhMucCV)
                    {
                        item.TrangThai_Xoa = false;
                    }
                    UnitOfWork.danhMucCV.UpdateRange(lstdanhMucCV);
                    UnitOfWork.Save();
                }
                else
                {
                    var lstdanhMucCV = UnitOfWork.danhMucCV.GetById(i => i.ID == lst[0]);
                    lstdanhMucCV.TrangThai_Xoa = false;
                    UnitOfWork.danhMucCV.Update(lstdanhMucCV);
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
                var lstdanhMucCV = UnitOfWork.danhMucCV.GetAllWhere(i => lst.Contains(i.ID));
                UnitOfWork.danhMucCV.DeleteRange(lstdanhMucCV);
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
