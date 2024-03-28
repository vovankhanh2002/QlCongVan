using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace WS_QuanLyCongVan.Controllers
{
    public class TinhKhanController : Controller
    {
        public IUnitOfWork UnitOfWork;

        public TinhKhanController(IUnitOfWork UnitOfWork)
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
            var totalRecords = UnitOfWork.mDKhan.GetAllWhere(i => i.TrangThai_Xoa == false).Count();
            var data = UnitOfWork.mDKhan.GetFlowRestore(i => i.TrangThai_Xoa == false, start, length, sortColumn, sortDirection);
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_MDKhan.ToLower().Contains(searchVal) || i.GhiChu.ToLower().Contains(searchVal));
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
                return View(new Tb_MDKhan());
            }
            else
            {
                var linhVuc = UnitOfWork.mDKhan.GetById(i => i.ID == id);
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
                return View(new Tb_MDKhan());
            }
            else
            {
                var mDKhan = UnitOfWork.mDKhan.GetById(i => i.ID == id);
                if (mDKhan == null)
                {
                    return NotFound();
                }
                return View(mDKhan);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOfEdit(int id, Tb_MDKhan tb_MDKhan)

        {
            if (ModelState.IsValid)
            {
                if (tb_MDKhan.ID == 0)
                {

                    UnitOfWork.mDKhan.Add(tb_MDKhan);
                    UnitOfWork.Save();
                }
                else
                {
                    try
                    {
                        UnitOfWork.mDKhan.Update(tb_MDKhan);
                        UnitOfWork.Save();
                        return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return Json(new { success = true, data = tb_MDKhan, notify = "Bạn đã thêm thành công." });

            }
            return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "AddOfEdit", tb_MDKhan) });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRangeConfirmed(List<int> lst)
        {
            try
            {
                var lstmDKhan = UnitOfWork.mDKhan.GetAllWhere(i => lst.Contains(i.ID));
                foreach (var item in lstmDKhan)
                {
                    item.TrangThai_Xoa = true;
                }
                UnitOfWork.mDKhan.UpdateRange(lstmDKhan);
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
            var totalRecords = UnitOfWork.mDKhan.GetAllWhere(i => i.TrangThai_Xoa == true).Count();
            var data = UnitOfWork.mDKhan.GetFlowRestore(i => i.TrangThai_Xoa == true, start, length, sortColumn, sortDirection);
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_MDKhan.ToLower().Contains(searchVal) || i.GhiChu.ToLower().Contains(searchVal));
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
                    var lstmDKhan = UnitOfWork.mDKhan.GetAllWhere(i => lst.Contains(i.ID));
                    foreach (var item in lstmDKhan)
                    {
                        item.TrangThai_Xoa = false;
                    }
                    UnitOfWork.mDKhan.UpdateRange(lstmDKhan);
                    UnitOfWork.Save();
                }
                else
                {
                    var lstmDKhan = UnitOfWork.mDKhan.GetById(i => i.ID == lst[0]);
                    lstmDKhan.TrangThai_Xoa = false;
                    UnitOfWork.mDKhan.Update(lstmDKhan);
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
                var lstmDKhan = UnitOfWork.mDKhan.GetAllWhere(i => lst.Contains(i.ID));
                UnitOfWork.mDKhan.DeleteRange(lstmDKhan);
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
