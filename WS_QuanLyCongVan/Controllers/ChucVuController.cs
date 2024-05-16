using BusinessLayer.Hubs;
using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace WS_QuanLyCongVan.Controllers
{

    public class ChucVuController : Controller
    {
        public IUnitOfWork UnitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ChucVuController(IUnitOfWork UnitOfWork,  IHttpContextAccessor httpContextAccessor)
        {
            this.UnitOfWork = UnitOfWork;
            _httpContextAccessor = httpContextAccessor;
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
            var data = UnitOfWork.chucVu.GetFlowRestore(i => i.TrangThai_Xoa == false, start, length, sortColumn, sortDirection);
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_CV.ToLower().Contains(searchVal));
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
                return View(new Tb_ChucVu());
            }
            else
            {
                var chucVu = UnitOfWork.chucVu.GetById(i => i.ID == id);
                if (chucVu == null)
                {
                    return NotFound();
                }
                return View(chucVu);
            }
        }
        public async Task<IActionResult> AddOfEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Tb_ChucVu());
            }
            else
            {
                var chucVu = UnitOfWork.chucVu.GetById(i => i.ID == id);
                if (chucVu == null)
                {
                    return NotFound();
                }
                return View(chucVu);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOfEdit(int id, Tb_ChucVu Tb_ChucVu)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                if (Tb_ChucVu.ID == 0)
                {

                    UnitOfWork.chucVu.Add(Tb_ChucVu);
                    UnitOfWork.Save();
                    // Gửi thông báo đến tất cả các tài khoản trừ tài khoản vừa được cập nhật hoặc thêm mới
                }
                else
                {
                    try
                    {
                        UnitOfWork.chucVu.Update(Tb_ChucVu);
                        UnitOfWork.Save();
                        return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return Json(new { success = true, data = Tb_ChucVu, notify = "Bạn đã thêm thành công." });

            }
            return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "AddOfEdit", Tb_ChucVu) });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRangeConfirmed(List<int> lst)
        {
            try
            {
                var lstchucVu = UnitOfWork.chucVu.GetAllWhere(i => lst.Contains(i.ID));
                foreach (var item in lstchucVu)
                {
                    item.TrangThai_Xoa = true;
                }
                UnitOfWork.chucVu.UpdateRange(lstchucVu);
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
            var data = UnitOfWork.chucVu.GetFlowRestore(i => i.TrangThai_Xoa == true, start, length, sortColumn, sortDirection);
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Ten_CV.ToLower().Contains(searchVal) || i.Ghichu.ToLower().Contains(searchVal));
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
                    var lstchucVu = UnitOfWork.chucVu.GetAllWhere(i => lst.Contains(i.ID));
                    foreach (var item in lstchucVu)
                    {
                        item.TrangThai_Xoa = false;
                    }
                    UnitOfWork.chucVu.UpdateRange(lstchucVu);
                    UnitOfWork.Save();
                }
                else
                {
                    var lstchucVu = UnitOfWork.chucVu.GetById(i => i.ID == lst[0]);
                    lstchucVu.TrangThai_Xoa = false;
                    UnitOfWork.chucVu.Update(lstchucVu);
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
                var lstchucVu = UnitOfWork.chucVu.GetAllWhere(i => lst.Contains(i.ID));
                UnitOfWork.chucVu.DeleteRange(lstchucVu);
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
