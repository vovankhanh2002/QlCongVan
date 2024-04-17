using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Handle;
using System.Diagnostics;

namespace WS_QuanLyCongVan.Controllers
{
    public class SoCVController : Controller
    {
        public IUnitOfWork UnitOfWork;

        public SoCVController(IUnitOfWork UnitOfWork)
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
            var data = UnitOfWork.soCV.GetFlowRestore(i => i.TrangThai_Xoa == false, start, length, sortColumn, sortDirection, include: "Tb_LoaiSoCV");
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_SoCV.ToLower().Contains(searchVal)
                || i.TrangThai.ToString().ToLower().Contains(searchVal)
                || i.Ngay_SoCV.ToString().ToLower().Contains(searchVal)
                );
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
                return View(new Tb_SoCV());
            }
            else
            {
                var loaiVB = UnitOfWork.soCV.GetById(i => i.ID == id);
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
            ViewBag.getListItem = getList.getLoaiSoCV();
            if (id == 0)
            {
                return View(new Tb_SoCV());
            }
            else
            {
                var soCV = UnitOfWork.soCV.GetById(i => i.ID == id);
                if (soCV == null)
                {
                    return NotFound();
                }
                return View(soCV);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOfEdit(int id, Tb_SoCV tb_SoCV)
        {
            if (ModelState.IsValid)
            {
                if (tb_SoCV.ID == 0)
                {

                    UnitOfWork.soCV.Add(tb_SoCV);
                    UnitOfWork.Save();
                }
                else
                {
                    try
                    {
                        UnitOfWork.soCV.Update(tb_SoCV);
                        UnitOfWork.Save();
                        return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return Json(new { success = true, data = tb_SoCV, notify = "Bạn đã thêm thành công." });

            }
            return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "AddOfEdit", tb_SoCV) });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRangeConfirmed(List<int> lst)
        {
            try
            {
                var lstSoCV = UnitOfWork.soCV.GetAllWhere(i => lst.Contains(i.ID));
                foreach (var item in lstSoCV)
                {
                    item.TrangThai_Xoa = true;
                }
                UnitOfWork.soCV.UpdateRange(lstSoCV);
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
            var data = UnitOfWork.soCV.GetFlowRestore(i => i.TrangThai_Xoa == true, start, length, sortColumn, sortDirection, include: "Tb_LoaiSoCV");
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_SoCV.ToLower().Contains(searchVal) || i.Ngay_SoCV.ToString().Contains(searchVal) || i.TrangThai.ToString().Contains(searchVal) || i.Ghichu.ToLower().Contains(searchVal));
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
                    var lstSoCV = UnitOfWork.soCV.GetAllWhere(i => lst.Contains(i.ID));
                    foreach (var item in lstSoCV)
                    {
                        item.TrangThai_Xoa = false;
                    }
                    UnitOfWork.soCV.UpdateRange(lstSoCV);
                    UnitOfWork.Save();
                }
                else
                {
                    var lstSoCV = UnitOfWork.soCV.GetById(i => i.ID == lst[0]);
                    lstSoCV.TrangThai_Xoa = false;
                    UnitOfWork.soCV.Update(lstSoCV);
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
                var lstSoCV = UnitOfWork.soCV.GetAllWhere(i => lst.Contains(i.ID));
                UnitOfWork.soCV.DeleteRange(lstSoCV);
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

