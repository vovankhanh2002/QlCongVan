using BusinessLayer.Handle;
using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace WS_QuanLyCongVan.Controllers
{
    public class CVDIController : Controller
    {
        public IUnitOfWork UnitOfWork;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly Ikhanh _emailSender;
        public CVDIController(IUnitOfWork UnitOfWork, IWebHostEnvironment webHostEnvironment, Ikhanh emailSender)
        {
            this.UnitOfWork = UnitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
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
            var data = UnitOfWork.cVDI.GetFlowRestore(i => i.TrangThai_Xoa == false, start, length, sortColumn, sortDirection, include: "Tb_LoaiVB,Tb_MDMat,Tb_MDKhan,tb_SoCV,Tb_LinhVuc,Tb_Nguoidung");
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Skh_CVDI.ToLower().Contains(searchVal)
                || i.NgayBH_CVDI.ToString().ToLower().Contains(searchVal)
                || i.Nguoinhan_CVDI.ToString().ToString().ToLower().Contains(searchVal)
                || i.SLTrang_CVDI.ToString().ToLower().Contains(searchVal)
                || i.SL_BPH.ToString().ToLower().Contains(searchVal)
                || i.TrichYeu_CVDI.ToString().ToLower().Contains(searchVal)
                || i.Tb_LoaiVB.Ten_LVB.ToLower().Contains(searchVal)
                || i.Tb_Nguoidung.Hoten_NV.ToLower().Contains(searchVal)
                || i.Tb_MDMat.Ten_MDMat.ToLower().Contains(searchVal)
                || i.Tb_MDKhan.Ten_MDKhan.ToLower().Contains(searchVal)
                || i.tb_SoCV.Ten_SoCV.ToLower().Contains(searchVal)
                || i.Tb_LinhVuc.Ten_LV.ToLower().Contains(searchVal)
            );
            var totalRecords = UnitOfWork.cVDI.GetAllWhere(i => i.TrangThai_Xoa == false).Count();
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
        public async Task<IActionResult> AddOfEdit(int id = 0)
        {
            AllGetListItem getList = new AllGetListItem(UnitOfWork);
            ViewBag.getListSCV = getList.getSoCV();
            ViewBag.getListND = getList.getNguoiDung();
            ViewBag.getListDM = getList.getDoMat();
            ViewBag.getListDK = getList.getDoKhan();
            ViewBag.getListLCV = getList.getLoaiCV();
            ViewBag.getListLV = getList.getLinhVuc();
            ViewBag.getListBP = getList.getBoPhan();
            ViewBag.getListBPG = getList.getBoPhanGui();

            if (id == 0)
            {
                return View(new Tb_CVDI());
            }
            else
            {
                var cVDI = UnitOfWork.cVDI.GetById(i => i.ID == id);
                if (cVDI == null)
                {
                    return NotFound();
                }
                return View(cVDI);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOfEdit(Tb_CVDI Tb_CVDI, IFormFile File_CVDI)
        {
            var pathRoot = _webHostEnvironment.WebRootPath;
            DateTime currentDate = DateTime.Now;
            var upload = Path.Combine(pathRoot, @"assets\Cong_Van_Di\");
            var isCVDI = UnitOfWork.cVDI.GetById(i => i.ID == Tb_CVDI.ID);
            AllGetListItem getList = new AllGetListItem(UnitOfWork);
            ViewBag.getListSCV = getList.getSoCV();
            ViewBag.getListND = getList.getNguoiDung();
            ViewBag.getListDM = getList.getDoMat();
            ViewBag.getListDK = getList.getDoKhan();
            ViewBag.getListLCV = getList.getLoaiCV();
            ViewBag.getListLV = getList.getLinhVuc();
            ViewBag.getListBP = getList.getBoPhan();
            ViewBag.getListBPG = getList.getBoPhanGui();

            if (ModelState.IsValid)
            {
                if (Tb_CVDI.ID == 0)
                {
                    var extention = Path.Combine(File_CVDI.FileName);
                    var CVDI = new Tb_CVDI()
                    {
                        Skh_CVDI = Tb_CVDI.Skh_CVDI,
                        NgayBH_CVDI = Tb_CVDI.NgayBH_CVDI,
                        TrichYeu_CVDI = Tb_CVDI.TrichYeu_CVDI,
                        SL_BPH = Tb_CVDI.SL_BPH,
                        Noigui_CVDI = Tb_CVDI.Noigui_CVDI,
                        Nguoinhan_CVDI = Tb_CVDI.Nguoinhan_CVDI,
                        SLTrang_CVDI = Tb_CVDI.SLTrang_CVDI,
                        GhiChu_CVDI = Tb_CVDI.GhiChu_CVDI,
                        TrangThai_CVDI = Tb_CVDI.TrangThai_CVDI,
                        File_CVDI = extention,
                        ID_LVB = Tb_CVDI.ID_LVB,
                        ID_ND = Tb_CVDI.ID_ND,
                        ID_MDMat = Tb_CVDI.ID_MDMat,
                        ID_MDKhan = Tb_CVDI.ID_MDKhan,
                        ID_SoCV = Tb_CVDI.ID_SoCV,
                        ID_LV = Tb_CVDI.ID_LV,
                        ID_BP = Tb_CVDI.ID_BP,
                        TrangThai_Xoa = Tb_CVDI.TrangThai_Xoa,
                        ngay = DateTime.Now
                    };
                    // Tạo thư mục năm
                    string yearFolder = currentDate.ToString("yyyy");
                    string yearFolderPath = Path.Combine(upload, yearFolder);

                    // Tạo thư mục tháng
                    string monthFolder = currentDate.ToString("MM");
                    string monthFolderPath = Path.Combine(yearFolderPath, monthFolder);

                    // Tạo thư mục ngày
                    string dayFolder = currentDate.ToString("dd");
                    string dayFolderPath = Path.Combine(monthFolderPath, dayFolder);

                    // Kiểm tra và tạo thư mục năm
                    if (!Directory.Exists(yearFolderPath))
                    {
                        Directory.CreateDirectory(yearFolderPath);
                        Console.WriteLine("Thư mục năm đã được tạo: " + yearFolderPath);
                    }

                    // Kiểm tra và tạo thư mục tháng
                    if (!Directory.Exists(monthFolderPath))
                    {
                        Directory.CreateDirectory(monthFolderPath);
                        Console.WriteLine("Thư mục tháng đã được tạo: " + monthFolderPath);
                    }

                    // Kiểm tra và tạo thư mục ngày
                    if (!Directory.Exists(dayFolderPath))
                    {
                        Directory.CreateDirectory(dayFolderPath);
                        Console.WriteLine("Thư mục ngày đã được tạo: " + dayFolderPath);
                    }

                    using (var fileTream = new FileStream(Path.Combine(dayFolderPath, extention), FileMode.Create))
                    {
                        File_CVDI.CopyTo(fileTream);
                    }
                    UnitOfWork.cVDI.Add(CVDI);
                    UnitOfWork.Save();
                }
                else
                {
                    try
                    {
                        isCVDI.Skh_CVDI = Tb_CVDI.Skh_CVDI;
                        isCVDI.NgayBH_CVDI = Tb_CVDI.NgayBH_CVDI;
                        isCVDI.TrichYeu_CVDI = Tb_CVDI.TrichYeu_CVDI;
                        isCVDI.SL_BPH = Tb_CVDI.SL_BPH;
                        isCVDI.Noigui_CVDI = Tb_CVDI.Noigui_CVDI;
                        isCVDI.Nguoinhan_CVDI = Tb_CVDI.Nguoinhan_CVDI;
                        isCVDI.SLTrang_CVDI = Tb_CVDI.SLTrang_CVDI;
                        isCVDI.GhiChu_CVDI = Tb_CVDI.GhiChu_CVDI;
                        isCVDI.TrangThai_CVDI = Tb_CVDI.TrangThai_CVDI;
                        isCVDI.ID_LVB = Tb_CVDI.ID_LVB;
                        isCVDI.ID_ND = Tb_CVDI.ID_ND;
                        isCVDI.ID_MDMat = Tb_CVDI.ID_MDMat;
                        isCVDI.ID_MDKhan = Tb_CVDI.ID_MDKhan;
                        isCVDI.ID_SoCV = Tb_CVDI.ID_SoCV;
                        isCVDI.ID_LV = Tb_CVDI.ID_LV;
                        isCVDI.ID_BP = Tb_CVDI.ID_BP;
                        isCVDI.TrangThai_Xoa = Tb_CVDI.TrangThai_Xoa;
                        if (File_CVDI != null)
                        {
                            DateTime desiredDate = isCVDI.ngay;
                            var extention = Path.Combine(File_CVDI.FileName);
                            string yearFolder = desiredDate.ToString("yyyy");
                            string yearFolderPath = Path.Combine(upload, yearFolder);

                            string monthFolder = desiredDate.ToString("MM");
                            string monthFolderPath = Path.Combine(yearFolderPath, monthFolder);

                            string dayFolder = desiredDate.ToString("dd");
                            string dayFolderPath = Path.Combine(monthFolderPath, dayFolder);

                            var file = Path.Combine(dayFolderPath, isCVDI.File_CVDI);
                            if (System.IO.File.Exists(file))
                            {
                                System.IO.File.Delete(file);
                            }
                            using (var fileTream = new FileStream(Path.Combine(dayFolderPath, extention), FileMode.Create))
                            {
                                File_CVDI.CopyTo(fileTream);
                            }
                            isCVDI.File_CVDI = extention;
                        }

                        UnitOfWork.cVDI.Update(isCVDI);
                        UnitOfWork.Save();
                        return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return Json(new { success = true, data = Tb_CVDI, notify = "Bạn đã thêm thành công." });

            }
            return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "AddOfEdit", Tb_CVDI) });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRangeConfirmed(List<int> lst)
        {
            try
            {
                var lstcVDI = UnitOfWork.cVDI.GetAllWhere(i => lst.Contains(i.ID));
                foreach (var item in lstcVDI)
                {
                    item.TrangThai_Xoa = true;
                }
                UnitOfWork.cVDI.UpdateRange(lstcVDI);
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
            var searchVal = Request.Form["search[value]"].ToString().ToLower();
            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortDirection = Request.Form["order[0][dir]"];
            var data = UnitOfWork.cVDI.GetFlowRestore(i => i.TrangThai_Xoa == true, start, length, sortColumn, sortDirection, include: "Tb_LoaiVB,Tb_MDMat,Tb_MDKhan,tb_SoCV,Tb_LinhVuc,Tb_Nguoidung");
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Skh_CVDI.ToLower().Contains(searchVal)
                || i.NgayBH_CVDI.ToString().ToLower().Contains(searchVal)
                || i.Nguoinhan_CVDI.ToString().ToString().ToLower().Contains(searchVal)
                || i.SLTrang_CVDI.ToString().ToLower().Contains(searchVal)
                || i.SL_BPH.ToString().ToLower().Contains(searchVal)
                || i.TrichYeu_CVDI.ToString().ToLower().Contains(searchVal)
                || i.Tb_LoaiVB.Ten_LVB.ToLower().Contains(searchVal)
                || i.Tb_Nguoidung.Hoten_NV.ToLower().Contains(searchVal)
                || i.Tb_MDMat.Ten_MDMat.ToLower().Contains(searchVal)
                || i.Tb_MDKhan.Ten_MDKhan.ToLower().Contains(searchVal)
                || i.tb_SoCV.Ten_SoCV.ToLower().Contains(searchVal)
                || i.Tb_LinhVuc.Ten_LV.ToLower().Contains(searchVal)
            );
            var totalRecords = UnitOfWork.cVDI.GetAllWhere(i => i.TrangThai_Xoa == true).Count();
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
                    var lstcVDI = UnitOfWork.cVDI.GetAllWhere(i => lst.Contains(i.ID));
                    foreach (var item in lstcVDI)
                    {
                        item.TrangThai_Xoa = false;
                    }
                    UnitOfWork.cVDI.UpdateRange(lstcVDI);
                    UnitOfWork.Save();
                }
                else
                {
                    var lstcVDI = UnitOfWork.cVDI.GetById(i => i.ID == lst[0]);
                    lstcVDI.TrangThai_Xoa = false;
                    UnitOfWork.cVDI.Update(lstcVDI);
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
            var pathRoot = _webHostEnvironment.WebRootPath;
            var upload = Path.Combine(pathRoot, @"assets\Cong_Van_Di\");
            try
            {
                var lstcVDI = UnitOfWork.cVDI.GetAllWhere(i => lst.Contains(i.ID));
                foreach (var item in lstcVDI)
                {
                    DateTime desiredDate = item.ngay;
                    string yearFolder = desiredDate.ToString("yyyy");
                    string yearFolderPath = Path.Combine(upload, yearFolder);

                    string monthFolder = desiredDate.ToString("MM");
                    string monthFolderPath = Path.Combine(yearFolderPath, monthFolder);

                    string dayFolder = desiredDate.ToString("dd");
                    string dayFolderPath = Path.Combine(monthFolderPath, dayFolder);

                    var file = Path.Combine(dayFolderPath, item.File_CVDI);
                    if (System.IO.File.Exists(file))
                    {
                        System.IO.File.Delete(file);
                    }
                }
                UnitOfWork.cVDI.DeleteRange(lstcVDI);
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
