using BusinessLayer.Handle;
using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace WS_QuanLyCongVan.Controllers
{
    public class HopController : Controller
    {
        public IUnitOfWork UnitOfWork;

        public HopController(IUnitOfWork UnitOfWork)
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
            var data = UnitOfWork.hop.GetFlowRestore(i => i.TrangThai_Xoa == false, start, length, sortColumn, sortDirection, include: "Tb_Kho,Tb_Ke");
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_Hop.ToLower().Contains(searchVal) || i.GhiChu.ToLower().Contains(searchVal));
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
                return View(new Tb_Hop());
            }
            else
            {
                var hop = UnitOfWork.hop.GetById(i => i.Id == id);
                if (hop == null)
                {
                    return NotFound();
                }
                return View(hop);
            }
        }
        //[NoDirectAccess]
        public async Task<IActionResult> AddOfEdit(int id = 0)
        {
            AllGetListItem getList = new AllGetListItem(UnitOfWork);
            ViewBag.getListKho = getList.getKho();
            ViewBag.getListKe = getList.getKe();
            if (id == 0)
            {
                return View(new Tb_Hop());
            }
            else
            {
                var hop = UnitOfWork.hop.GetById(i => i.Id == id);
                if (hop == null)
                {
                    return NotFound();
                }
                return View(hop);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOfEdit(int id, Tb_Hop tb_Hop)
        {
            if (ModelState.IsValid)
            {
                if (tb_Hop.Id == 0)
                {

                    UnitOfWork.hop.Add(tb_Hop);
                    UnitOfWork.Save();
                }
                else
                {
                    try
                    {
                        UnitOfWork.hop.Update(tb_Hop);
                        UnitOfWork.Save();
                        return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return Json(new { success = true, data = tb_Hop, notify = "Bạn đã thêm thành công." });

            }
            return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "AddOfEdit", tb_Hop) });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRangeConfirmed(List<int> lst)
        {
            try
            {
                var lstHop = UnitOfWork.hop.GetAllWhere(i => lst.Contains(i.Id));
                foreach (var item in lstHop)
                {
                    item.TrangThai_Xoa = true;
                }
                UnitOfWork.hop.UpdateRange(lstHop);
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
            var data = UnitOfWork.hop.GetFlowRestore(i => i.TrangThai_Xoa == true, start, length, sortColumn, sortDirection, include: "Tb_Kho,Tb_Ke");
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_Hop.ToLower().Contains(searchVal) || i.GhiChu.ToLower().Contains(searchVal));
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
                    var lstHop = UnitOfWork.hop.GetAllWhere(i => lst.Contains(i.Id));
                    foreach (var item in lstHop)
                    {
                        item.TrangThai_Xoa = false;
                    }
                    UnitOfWork.hop.UpdateRange(lstHop);
                    UnitOfWork.Save();
                }
                else
                {
                    var lstHop = UnitOfWork.hop.GetById(i => i.Id == lst[0]);
                    lstHop.TrangThai_Xoa = false;
                    UnitOfWork.hop.Update(lstHop);
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
                var lstHop = UnitOfWork.hop.GetAllWhere(i => lst.Contains(i.Id));
                UnitOfWork.hop.DeleteRange(lstHop);
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
