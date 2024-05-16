using AccsessLayer;
using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
namespace WS_QuanLyCongVan.Controllers
{
    public class LoaiSoCVController : Controller
    {
        public IUnitOfWork UnitOfWork;

        public LoaiSoCVController(IUnitOfWork UnitOfWork)
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
            var searchVal = Request.Form["search[value]"].ToString().ToLower();
            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortDirection = Request.Form["order[0][dir]"];
            var data = UnitOfWork.loaiSoCV.GetFlowRestore(i => i.TrangThai_Xoa == false, start, length, sortColumn, sortDirection);
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_LSCV.ToLower().Contains(searchVal) || i.GhiChu.ToLower().Contains(searchVal));
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
                return View(new Tb_LoaiSoCV());
            }
            else
            {
                var loaiSoCV = UnitOfWork.loaiSoCV.GetById(i => i.ID == id);
                if (loaiSoCV == null)
                {
                    return NotFound();
                }
                return View(loaiSoCV);
            }
        }
        //[NoDirectAccess]
        public async Task<IActionResult> AddOfEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Tb_LoaiSoCV());
            }
            else
            {
                var loaiSoCV = UnitOfWork.loaiSoCV.GetById(i => i.ID == id);
                if (loaiSoCV == null)
                {
                    return NotFound();
                }
                return View(loaiSoCV);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOfEdit(int id, Tb_LoaiSoCV tb_LoaiSoCV)

        {
            if (ModelState.IsValid)
            {
                if (tb_LoaiSoCV.ID == 0)
                {

                    UnitOfWork.loaiSoCV.Add(tb_LoaiSoCV);
                    UnitOfWork.Save();
                }
                else
                {
                    try
                    {
                        UnitOfWork.loaiSoCV.Update(tb_LoaiSoCV);
                        UnitOfWork.Save();
                        return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return Json(new { success = true, data = tb_LoaiSoCV, notify = "Bạn đã thêm thành công.", action = "LoaiSoCV" });
                
            }
            return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "AddOfEdit", tb_LoaiSoCV) });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRangeConfirmed(List<int> lst)
        {
            try
            {
                var lstLoaiSoCV = UnitOfWork.loaiSoCV.GetAllWhere(i => lst.Contains(i.ID));
                foreach (var item in lstLoaiSoCV)
                {
                    item.TrangThai_Xoa = true;
                }
                UnitOfWork.loaiSoCV.UpdateRange(lstLoaiSoCV);
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
            var data = UnitOfWork.loaiSoCV.GetFlowRestore(i => i.TrangThai_Xoa == true,start,length,sortColumn,sortDirection);
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_LSCV.ToLower().Contains(searchVal) || i.GhiChu.ToLower().Contains(searchVal));
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
                    var lstLoaiSoCV = UnitOfWork.loaiSoCV.GetAllWhere(i => lst.Contains(i.ID));
                    foreach (var item in lstLoaiSoCV)
                    {
                        item.TrangThai_Xoa = false;
                    }
                    UnitOfWork.loaiSoCV.UpdateRange(lstLoaiSoCV);
                    UnitOfWork.Save();
                }
                else
                {
                    var lstLoaiSoCV = UnitOfWork.loaiSoCV.GetById(i => i.ID == lst[0]);
                    lstLoaiSoCV.TrangThai_Xoa = false;
                    UnitOfWork.loaiSoCV.Update(lstLoaiSoCV);
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
                var lstLoaiSoCV = UnitOfWork.loaiSoCV.GetAllWhere(i => lst.Contains(i.ID));
                UnitOfWork.loaiSoCV.DeleteRange(lstLoaiSoCV);
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
