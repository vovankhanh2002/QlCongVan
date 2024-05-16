using BanDoWeb.Model.Models;
using BusinessLayer.Handle;
using BusinessLayer.Hubs;
using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OfficeOpenXml;
using System.Security.Claims;
using System.Text;

namespace WS_QuanLyCongVan.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class CVDENController : Controller
    {
        public IUnitOfWork UnitOfWork;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly Ikhanh _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHubContext<Notihub> _hubContext;
        private readonly UserManager<Tb_Nguoidung> _userManager;


        public CVDENController(UserManager<Tb_Nguoidung> userManager, IUnitOfWork UnitOfWork, IWebHostEnvironment webHostEnvironment, Ikhanh emailSender, IHttpContextAccessor httpContextAccessor, IHubContext<Notihub> hubContext)
        {
            this.UnitOfWork = UnitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
            _hubContext = hubContext;
            _userManager = userManager;

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
            var data = UnitOfWork.cVDEN.GetFlowRestore(i => i.TrangThai_Xoa == false, start, length, sortColumn, sortDirection, include: "Tb_LoaiVB,Tb_MDMat,Tb_MDKhan,tb_SoCV,Tb_LinhVuc,Tb_NhanVien").Reverse();
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Skh_CVDEN.ToLower().Contains(searchVal) 
                || i.NgayBH_CVDEN.ToString().ToLower().Contains(searchVal) 
                || i.NgayNhan_CVDEN.ToString().ToString().ToLower().Contains(searchVal) 
                || i.SLTrang_CVDEN.ToString().ToLower().Contains(searchVal) 
                || i.SL_BPH.ToString().ToLower().Contains(searchVal) 
                || i.TrichYeu_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.HanTL_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.Tb_LoaiVB.Ten_LVB.ToLower().Contains(searchVal)
                || i.Tb_NhanVien.Hoten_NV.ToLower().Contains(searchVal)
                || i.Tb_MDMat.Ten_MDMat.ToLower().Contains(searchVal)
                || i.Tb_MDKhan.Ten_MDKhan.ToLower().Contains(searchVal)
                || i.tb_SoCV.Ten_SoCV.ToLower().Contains(searchVal)
                || i.Tb_LinhVuc.Ten_LV.ToLower().Contains(searchVal)
            );
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
        public async Task<IActionResult> getById(int id = 0, string skh = null)
        {

            AllGetListItem getList = new AllGetListItem(UnitOfWork);
            ViewBag.getListSCV = getList.getSoCV();
            ViewBag.getListDM = getList.getDoMat();
            ViewBag.getListDK = getList.getDoKhan();
            ViewBag.getListLCV = getList.getLoaiCV();
            ViewBag.getListLV = getList.getLinhVuc();
            ViewBag.getListBP = getList.getBoPhan();
            ViewBag.getListBPG = getList.getBoPhanGui();
            ViewBag.getListNgK = getList.getNguoiKyCV();

            if (id == 0)
            {
                return View(new Tb_CVDEN());
            }
            else
            {
                var cVDEN = UnitOfWork.cVDEN.GetById(i => i.ID == id || i.Skh_CVDEN == skh);
                ViewBag.getListNV = getList.getNhanvien(cVDEN.ID_BP);
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
                    var cVDEN = UnitOfWork.cVDEN.GetById(i => i.ID == id);
                    string decodedString = Encoding.UTF8.GetString(cVDEN.File_CVDEN);
                    string message = "";
                    message += cVDEN.Skh_CVDEN.ToString();
                    await _emailSender.SendEmailCV(lstMail, tieude, message, decodedString);
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
        [HttpGet]
        public async Task<IActionResult> GetEmployeesByDepartment(string emloy)
        {
            var employees = UnitOfWork.nhanVien.GetAllWhere(i => i.Tb_BoPhan.Ten_BP.ToLower().Contains(emloy));
            return Json(new {sussess = true ,data = employees});
        }
        public async Task<IActionResult> AddOfEdit(int id = 0)
        {
            AllGetListItem getList = new AllGetListItem(UnitOfWork);
            ViewBag.getListSCV = getList.getSoCV();
            ViewBag.getListDM = getList.getDoMat();
            ViewBag.getListDK = getList.getDoKhan();
            ViewBag.getListLCV = getList.getLoaiCV();
            ViewBag.getListLV = getList.getLinhVuc();
            ViewBag.getListBP = getList.getBoPhan();
            ViewBag.getListBPG = getList.getBoPhanGui();
            ViewBag.getListNgK = getList.getNguoiKyCV();
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
            var lstSKH = UnitOfWork.cVDEN.GetAllWhere(i => i.Skh_CVDEN == Tb_CVDEN.Skh_CVDEN).Count();
            AllGetListItem getList = new AllGetListItem(UnitOfWork);
            ViewBag.getListSCV = getList.getSoCV();
            ViewBag.getListDM = getList.getDoMat();
            ViewBag.getListDK = getList.getDoKhan();
            ViewBag.getListLCV = getList.getLoaiCV();
            ViewBag.getListLV = getList.getLinhVuc();
            ViewBag.getListBP = getList.getBoPhan();
            ViewBag.getListBPG = getList.getBoPhanGui();
            ViewBag.getListNgK = getList.getNguoiKyCV();

            if (ModelState.IsValid)
            {
                if (Tb_CVDEN.ID == 0)
                {
                    if (lstSKH == 0)
                    {
                        if (File_CVDEN != null)
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
                                ID_LVB = Tb_CVDEN.ID_LVB,
                                ID_NV = Tb_CVDEN.ID_NV,
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
                            string filePath = Path.Combine(dayFolderPath, extention);
                            using (var fileTream = new FileStream(filePath, FileMode.Create))
                            {
                                File_CVDEN.CopyTo(fileTream);
                            }
                            byte[] Bytes = Encoding.UTF8.GetBytes(filePath);
                            cvden.File_CVDEN = Bytes.ToArray();

                            var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                            var lstUser = UnitOfWork.nguoidung.GetAllWhere(i => i.Id != user.Id);
                            List<Tb_Thongbao> lstThongbao = new List<Tb_Thongbao>();
                            string avartar = Convert.ToBase64String(user.Hinh);
                            lstThongbao.AddRange(lstUser.Select(item => new Tb_Thongbao()
                            {
                                UserID = item.Id,
                                Name = user.Hoten_NV,
                                Cvden_di = true,
                                Skh = cvden.Skh_CVDEN,
                                UserHandelID = user.Id,
                                Noidung = "Bạn có 1 công văn đến với số ký hiệu là: " + cvden.Skh_CVDEN,
                                Thoigian = DateTime.Now,
                                Hinh = avartar
                            }));
                            UnitOfWork.cVDEN.Add(cvden);
                            UnitOfWork.thongbao.AddRange(lstThongbao);
                            UnitOfWork.Save();
                            await _hubContext.Clients.All.SendAsync("ReceiveMessage");
                        }
                        else
                        {
                            return Json(new { success = false, notify = "Bạn chưa chọn file." });
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Skh_CVDEN", "Số ký hiệu đã tồn tại.");
                        return Json(new { success = false, notify = "Đã thêm không thành công.", html = Helper.RenderRazorViewToString(this, "AddOfEdit", Tb_CVDEN) });
                    }

                }
                else
                {
                    var isCvDen = UnitOfWork.cVDEN.GetById(i => i.ID == Tb_CVDEN.ID);
                    try
                    {
                        if (isCvDen.Skh_CVDEN != Tb_CVDEN.Skh_CVDEN && lstSKH != null)
                        {
                            ModelState.AddModelError("Skh_CVDEN", "Số ký hiệu đã tồn tại.");
                            return Json(new { success = false, notify = "Bạn cập nhật không thành công.", html = Helper.RenderRazorViewToString(this, "AddOfEdit", Tb_CVDEN) });
                        }
                        else
                        {
                            isCvDen.Skh_CVDEN = Tb_CVDEN.Skh_CVDEN;
                        }

                        isCvDen.NgayNhan_CVDEN = Tb_CVDEN.NgayNhan_CVDEN;
                        isCvDen.TrichYeu_CVDEN = Tb_CVDEN.TrichYeu_CVDEN;
                        isCvDen.HanTL_CVDEN = Tb_CVDEN.HanTL_CVDEN;
                        isCvDen.GhiChu_CVDEN = Tb_CVDEN.GhiChu_CVDEN;
                        isCvDen.Nguoigui_CVDEN = Tb_CVDEN.Nguoigui_CVDEN;
                        isCvDen.Noigui_CVDEN = Tb_CVDEN.Noigui_CVDEN;
                        isCvDen.PhanCongXLVB_CVDEN = Tb_CVDEN.PhanCongXLVB_CVDEN;
                        isCvDen.TrangThai_CVDI = Tb_CVDEN.TrangThai_CVDI;
                        isCvDen.ID_LVB = Tb_CVDEN.ID_LVB;
                        isCvDen.ID_NV = Tb_CVDEN.ID_NV;
                        isCvDen.ID_MDMat = Tb_CVDEN.ID_MDMat;
                        isCvDen.ID_MDKhan = Tb_CVDEN.ID_MDKhan;
                        isCvDen.ID_PTNhan = Tb_CVDEN.ID_PTNhan;
                        isCvDen.ID_SoCV = Tb_CVDEN.ID_SoCV;
                        isCvDen.ID_LV = Tb_CVDEN.ID_LV;
                        isCvDen.ID_BP = Tb_CVDEN.ID_BP;
                        isCvDen.SLTrang_CVDEN = Tb_CVDEN.SLTrang_CVDEN;
                        isCvDen.SL_BPH = Tb_CVDEN.SL_BPH;
                        isCvDen.TrangThai_Xoa = Tb_CVDEN.TrangThai_Xoa;
                        
                        if (isCvDen.NgayBH_CVDEN == Tb_CVDEN.NgayBH_CVDEN && File_CVDEN != null)
                        {
                            DateTime desiredDate = Tb_CVDEN.NgayBH_CVDEN;
                            var extention = Path.Combine(File_CVDEN.FileName);
                            string yearFolder = desiredDate.ToString("yyyy");
                            string yearFolderPath = Path.Combine(upload, yearFolder);

                            string monthFolder = desiredDate.ToString("MM");
                            string monthFolderPath = Path.Combine(yearFolderPath, monthFolder);

                            string dayFolder = desiredDate.ToString("dd");
                            string dayFolderPath = Path.Combine(monthFolderPath, dayFolder);

                            string decodedString = Encoding.UTF8.GetString(isCvDen.File_CVDEN);


                            if (System.IO.File.Exists(decodedString))
                            {
                                System.IO.File.Delete(decodedString);
                            }

                            using (var fileTream = new FileStream(Path.Combine(dayFolderPath, extention), FileMode.Create))
                            {
                                File_CVDEN.CopyTo(fileTream);
                            }
                            var filePath = Path.Combine(dayFolderPath, extention);

                            byte[] Bytes = Encoding.UTF8.GetBytes(filePath);
                            isCvDen.File_CVDEN = Bytes.ToArray();

                        }
                        else if (isCvDen.NgayBH_CVDEN != Tb_CVDEN.NgayBH_CVDEN && File_CVDEN == null){
                            DateTime desiredDate = Tb_CVDEN.NgayBH_CVDEN;
                            string yearFolder = desiredDate.ToString("yyyy");
                            string yearFolderPath = Path.Combine(upload, yearFolder);

                            string monthFolder = desiredDate.ToString("MM");
                            string monthFolderPath = Path.Combine(yearFolderPath, monthFolder);

                            string dayFolder = desiredDate.ToString("dd");
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

                            string decodedString = Encoding.UTF8.GetString(isCvDen.File_CVDEN);
                            string fileName = Path.GetFileName(decodedString);
                            string destinationPath = Path.Combine(dayFolderPath, fileName);

                            if (System.IO.File.Exists(decodedString))
                            {
                                System.IO.File.Copy(decodedString, destinationPath, true);
                                System.IO.File.Delete(decodedString);
                            }
                            byte[] Bytes = Encoding.UTF8.GetBytes(destinationPath);
                            isCvDen.File_CVDEN = Bytes.ToArray();
                            isCvDen.NgayBH_CVDEN = Tb_CVDEN.NgayBH_CVDEN;
                        }
                        
                        UnitOfWork.cVDEN.Update(isCvDen);
                        UnitOfWork.Save();
                        return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, notify = "Bạn cập nhật không thành công." });
                    }
                }
                return Json(new { success = true, data = Tb_CVDEN, notify = "Bạn đã thêm thành công." });

            }
            return Json(new { success = false, notify = "Đã thêm không thành công.", html = Helper.RenderRazorViewToString(this, "AddOfEdit", Tb_CVDEN) });
        }


        public async Task<IActionResult> ImportExcel()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile excelFile)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var lstSKH = UnitOfWork.cVDEN.GetAll();
            var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var lstUser = UnitOfWork.nguoidung.GetAllWhere(i => i.Id != user.Id);
            List<Tb_Thongbao> lstThongbao = new List<Tb_Thongbao>();
            string avartar = Convert.ToBase64String(user.Hinh);
            var pathRoot = _webHostEnvironment.WebRootPath;
            var upload = Path.Combine(pathRoot, @"assets\Cong_Van_Den\");
            var listExcel = new List<Tb_CVDEN>();
            if (excelFile == null || excelFile.Length <= 0)
            {
                return Json(new { success = false, notify = "Bạn chưa thêm excel hoặc đường dẫn cũ." });
            }
            string extension = System.IO.Path.GetExtension(excelFile.FileName);
            if (extension == ".xlsx")
            {
                using (var stream = new MemoryStream())
                {
                    excelFile.CopyTo(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                       
                        for (int row = worksheet.Dimension.Start.Row + 1; row <= worksheet.Dimension.End.Row; row++)
                        {
                            var lstcv = new Tb_CVDEN()
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
                                File_CVDEN = Encoding.UTF8.GetBytes(worksheet.Cells[row, 10].Value.ToString().Trim()).ToArray(),
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
                                ID_NV = Convert.ToInt32(worksheet.Cells[row, 21].Value),
                                Nguoigui_CVDEN = worksheet.Cells[row, 22].Value.ToString().Trim(),
                                Noigui_CVDEN = worksheet.Cells[row, 23].Value.ToString().Trim(),
                            };
                            if(lstSKH.Any(i => i.Skh_CVDEN == lstcv.Skh_CVDEN))
                            {
                                return Json(new { success = false, notify = "Số ký hiệu" + lstcv.Skh_CVDEN + "đã bị trùng." });
                            }
                            var currentDate = Convert.ToDateTime(lstcv.NgayBH_CVDEN);
                            string yearFolder = currentDate.ToString("yyyy");
                            string yearFolderPath = Path.Combine(upload, yearFolder);
                            string decodedString = Encoding.UTF8.GetString(lstcv.File_CVDEN);
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
                            if (System.IO.File.Exists(decodedString))
                            {
                                string fileName = Path.GetFileName(decodedString);
                                string destinationPath = Path.Combine(dayFolderPath, fileName);
                                System.IO.File.Copy(decodedString, destinationPath, true);
                                byte[] Bytes = Encoding.UTF8.GetBytes(destinationPath);
                                lstcv.File_CVDEN = Bytes.ToArray();
                            }

                            lstThongbao.AddRange(lstUser.Select(item => new Tb_Thongbao()
                            {
                                UserID = item.Id,
                                Name = user.Hoten_NV,
                                Cvden_di = true,
                                Skh = lstcv.Skh_CVDEN,
                                UserHandelID = user.Id,
                                Noidung = "Bạn có 1 công văn đến với số ký hiệu là: " + lstcv.Skh_CVDEN,
                                Thoigian = DateTime.Now,
                                Hinh = avartar
                            }));

                            UnitOfWork.thongbao.AddRange(lstThongbao);
                            listExcel.Add(lstcv);
                        }
                        UnitOfWork.cVDEN.AddRange(listExcel);
                        UnitOfWork.Save();
                        await _hubContext.Clients.All.SendAsync("ReceiveMessage");

                    }
                    return Json(new { success = true, notify = "Bạn đã thêm thành công." });
                }
            }
            return Json(new { success = false, notify = "Bạn file không phải excel." });
        }

        public async Task<IActionResult> AddInEditCategory(int id = 0)
        {
            AllGetListItem allGetListItem = new AllGetListItem(UnitOfWork);
            ViewBag.getListDMCV = allGetListItem.getDanhMucCV();
            var Category = UnitOfWork.dMCV_CV.GetById(i => i.Tb_CVDEN.ID == id, include: "Tb_CVDEN");
            if (Category == null)
            {
                return View(new Tb_DMCV_CV
                {
                    ID = 0,
                    ID_CVDEN = id
                });
            }
            return View(Category);
        }
        [HttpPost]
        public async Task<IActionResult> AddInEditCategory(Tb_DMCV_CV tb_DMCV_CV)
        {
            if (ModelState.IsValid)
            {
                if (tb_DMCV_CV.ID == 0)
                {
                    UnitOfWork.dMCV_CV.Add(tb_DMCV_CV);
                    UnitOfWork.Save();
                    return Json(new { success = true, notify = "Bạn đã thêm thành công." });
                }
                else
                {
                    UnitOfWork.dMCV_CV.Update(tb_DMCV_CV);
                    UnitOfWork.Save();
                    return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });
                }
            }
                return Json(new { success = false, notify = "Đã thêm không thành công.", html = Helper.RenderRazorViewToString(this, "AddOfEdit", tb_DMCV_CV) });
            
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
            var data = UnitOfWork.cVDEN.GetFlowRestore(i => i.TrangThai_Xoa == true, start, length, sortColumn, sortDirection, include: "Tb_LoaiVB,Tb_MDMat,Tb_MDKhan,tb_SoCV,Tb_LinhVuc,Tb_PTNhan,Tb_NhanVien");
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Skh_CVDEN.ToLower().Contains(searchVal)
                || i.NgayBH_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.NgayNhan_CVDEN.ToString().ToString().ToLower().Contains(searchVal)
                || i.SLTrang_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.SL_BPH.ToString().ToLower().Contains(searchVal)
                || i.TrichYeu_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.HanTL_CVDEN.ToString().ToLower().Contains(searchVal)
                || i.Tb_LoaiVB.Ten_LVB.ToString().ToLower().Contains(searchVal)
                || i.Tb_NhanVien.Hoten_NV.ToString().ToLower().Contains(searchVal)
                || i.Tb_MDMat.Ten_MDMat.ToString().ToLower().Contains(searchVal)
                || i.Tb_MDKhan.Ten_MDKhan.ToString().ToLower().Contains(searchVal)
                || i.tb_SoCV.Ten_SoCV.ToString().ToLower().Contains(searchVal)
                || i.Tb_LinhVuc.Ten_LV.ToString().ToLower().Contains(searchVal)
            );
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
                    var lstcVDEN = UnitOfWork.cVDEN.GetAllWhere(i => lst.Contains(i.ID));
                    lstcVDEN.ToList().ForEach(item => item.TrangThai_Xoa = false);
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
            try
            {
                var lstcVDEN = UnitOfWork.cVDEN.GetAllWhere(i => lst.Contains(i.ID));
                lstcVDEN.Where(item => item.File_CVDEN != null)
                    .Select(item => Encoding.UTF8.GetString(item.File_CVDEN))
                    .Where(decodedString => System.IO.File.Exists(decodedString))
                    .ToList()
                    .ForEach(System.IO.File.Delete);
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
            var cvDen = UnitOfWork.cVDEN.GetById(i => i.ID == id);
            string decodedString = Encoding.UTF8.GetString(cvDen.File_CVDEN);
            string extension = System.IO.Path.GetExtension(decodedString);
            if(System.IO.File.Exists(decodedString))
            {
                string contentType;
                switch (extension)
                {
                    case ".pdf":
                        contentType = "application/pdf";
                        break;
                    case ".docx":
                    case ".doc":
                        contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        break;
                    default:
                        return NotFound();
                }
                return File(System.IO.File.ReadAllBytes(decodedString), contentType);
            }
            return View();
        }
    }
}
