using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace WS_QuanLyCongVan.Controllers
{
    public class BoPhanController : Controller
    {
        public IUnitOfWork UnitOfWork;

        public BoPhanController(IUnitOfWork UnitOfWork)
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
            var totalRecords = UnitOfWork.boPhan.GetAllWhere(i => i.TrangThai_Xoa == false).Count();
            var data = UnitOfWork.boPhan.GetFlowRestore(i => i.TrangThai_Xoa == false, start, length, sortColumn, sortDirection);
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_BP.ToLower().Contains(searchVal) 
                || i.TenLD_BP.ToLower().Contains(searchVal)
                || i.SoNguoi_BP.ToString().ToLower().Contains(searchVal)
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
        public async Task<IActionResult> getById(int id = 0)
        {
            if (id == 0)
            {
                return View(new Tb_BoPhan());
            }
            else
            {
                var boPhan = UnitOfWork.boPhan.GetById(i => i.ID == id);
                if (boPhan == null)
                {
                    return NotFound();
                }
                return View(boPhan);
            }
        }
        public async Task<IActionResult> AddOfEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Tb_BoPhan());
            }
            else
            {
                var boPhan = UnitOfWork.boPhan.GetById(i => i.ID == id);
                if (boPhan == null)
                {
                    return NotFound();
                }
                return View(boPhan);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOfEdit(int id, Tb_BoPhan Tb_BoPhan)

        {
            if (ModelState.IsValid)
            {
                if (Tb_BoPhan.ID == 0)
                {

                    UnitOfWork.boPhan.Add(Tb_BoPhan);
                    UnitOfWork.Save();
                }
                else
                {
                    try
                    {
                        UnitOfWork.boPhan.Update(Tb_BoPhan);
                        UnitOfWork.Save();
                        return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return Json(new { success = true, data = Tb_BoPhan, notify = "Bạn đã thêm thành công." });

            }
            return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "AddOfEdit", Tb_BoPhan) });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRangeConfirmed(List<int> lst)
        {
            try
            {
                var lstboPhan = UnitOfWork.boPhan.GetAllWhere(i => lst.Contains(i.ID));
                foreach (var item in lstboPhan)
                {
                    item.TrangThai_Xoa = true;
                }
                UnitOfWork.boPhan.UpdateRange(lstboPhan);
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
            var totalRecords = UnitOfWork.boPhan.GetAllWhere(i => i.TrangThai_Xoa == true).Count();
            var data = UnitOfWork.boPhan.GetFlowRestore(i => i.TrangThai_Xoa == true, start, length, sortColumn, sortDirection);
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_BP.ToLower().Contains(searchVal)
                || i.TenLD_BP.ToLower().Contains(searchVal)
                || i.SoNguoi_BP.ToString().ToLower().Contains(searchVal)
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
                    var lstboPhan = UnitOfWork.boPhan.GetAllWhere(i => lst.Contains(i.ID));
                    foreach (var item in lstboPhan)
                    {
                        item.TrangThai_Xoa = false;
                    }
                    UnitOfWork.boPhan.UpdateRange(lstboPhan);
                    UnitOfWork.Save();
                }
                else
                {
                    var lstboPhan = UnitOfWork.boPhan.GetById(i => i.ID == lst[0]);
                    lstboPhan.TrangThai_Xoa = false;
                    UnitOfWork.boPhan.Update(lstboPhan);
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
                var lstboPhan = UnitOfWork.boPhan.GetAllWhere(i => lst.Contains(i.ID));
                UnitOfWork.boPhan.DeleteRange(lstboPhan);
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
