using BusinessLayer.Handle;
using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace WS_QuanLyCongVan.Controllers
{
    public class NhanVienController : Controller
    {
        public IUnitOfWork UnitOfWork;

        public NhanVienController(IUnitOfWork UnitOfWork)
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
            var data = UnitOfWork.nhanVien.GetFlowRestore(i => i.TrangThai_Xoa == false, start, length, sortColumn, sortDirection, include: "Tb_BoPhan,Tb_ChucVu");
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Hoten_NV.ToLower().Contains(searchVal) || i.DiaChi_NV.ToLower().Contains(searchVal) || i.SDT_NV.ToString().ToLower().Contains(searchVal) || i.NgaySinh_NV.ToString().ToLower().Contains(searchVal) || i.Tb_ChucVu.Ten_CV.ToString().ToLower().Contains(searchVal) || i.GhiChu.ToString().ToLower().Contains(searchVal));
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
        public async Task<IActionResult> getById(string id)
        {
            if (id == null)
            {
                return View(new Tb_NhanVien());
            }
            else
            {
                //var loaiVB = UnitOfWork.nhanVien.GetById(i => i.Id == id);
                //if (loaiVB == null)
                //{
                //    return NotFound();
                //}
                return View(/*loaiVB*/);
            }
        }
        public async Task<IActionResult> AddOfEdit(int id = 0)
        {
            AllGetListItem getList = new AllGetListItem(UnitOfWork);
            ViewBag.getListCVu = getList.getChucvu();
            ViewBag.getListBP = getList.getBoPhan();
            if (id == 0)
            {
                return View(new Tb_NhanVien());
            }
            else
            {
                var nhanVien = UnitOfWork.nhanVien.GetById(i => i.Id == id);
                if (nhanVien == null)
                {
                    return NotFound();
                }
                return View(nhanVien);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOfEdit(int id, Tb_NhanVien Tb_NhanVien)
        {
            AllGetListItem getList = new AllGetListItem(UnitOfWork);
            ViewBag.getListCVu = getList.getChucvu();
            ViewBag.getListBP = getList.getBoPhan();
            if (ModelState.IsValid)
            {
                if (Tb_NhanVien.Id == 0)
                {
                    UnitOfWork.nhanVien.Add(Tb_NhanVien);
                    UnitOfWork.Save();
                }
                else
                {
                    try
                    {
                        UnitOfWork.nhanVien.Update(Tb_NhanVien);
                        UnitOfWork.Save();
                        return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return Json(new { success = true, data = Tb_NhanVien, notify = "Bạn đã thêm thành công." });

            }
            return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "AddOfEdit", Tb_NhanVien) });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRangeConfirmed(List<int> lst)
        {
            try
            {
                var lstnhanVien = UnitOfWork.nhanVien.GetAllWhere(i => lst.Contains(i.Id));
                foreach (var item in lstnhanVien)
                {
                    item.TrangThai_Xoa = true;
                }
                UnitOfWork.nhanVien.UpdateRange(lstnhanVien);
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
            var data = UnitOfWork.nhanVien.GetFlowRestore(i => i.TrangThai_Xoa == true, start, length, sortColumn, sortDirection, include: "Tb_PhongBan,Tb_ChucVu");
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Hoten_NV.ToLower().Contains(searchVal) || i.DiaChi_NV.ToLower().Contains(searchVal) || i.SDT_NV.ToString().ToLower().Contains(searchVal) || i.NgaySinh_NV.ToString().ToLower().Contains(searchVal) || i.Tb_ChucVu.Ten_CV.ToString().ToLower().Contains(searchVal) || i.GhiChu.ToString().ToLower().Contains(searchVal));
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
        public async Task<IActionResult> RestoreRangeConfirmed(List<string> lst)
        {
            try
            {
                if (lst.Count() > 1)
                {
                    //var lstnhanVien = UnitOfWork.nhanVien.GetAllWhere(i => lst.Contains(i.Id));
                    //foreach (var item in lstnhanVien)
                    //{
                    //    item.TrangThai_Xoa = false;
                    //}
                    //UnitOfWork.nhanVien.UpdateRange(lstnhanVien);
                    //UnitOfWork.Save();
                }
                else
                {
                    //var lstnhanVien = UnitOfWork.nhanVien.GetById(i => i.Id == lst[0]);
                    //lstnhanVien.TrangThai_Xoa = false;
                    //UnitOfWork.nhanVien.Update(lstnhanVien);
                    //UnitOfWork.Save();

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(new { isValue = true, notify = "Bạn đã phục hồi thành công" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRange(List<string> lst)
        {
            try
            {
                //var lstnhanVien = UnitOfWork.nhanVien.GetAllWhere(i => lst.Contains(i.Id));
                //UnitOfWork.nhanVien.DeleteRange(lstnhanVien);
                //UnitOfWork.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(new { isValue = true, notify = "Bạn đã xóa thành công" });
        }
    }
}
