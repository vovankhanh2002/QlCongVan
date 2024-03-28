using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace WS_QuanLyCongVan.Controllers
{
    public class LinhVucController : Controller
    {
        public IUnitOfWork UnitOfWork;

        public LinhVucController(IUnitOfWork UnitOfWork)
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
            var totalRecords = UnitOfWork.linhVuc.GetAllWhere(i => i.TrangThai_Xoa == false).Count();
            var data = UnitOfWork.linhVuc.GetFlowRestore(i => i.TrangThai_Xoa == false, start, length, sortColumn, sortDirection);
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_LV.ToLower().Contains(searchVal) || i.Ghichu.ToLower().Contains(searchVal));
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
                return View(new Tb_LinhVuc());
            }
            else
            {
                var linhVuc = UnitOfWork.linhVuc.GetById(i => i.ID == id);
                if (linhVuc == null)
                {
                    return NotFound();
                }
                return View(linhVuc);
            }
        }
        public async Task<IActionResult> AddOfEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Tb_LinhVuc());
            }
            else
            {
                var linhVuc = UnitOfWork.linhVuc.GetById(i => i.ID == id);
                if (linhVuc == null)
                {
                    return NotFound();
                }
                return View(linhVuc);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOfEdit(int id, Tb_LinhVuc tb_LinhVuc)

        {
            if (ModelState.IsValid)
            {
                if (tb_LinhVuc.ID == 0)
                {

                    UnitOfWork.linhVuc.Add(tb_LinhVuc);
                    UnitOfWork.Save();
                }
                else
                {
                    try
                    {
                        UnitOfWork.linhVuc.Update(tb_LinhVuc);
                        UnitOfWork.Save();
                        return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return Json(new { success = true, data = tb_LinhVuc, notify = "Bạn đã thêm thành công." });

            }
            return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "AddOfEdit", tb_LinhVuc) });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRangeConfirmed(List<int> lst)
        {
            try
            {
                var lstLinhVuc = UnitOfWork.linhVuc.GetAllWhere(i => lst.Contains(i.ID));
                foreach (var item in lstLinhVuc)
                {
                    item.TrangThai_Xoa = true;
                }
                UnitOfWork.linhVuc.UpdateRange(lstLinhVuc);
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
            var totalRecords = UnitOfWork.linhVuc.GetAllWhere(i => i.TrangThai_Xoa == true).Count();
            var data = UnitOfWork.linhVuc.GetFlowRestore(i => i.TrangThai_Xoa == true, start, length, sortColumn, sortDirection);
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_LV.ToLower().Contains(searchVal) || i.Ghichu.ToLower().Contains(searchVal));
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
                    var lstLinhVuc = UnitOfWork.linhVuc.GetAllWhere(i => lst.Contains(i.ID));
                    foreach (var item in lstLinhVuc)
                    {
                        item.TrangThai_Xoa = false;
                    }
                    UnitOfWork.linhVuc.UpdateRange(lstLinhVuc);
                    UnitOfWork.Save();
                }
                else
                {
                    var lstLinhVuc = UnitOfWork.linhVuc.GetById(i => i.ID == lst[0]);
                    lstLinhVuc.TrangThai_Xoa = false;
                    UnitOfWork.linhVuc.Update(lstLinhVuc);
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
                var lstLinhVuc = UnitOfWork.linhVuc.GetAllWhere(i => lst.Contains(i.ID));
                UnitOfWork.linhVuc.DeleteRange(lstLinhVuc);
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
