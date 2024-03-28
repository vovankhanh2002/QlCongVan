using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace WS_QuanLyCongVan.Controllers
{
    public class KeController : Controller
    {
        public IUnitOfWork UnitOfWork;

        public KeController(IUnitOfWork UnitOfWork)
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
            var totalRecords = UnitOfWork.ke.GetAllWhere(i => i.TrangThai_Xoa == false).Count();
            var data = UnitOfWork.ke.GetFlowRestore(i => i.TrangThai_Xoa == false, start, length, sortColumn, sortDirection);
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_Ke.ToLower().Contains(searchVal) || i.GhiChu.ToLower().Contains(searchVal));
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
                return View(new Tb_Ke());
            }
            else
            {
                var ke = UnitOfWork.ke.GetById(i => i.Id == id);
                if (ke == null)
                {
                    return NotFound();
                }
                return View(ke);
            }
        }
        public async Task<IActionResult> AddOfEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Tb_Ke());
            }
            else
            {
                var ke = UnitOfWork.ke.GetById(i => i.Id == id);
                if (ke == null)
                {
                    return NotFound();
                }
                return View(ke);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOfEdit(int id, Tb_Ke tb_Ke)

        {
            if (ModelState.IsValid)
            {
                if (tb_Ke.Id == 0)
                {

                    UnitOfWork.ke.Add(tb_Ke);
                    UnitOfWork.Save();
                }
                else
                {
                    try
                    {
                        UnitOfWork.ke.Update(tb_Ke);
                        UnitOfWork.Save();
                        return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return Json(new { success = true, data = tb_Ke, notify = "Bạn đã thêm thành công." });

            }
            return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "AddOfEdit", tb_Ke) });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRangeConfirmed(List<int> lst)
        {
            try
            {
                var lstke = UnitOfWork.ke.GetAllWhere(i => lst.Contains(i.Id));
                foreach (var item in lstke)
                {
                    item.TrangThai_Xoa = true;
                }
                UnitOfWork.ke.UpdateRange(lstke);
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
            var totalRecords = UnitOfWork.ke.GetAllWhere(i => i.TrangThai_Xoa == true).Count();
            var data = UnitOfWork.ke.GetFlowRestore(i => i.TrangThai_Xoa == true, start, length, sortColumn, sortDirection);
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_Ke.ToLower().Contains(searchVal) || i.GhiChu.ToLower().Contains(searchVal));
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
                    var lstke = UnitOfWork.ke.GetAllWhere(i => lst.Contains(i.Id));
                    foreach (var item in lstke)
                    {
                        item.TrangThai_Xoa = false;
                    }
                    UnitOfWork.ke.UpdateRange(lstke);
                    UnitOfWork.Save();
                }
                else
                {
                    var lstke = UnitOfWork.ke.GetById(i => i.Id == lst[0]);
                    lstke.TrangThai_Xoa = false;
                    UnitOfWork.ke.Update(lstke);
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
                var lstke = UnitOfWork.ke.GetAllWhere(i => lst.Contains(i.Id));
                UnitOfWork.ke.DeleteRange(lstke);
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
