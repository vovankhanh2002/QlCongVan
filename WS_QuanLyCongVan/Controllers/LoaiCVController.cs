using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace WS_QuanLyCongVan.Controllers
{
    public class LoaiCVController : Controller
    {
        public IUnitOfWork UnitOfWork;

        public LoaiCVController(IUnitOfWork UnitOfWork)
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
            var data = UnitOfWork.loaiVB.GetFlowRestore(i => i.TrangThai_Xoa == false, start, length, sortColumn, sortDirection);
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_LVB.ToLower().Contains(searchVal) || i.GhiChu.ToLower().Contains(searchVal));
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
                return View(new Tb_LoaiVB());
            }
            else
            {
                var loaiVB = UnitOfWork.loaiVB.GetById(i => i.ID == id);
                if (loaiVB == null)
                {
                    return NotFound();
                }
                return View(loaiVB);
            }
        }
        public async Task<IActionResult> AddOfEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Tb_LoaiVB());
            }
            else
            {
                var loaiVB = UnitOfWork.loaiVB.GetById(i => i.ID == id);
                if (loaiVB == null)
                {
                    return NotFound();
                }
                return View(loaiVB);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOfEdit(int id, Tb_LoaiVB tb_LoaiVB)

        {
            if (ModelState.IsValid)
            {
                if (tb_LoaiVB.ID == 0)
                {

                    UnitOfWork.loaiVB.Add(tb_LoaiVB);
                    UnitOfWork.Save();
                }
                else
                {
                    try
                    {
                        UnitOfWork.loaiVB.Update(tb_LoaiVB);
                        UnitOfWork.Save();
                        return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return Json(new { success = true, data = tb_LoaiVB, notify = "Bạn đã thêm thành công." });

            }
            return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "AddOfEdit", tb_LoaiVB) });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRangeConfirmed(List<int> lst)
        {
            try
            {
                var lstLoaiVB = UnitOfWork.loaiVB.GetAllWhere(i => lst.Contains(i.ID));
                foreach(var item in lstLoaiVB)
                {
                    item.TrangThai_Xoa = true;
                }
                UnitOfWork.loaiVB.UpdateRange(lstLoaiVB);
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
            var data = UnitOfWork.loaiVB.GetFlowRestore(i => i.TrangThai_Xoa==true, start, length, sortColumn, sortDirection);
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_LVB.ToLower().Contains(searchVal) || i.GhiChu.ToLower().Contains(searchVal));
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
                if(lst.Count() > 1)
                {
                    var lstLoaiVB = UnitOfWork.loaiVB.GetAllWhere(i => lst.Contains(i.ID));
                    foreach (var item in lstLoaiVB)
                    {
                        item.TrangThai_Xoa = false;
                    }
                    UnitOfWork.loaiVB.UpdateRange(lstLoaiVB);
                    UnitOfWork.Save();
                }
                else
                {
                    var lstLoaiVB = UnitOfWork.loaiVB.GetById(i => i.ID == lst[0]);
                    lstLoaiVB.TrangThai_Xoa = false;
                    UnitOfWork.loaiVB.Update(lstLoaiVB);
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
                var lstLoaiVB = UnitOfWork.loaiVB.GetAllWhere(i => lst.Contains(i.ID));
                UnitOfWork.loaiVB.DeleteRange(lstLoaiVB);
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
