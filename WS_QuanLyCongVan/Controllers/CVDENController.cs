using BanDoWeb.Model.Models;
using BusinessLayer.Handle;
using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace WS_QuanLyCongVan.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class CVDENController : Controller
    {
        public IUnitOfWork UnitOfWork;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly Ikhanh _emailSender;
        public CVDENController(IUnitOfWork UnitOfWork, IWebHostEnvironment webHostEnvironment, Ikhanh emailSender)
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
            var data = UnitOfWork.cVDEN.GetFlowRestore(i => i.TrangThai_Xoa == false, start, length, sortColumn, sortDirection, include: "Tb_LoaiVB,Tb_MDMat,Tb_MDKhan,tb_SoCV,Tb_LinhVuc,Tb_PTNhan,Tb_Nguoidung");
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Skh_CVDEN.ToLower().Contains(searchVal) 
                || i.NgayBH_CVDEN.ToString().ToLower().Contains(searchVal) 
                || i.NgayNhan_CVDEN.ToString().ToString().ToLower().Contains(searchVal) 
                || i.SLTrang_CVDEN.ToString().ToLower().Contains(searchVal) 
                || i.SL_BPH.ToString().ToLower().Contains(searchVal) 
                || i.TrichYeu_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.HanTL_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.Tb_LoaiVB.Ten_LVB.ToLower().Contains(searchVal)
                || i.Tb_Nguoidung.Hoten_NV.ToLower().Contains(searchVal)
                || i.Tb_MDMat.Ten_MDMat.ToLower().Contains(searchVal)
                || i.Tb_MDKhan.Ten_MDKhan.ToLower().Contains(searchVal)
                || i.tb_SoCV.Ten_SoCV.ToLower().Contains(searchVal)
                || i.Tb_LinhVuc.Ten_LV.ToLower().Contains(searchVal)
            );
            var totalRecords = UnitOfWork.cVDEN.GetAllWhere(i => i.TrangThai_Xoa == false).Count();
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

            AllGetListItem getList = new AllGetListItem(UnitOfWork);
            ViewBag.getListSCV = getList.getSoCV();
            ViewBag.getListND = getList.getNguoiDung();
            ViewBag.getListDM = getList.getDoMat();
            ViewBag.getListDK = getList.getDoKhan();
            ViewBag.getListLCV = getList.getLoaiCV();
            ViewBag.getListLV = getList.getLinhVuc();
            ViewBag.getListBP = getList.getBoPhan();
            ViewBag.getListBPG = getList.getBoPhanGui();
            ViewBag.getListNV = getList.getNhanVien();
            if (id == 0)
            {
                return View(new Tb_CVDEN());
            }
            else
            {
                var cVDEN = UnitOfWork.cVDEN.GetById(i => i.ID == id);
                if (cVDEN == null)
                {
                    return NotFound();
                }
                return View(cVDEN);
            }
        }
        [HttpPost]
        public async Task<IActionResult> getById(int id = 0,string tieude = null, List<string> lstMail = null)
        {
            if (id == 0)
            {
                return View(new Tb_CVDEN());
            }
            else
            {
                if(lstMail != null)
                {
                    var pathRoot = _webHostEnvironment.WebRootPath;
                    var cVDEN = UnitOfWork.cVDEN.GetById(i => i.ID == id);
                    var upload = Path.Combine(pathRoot, @"assets\CongVan\" + cVDEN.File_CVDEN);
                    string message = "";
                    message += cVDEN.Skh_CVDEN.ToString();
                    await _emailSender.SendEmailCV(lstMail, tieude, message, upload);
                    var jsonData = new
                    {
                        sussess = true,
                        notify = "Đã gửi mail thành công."
                    };
                    return Json(jsonData);
                }
                return Json(new { sussess = false, notify = "Đã gửi mail không thành công." });
            }
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
                return View(new Tb_CVDEN());
            }
            else
            {
                var cVDEN = UnitOfWork.cVDEN.GetById(i => i.ID == id);
                if (cVDEN == null)
                {
                    return NotFound();
                }
                return View(cVDEN);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOfEdit(Tb_CVDEN Tb_CVDEN, IFormFile File_CVDEN)
        {
            var pathRoot = _webHostEnvironment.WebRootPath;
            DateTime currentDate = DateTime.Now;
            var upload = Path.Combine(pathRoot, @"assets\Cong_Van_Den\");
            var isCvDen = UnitOfWork.cVDEN.GetById(i => i.ID == Tb_CVDEN.ID);
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
                if (Tb_CVDEN.ID == 0)
                {
                    if(File_CVDEN != null)
                    {
                        var extention = Path.Combine(File_CVDEN.FileName);
                        var cvden = new Tb_CVDEN()
                        {
                            Skh_CVDEN = Tb_CVDEN.Skh_CVDEN,
                            NgayBH_CVDEN = Tb_CVDEN.NgayBH_CVDEN,
                            NgayNhan_CVDEN = Tb_CVDEN.NgayNhan_CVDEN,
                            TrichYeu_CVDEN = Tb_CVDEN.TrichYeu_CVDEN,
                            HanTL_CVDEN = Tb_CVDEN.HanTL_CVDEN,
                            GhiChu_CVDEN = Tb_CVDEN.GhiChu_CVDEN,
                            Nguoigui_CVDEN = Tb_CVDEN.Nguoigui_CVDEN,
                            Noigui_CVDEN = Tb_CVDEN.Noigui_CVDEN,
                            PhanCongXLVB_CVDEN = Tb_CVDEN.PhanCongXLVB_CVDEN,
                            TrangThai_CVDI = Tb_CVDEN.TrangThai_CVDI,
                            File_CVDEN = extention,
                            ID_LVB = Tb_CVDEN.ID_LVB,
                            ID_ND = Tb_CVDEN.ID_ND,
                            ID_MDMat = Tb_CVDEN.ID_MDMat,
                            ID_MDKhan = Tb_CVDEN.ID_MDKhan,
                            ID_PTNhan = Tb_CVDEN.ID_PTNhan,
                            ID_SoCV = Tb_CVDEN.ID_SoCV,
                            ID_LV = Tb_CVDEN.ID_LV,
                            ID_BP = Tb_CVDEN.ID_BP,
                            SLTrang_CVDEN = Tb_CVDEN.SLTrang_CVDEN,
                            SL_BPH = Tb_CVDEN.SL_BPH,
                            TrangThai_Xoa = Tb_CVDEN.TrangThai_Xoa,
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
                            File_CVDEN.CopyTo(fileTream);
                        }
                        UnitOfWork.cVDEN.Add(cvden);
                        UnitOfWork.Save();
                    }
                    else
                    {
                        return Json(new { success = false, notify = "Bạn chưa chọn file." });
                    }
                }
                else
                {
                    try
                    {
                        isCvDen.Skh_CVDEN = Tb_CVDEN.Skh_CVDEN;
                        isCvDen.NgayBH_CVDEN = Tb_CVDEN.NgayBH_CVDEN;
                        isCvDen.NgayNhan_CVDEN = Tb_CVDEN.NgayNhan_CVDEN;
                        isCvDen.TrichYeu_CVDEN = Tb_CVDEN.TrichYeu_CVDEN;
                        isCvDen.HanTL_CVDEN = Tb_CVDEN.HanTL_CVDEN;
                        isCvDen.GhiChu_CVDEN = Tb_CVDEN.GhiChu_CVDEN;
                        isCvDen.Nguoigui_CVDEN = Tb_CVDEN.Nguoigui_CVDEN;
                        isCvDen.Noigui_CVDEN = Tb_CVDEN.Noigui_CVDEN;
                        isCvDen.PhanCongXLVB_CVDEN = Tb_CVDEN.PhanCongXLVB_CVDEN;
                        isCvDen.TrangThai_CVDI = Tb_CVDEN.TrangThai_CVDI;
                        isCvDen.ID_LVB = Tb_CVDEN.ID_LVB;
                        isCvDen.ID_ND = Tb_CVDEN.ID_ND;
                        isCvDen.ID_MDMat = Tb_CVDEN.ID_MDMat;
                        isCvDen.ID_MDKhan = Tb_CVDEN.ID_MDKhan;
                        isCvDen.ID_PTNhan = Tb_CVDEN.ID_PTNhan;
                        isCvDen.ID_SoCV = Tb_CVDEN.ID_SoCV;
                        isCvDen.ID_LV = Tb_CVDEN.ID_LV;
                        isCvDen.ID_BP = Tb_CVDEN.ID_BP;
                        isCvDen.SLTrang_CVDEN = Tb_CVDEN.SLTrang_CVDEN;
                        isCvDen.SL_BPH = Tb_CVDEN.SL_BPH;
                        isCvDen.TrangThai_Xoa = Tb_CVDEN.TrangThai_Xoa;
                        if (File_CVDEN != null)
                        {
                            DateTime desiredDate = isCvDen.ngay;
                            var extention = Path.Combine(File_CVDEN.FileName);
                            string yearFolder = desiredDate.ToString("yyyy");
                            string yearFolderPath = Path.Combine(upload, yearFolder);

                            string monthFolder = desiredDate.ToString("MM");
                            string monthFolderPath = Path.Combine(yearFolderPath, monthFolder);

                            string dayFolder = desiredDate.ToString("dd");
                            string dayFolderPath = Path.Combine(monthFolderPath, dayFolder);

                            var file = Path.Combine(dayFolderPath, isCvDen.File_CVDEN);
                            if (System.IO.File.Exists(file))
                            {
                                System.IO.File.Delete(file);
                            }
                            using (var fileTream = new FileStream(Path.Combine(dayFolderPath, extention), FileMode.Create))
                            {
                                File_CVDEN.CopyTo(fileTream);
                            }
                            isCvDen.File_CVDEN = extention;
                        }
                        
                        UnitOfWork.cVDEN.Update(isCvDen);
                        UnitOfWork.Save();
                        return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return Json(new { success = true, data = Tb_CVDEN, notify = "Bạn đã thêm thành công." });

            }
            return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "AddOfEdit", Tb_CVDEN) });
        }


        public async Task<IActionResult> ImportExcel()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile excelFile)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var pathRoot = _webHostEnvironment.WebRootPath;
            var upload = Path.Combine(pathRoot, @"assets\Cong_Van_Den\");
            var listExcel = new List<Tb_CVDEN>();
            if (excelFile == null || excelFile.Length <= 0)
            {
                return Json(new { success = false, notify = "Bạn chưa thêm excel hoặc đường dẫn cũ." });
            }
            using (var stream = new MemoryStream())
            {
                excelFile.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    for (int row = worksheet.Dimension.Start.Row + 1; row <= worksheet.Dimension.End.Row; row++)
                    {
                        listExcel.Add(new Tb_CVDEN
                        {
                            Skh_CVDEN = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            NgayBH_CVDEN = Convert.ToDateTime(worksheet.Cells[row, 2].Value),
                            NgayNhan_CVDEN = Convert.ToDateTime(worksheet.Cells[row, 3].Value),
                            SLTrang_CVDEN = Convert.ToInt32(worksheet.Cells[row, 4].Value),
                            SL_BPH = Convert.ToInt32(worksheet.Cells[row, 5].Value),
                            TrichYeu_CVDEN = worksheet.Cells[row, 6].Value.ToString().Trim(),
                            HanTL_CVDEN = Convert.ToDateTime(worksheet.Cells[row, 7].Value),
                            GhiChu_CVDEN = worksheet.Cells[row, 8].Value.ToString().Trim(),
                            PhanCongXLVB_CVDEN = worksheet.Cells[row, 9].Value.ToString().Trim(),
                            File_CVDEN =  worksheet.Cells[row, 10].Value.ToString().Trim(),
                            TrangThai_CVDI = Convert.ToBoolean(worksheet.Cells[row, 11].Value),
                            TrangThai_Xoa = Convert.ToBoolean(worksheet.Cells[row, 12].Value),
                            ngay = Convert.ToDateTime(worksheet.Cells[row, 13].Value),
                            ID_LVB = Convert.ToInt32(worksheet.Cells[row, 14].Value),
                            ID_MDMat = Convert.ToInt32(worksheet.Cells[row, 15].Value),
                            ID_MDKhan = Convert.ToInt32(worksheet.Cells[row, 16].Value),
                            ID_PTNhan = Convert.ToInt32(worksheet.Cells[row, 17].Value),
                            ID_SoCV = Convert.ToInt32(worksheet.Cells[row, 18].Value),
                            ID_LV = Convert.ToInt32(worksheet.Cells[row, 19].Value),
                            ID_BP = Convert.ToInt32(worksheet.Cells[row, 20].Value),
                            ID_ND = worksheet.Cells[row, 21].Value.ToString().Trim(),
                            Nguoigui_CVDEN = worksheet.Cells[row, 22].Value.ToString().Trim(),
                            Noigui_CVDEN = worksheet.Cells[row, 23].Value.ToString().Trim(),
                        });

                    }
                    if (excelFile != null)
                    {
                        foreach (var item in listExcel)
                        {
                            var currentDate = item.ngay;
                            string yearFolder = currentDate.ToString("yyyy");
                            string yearFolderPath = Path.Combine(upload, yearFolder);
                            var extention = Path.Combine(item.File_CVDEN);
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
                            if (System.IO.File.Exists(item.File_CVDEN))
                            {
                                string fileName = Path.GetFileName(item.File_CVDEN);
                                string destinationPath = Path.Combine(dayFolderPath, fileName);
                                System.IO.File.Copy(item.File_CVDEN, destinationPath, true);
                                item.File_CVDEN = fileName;
                            }
                        }
                    }
                    UnitOfWork.cVDEN.AddRange(listExcel);
                    UnitOfWork.Save();
                    
                }
                return Json(new { success = true, notify = "Bạn đã thêm thành công." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRangeConfirmed(List<int> lst)
        {
            try
            {
                var lstcVDEN = UnitOfWork.cVDEN.GetAllWhere(i => lst.Contains(i.ID));
                foreach (var item in lstcVDEN)
                {
                    item.TrangThai_Xoa = true;
                }
                UnitOfWork.cVDEN.UpdateRange(lstcVDEN);
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
            var totalRecords = UnitOfWork.cVDEN.GetAllWhere(i => i.TrangThai_Xoa == true).Count();
            var data = UnitOfWork.cVDEN.GetFlowRestore(i => i.TrangThai_Xoa == true, start, length, sortColumn, sortDirection, include: "Tb_LoaiVB,Tb_MDMat,Tb_MDKhan,tb_SoCV,Tb_LinhVuc,Tb_PTNhan,Tb_Nguoidung");
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Skh_CVDEN.ToLower().Contains(searchVal)
                || i.NgayBH_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.NgayNhan_CVDEN.ToString().ToString().ToLower().Contains(searchVal)
                || i.SLTrang_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.SL_BPH.ToString().ToLower().Contains(searchVal)
                || i.TrichYeu_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.HanTL_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.Tb_LoaiVB.Ten_LVB.ToString().ToLower().Contains(searchVal)
                || i.Tb_Nguoidung.Hoten_NV.ToString().ToLower().Contains(searchVal)
                || i.Tb_MDMat.Ten_MDMat.ToString().ToLower().Contains(searchVal)
                || i.Tb_MDKhan.Ten_MDKhan.ToString().ToLower().Contains(searchVal)
                || i.tb_SoCV.Ten_SoCV.ToString().ToLower().Contains(searchVal)
                || i.Tb_LinhVuc.Ten_LV.ToString().ToLower().Contains(searchVal)
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
                    var lstcVDEN = UnitOfWork.cVDEN.GetAllWhere(i => lst.Contains(i.ID));
                    foreach (var item in lstcVDEN)
                    {
                        item.TrangThai_Xoa = false;
                    }
                    UnitOfWork.cVDEN.UpdateRange(lstcVDEN);
                    UnitOfWork.Save();
                }
                else
                {
                    var lstcVDEN = UnitOfWork.cVDEN.GetById(i => i.ID == lst[0]);
                    lstcVDEN.TrangThai_Xoa = false;
                    UnitOfWork.cVDEN.Update(lstcVDEN);
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
            var upload = Path.Combine(pathRoot, @"assets\Cong_Van_Den\");
            try
            {
                var lstcVDEN = UnitOfWork.cVDEN.GetAllWhere(i => lst.Contains(i.ID));
                foreach(var item in lstcVDEN)
                {
                    DateTime desiredDate = item.ngay;
                    string yearFolder = desiredDate.ToString("yyyy");
                    string yearFolderPath = Path.Combine(upload, yearFolder);

                    string monthFolder = desiredDate.ToString("MM");
                    string monthFolderPath = Path.Combine(yearFolderPath, monthFolder);

                    string dayFolder = desiredDate.ToString("dd");
                    string dayFolderPath = Path.Combine(monthFolderPath, dayFolder);

                    var file = Path.Combine(dayFolderPath, item.File_CVDEN);
                    if (System.IO.File.Exists(file))
                    {
                        System.IO.File.Delete(file);
                    }
                }
                UnitOfWork.cVDEN.DeleteRange(lstcVDEN);
                UnitOfWork.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(new { isValue = true, notify = "Bạn đã xóa thành công" });
        }
        
        public async Task<IActionResult> ViewTab(int id)
        {
            var pathRoot = _webHostEnvironment.WebRootPath;
            var cvDen = UnitOfWork.cVDEN.GetById(i => i.ID == id);
            var upload = Path.Combine(pathRoot, @"assets\Cong_Van_Den\");

            DateTime desiredDate = cvDen.ngay;

            string yearFolder = desiredDate.ToString("yyyy");
            string yearFolderPath = Path.Combine(upload, yearFolder);

            string monthFolder = desiredDate.ToString("MM");
            string monthFolderPath = Path.Combine(yearFolderPath, monthFolder);

            string dayFolder = desiredDate.ToString("dd");
            string dayFolderPath = Path.Combine(monthFolderPath, dayFolder);

            var file = Path.Combine(dayFolderPath,cvDen.File_CVDEN);

            string extension = System.IO.Path.GetExtension(cvDen.File_CVDEN);

            if (extension == ".pdf")
            {
                return File(System.IO.File.ReadAllBytes(file), "application/pdf");
            }
            else if (extension == ".docx" || extension == ".doc")
            {
                byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(file);

                return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", Path.GetFileName(file));
            }
            else
            {
                return View();
            }
        }
        
    }
}
