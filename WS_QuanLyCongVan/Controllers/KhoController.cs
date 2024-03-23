using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace WS_QuanLyCongVan.Controllers
{
    public class KhoController : Controller
    {
        public IUnitOfWork UnitOfWork;

        public KhoController(IUnitOfWork UnitOfWork)
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
            var totalRecords = UnitOfWork.kho.GetAllWhere(i => i.TrangThai_Xoa == false).Count();
            var data = UnitOfWork.kho.GetFlowRestore(i => i.TrangThai_Xoa == false, start, length, sortColumn, sortDirection);
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_Kho.ToLower().Contains(searchVal) || i.GhiChu.ToLower().Contains(searchVal));
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
                return View(new Tb_Kho());
            }
            else
            {
                var kho = UnitOfWork.kho.GetById(i => i.Id == id);
                if (kho == null)
                {
                    return NotFound();
                }
                return View(kho);
            }
        }
        public async Task<IActionResult> AddOfEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Tb_Kho());
            }
            else
            {
                var kho = UnitOfWork.kho.GetById(i => i.Id == id);
                if (kho == null)
                {
                    return NotFound();
                }
                return View(kho);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOfEdit(int id, Tb_Kho tb_Kho)

        {
            if (ModelState.IsValid)
            {
                if (tb_Kho.Id == 0)
                {

                    UnitOfWork.kho.Add(tb_Kho);
                    UnitOfWork.Save();
                }
                else
                {
                    try
                    {
                        UnitOfWork.kho.Update(tb_Kho);
                        UnitOfWork.Save();
                        return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return Json(new { success = true, data = tb_Kho, notify = "Bạn đã thêm thành công." });

            }
            return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "AddOfEdit", tb_Kho) });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRangeConfirmed(List<int> lst)
        {
            try
            {
                var lstkho = UnitOfWork.kho.GetAllWhere(i => lst.Contains(i.Id));
                foreach (var item in lstkho)
                {
                    item.TrangThai_Xoa = true;
                }
                UnitOfWork.kho.UpdateRange(lstkho);
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
            var totalRecords = UnitOfWork.kho.GetAllWhere(i => i.TrangThai_Xoa == false).Count();
            var data = UnitOfWork.kho.GetFlowRestore(i => i.TrangThai_Xoa == true, start, length, sortColumn, sortDirection);
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_Kho.ToLower().Contains(searchVal) || i.GhiChu.ToLower().Contains(searchVal));
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
                    var lstkho = UnitOfWork.kho.GetAllWhere(i => lst.Contains(i.Id));
                    foreach (var item in lstkho)
                    {
                        item.TrangThai_Xoa = false;
                    }
                    UnitOfWork.kho.UpdateRange(lstkho);
                    UnitOfWork.Save();
                }
                else
                {
                    var lstkho = UnitOfWork.kho.GetById(i => i.Id == lst[0]);
                    lstkho.TrangThai_Xoa = false;
                    UnitOfWork.kho.Update(lstkho);
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
                var lstkho = UnitOfWork.kho.GetAllWhere(i => lst.Contains(i.Id));
                UnitOfWork.kho.DeleteRange(lstkho);
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
