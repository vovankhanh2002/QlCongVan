using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace WS_QuanLyCongVan.Controllers
{
    public class PhongBanController : Controller
    {
        public IUnitOfWork UnitOfWork;

        public PhongBanController(IUnitOfWork UnitOfWork)
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
            var data = UnitOfWork.phongBan.GetFlowRestore(i => i.TrangThai_Xoa == false, start, length, sortColumn, sortDirection);
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_PB.ToLower().Contains(searchVal) || i.GhiChu.ToLower().Contains(searchVal));
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
                return View(new Tb_PhongBan());
            }
            else
            {
                var phongBan = UnitOfWork.phongBan.GetById(i => i.ID == id);
                if (phongBan == null)
                {
                    return NotFound();
                }
                return View(phongBan);
            }
        }
        public async Task<IActionResult> AddOfEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Tb_PhongBan());
            }
            else
            {
                var phongBan = UnitOfWork.phongBan.GetById(i => i.ID == id);
                if (phongBan == null)
                {
                    return NotFound();
                }
                return View(phongBan);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOfEdit(int id, Tb_PhongBan Tb_PhongBan)

        {
            if (ModelState.IsValid)
            {
                if (Tb_PhongBan.ID == 0)
                {

                    UnitOfWork.phongBan.Add(Tb_PhongBan);
                    UnitOfWork.Save();
                }
                else
                {
                    try
                    {
                        UnitOfWork.phongBan.Update(Tb_PhongBan);
                        UnitOfWork.Save();
                        return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return Json(new { success = true, data = Tb_PhongBan, notify = "Bạn đã thêm thành công." });

            }
            return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "AddOfEdit", Tb_PhongBan) });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRangeConfirmed(List<int> lst)
        {
            try
            {
                var lstphongBan = UnitOfWork.phongBan.GetAllWhere(i => lst.Contains(i.ID));
                foreach (var item in lstphongBan)
                {
                    item.TrangThai_Xoa = true;
                }
                UnitOfWork.phongBan.UpdateRange(lstphongBan);
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
            var data = UnitOfWork.phongBan.GetFlowRestore(i => i.TrangThai_Xoa == true, start, length, sortColumn, sortDirection);
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_PB.ToLower().Contains(searchVal) || i.GhiChu.ToLower().Contains(searchVal));
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
                    var lstphongBan = UnitOfWork.phongBan.GetAllWhere(i => lst.Contains(i.ID));
                    foreach (var item in lstphongBan)
                    {
                        item.TrangThai_Xoa = false;
                    }
                    UnitOfWork.phongBan.UpdateRange(lstphongBan);
                    UnitOfWork.Save();
                }
                else
                {
                    var lstphongBan = UnitOfWork.phongBan.GetById(i => i.ID == lst[0]);
                    lstphongBan.TrangThai_Xoa = false;
                    UnitOfWork.phongBan.Update(lstphongBan);
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
                var lstphongBan = UnitOfWork.phongBan.GetAllWhere(i => lst.Contains(i.ID));
                UnitOfWork.phongBan.DeleteRange(lstphongBan);
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
