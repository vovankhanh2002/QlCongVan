using BusinessLayer.Handle;
using BusinessLayer.Hubs;
using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OfficeOpenXml;
using System.Security.Claims;
using System.Text;

namespace WS_QuanLyCongVan.Controllers
{
    public class CVDIController : Controller
    {
        public IUnitOfWork UnitOfWork;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly Ikhanh _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Tb_Nguoidung> _userManager;
        private readonly IHubContext<Notihub> _hubContext;

        public CVDIController(UserManager<Tb_Nguoidung> userManager, IUnitOfWork UnitOfWork, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment, Ikhanh emailSender, IHubContext<Notihub> hubContext)
        {
            this.UnitOfWork = UnitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _hubContext = hubContext;
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
            var data = UnitOfWork.cVDI.GetFlowRestore(i => i.TrangThai_Xoa == false, start, length, sortColumn, sortDirection, include: "Tb_LoaiVB,Tb_MDMat,Tb_MDKhan,tb_SoCV,Tb_LinhVuc,Tb_NhanVien").Reverse();
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Skh_CVDI.ToLower().Contains(searchVal)
                || i.NgayBH_CVDI.ToString().ToLower().Contains(searchVal)
                || i.Nguoinhan_CVDI.ToString().ToString().ToLower().Contains(searchVal)
                || i.SLTrang_CVDI.ToString().ToLower().Contains(searchVal)
                || i.SL_BPH.ToString().ToLower().Contains(searchVal)
                || i.TrichYeu_CVDI.ToString().ToLower().Contains(searchVal)
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

        public async Task<IActionResult> getById(int id = 0)
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
                return View(new Tb_CVDI());
            }
            else
            {
                var cVDI = UnitOfWork.cVDI.GetById(i => i.ID == id);
                ViewBag.getListNV = getList.getNhanvien(cVDI.ID_BP);
                if (cVDI == null)
                {
                    return NotFound();
                }
                return View(cVDI);
            }
        }
        [HttpPost]
        public async Task<IActionResult> getById(int id = 0, string tieude = null, List<string> lstMail = null)
        {
            if (id == 0)
            {
                return View(new Tb_CVDI());
            }
            else
            {
                if (lstMail != null)
                {
                    var cVDI = UnitOfWork.cVDI.GetById(i => i.ID == id);
                    string decodedString = Encoding.UTF8.GetString(cVDI.File_CVDI);
                    string message = "";
                    message += cVDI.Skh_CVDI.ToString();
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
        public async Task<IActionResult> GetEmployeesByDepartment(int emloy)
        {
            var employees = UnitOfWork.nhanVien.GetAllWhere(i => i.ID_BP == emloy);
            return Json(new { sussess = true, data = employees });
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
            var upload = Path.Combine(pathRoot, @"assets\Cong_Van_Di\");
            AllGetListItem getList = new AllGetListItem(UnitOfWork);
            var lstSKH = UnitOfWork.cVDI.GetAllWhere(i => i.Skh_CVDI == Tb_CVDI.Skh_CVDI).Count();
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
                if(Tb_CVDI.ID == 0)
                {
                    if (lstSKH == 0)
                    {
                        if(File_CVDI != null)
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
                                ID_LVB = Tb_CVDI.ID_LVB,
                                ID_NV = Tb_CVDI.ID_NV,
                                ID_MDMat = Tb_CVDI.ID_MDMat,
                                ID_MDKhan = Tb_CVDI.ID_MDKhan,
                                ID_SoCV = Tb_CVDI.ID_SoCV,
                                ID_LV = Tb_CVDI.ID_LV,
                                ID_BP = Tb_CVDI.ID_BP,
                                TrangThai_Xoa = Tb_CVDI.TrangThai_Xoa,
                                ngay = DateTime.Now
                            };

                            // Tạo thư mục năm
                            string yearFolder = Tb_CVDI.NgayBH_CVDI.ToString("yyyy");
                            string yearFolderPath = Path.Combine(upload, yearFolder);

                            // Tạo thư mục tháng
                            string monthFolder = Tb_CVDI.NgayBH_CVDI.ToString("MM");
                            string monthFolderPath = Path.Combine(yearFolderPath, monthFolder);

                            // Tạo thư mục ngày
                            string dayFolder = Tb_CVDI.NgayBH_CVDI.ToString("dd");
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
                                File_CVDI.CopyTo(fileTream);
                            }
                            byte[] Bytes = Encoding.UTF8.GetBytes(filePath);
                            CVDI.File_CVDI = Bytes.ToArray();

                            var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                            var lstUser = UnitOfWork.nguoidung.GetAllWhere(i => i.Id != user.Id);
                            List<Tb_Thongbao> lstThongbao = new List<Tb_Thongbao>();
                            string avartar = Convert.ToBase64String(user.Hinh);
                            foreach (var item in lstUser)
                            {

                                lstThongbao.Add(new Tb_Thongbao()
                                {
                                    UserID = item.Id,
                                    Name = user.Hoten_NV,
                                    Cvden_di = false,
                                    Skh = CVDI.Skh_CVDI,
                                    UserHandelID = user.Id,
                                    Noidung = "Bạn có 1 công văn đi với số ký hiệu là: " + CVDI.Skh_CVDI,
                                    Thoigian = DateTime.Now,
                                    Hinh = avartar
                                });
                            }
                            UnitOfWork.cVDI.Add(CVDI);
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
                        ModelState.AddModelError("Skh_CVDEN", "Số ký hiệu đã bị trùng.");
                        return Json(new { success = false, notify = "Đã thêm không thành công.", html = Helper.RenderRazorViewToString(this, "AddOfEdit", Tb_CVDI) });
                    }

                }
                else
                {
                    var isCVDI = UnitOfWork.cVDI.GetById(i => i.ID == Tb_CVDI.ID);
                    try
                    {
                        if (isCVDI.Skh_CVDI != Tb_CVDI.Skh_CVDI && lstSKH != null)
                        {
                            ModelState.AddModelError("Skh_CVDEN", "Số ký hiệu đã bị trùng.");
                            return Json(new { success = false, notify = "Bạn cập nhật không thành công.", html = Helper.RenderRazorViewToString(this, "AddOfEdit", Tb_CVDI) });
                        }
                        else
                        {
                            isCVDI.Skh_CVDI = Tb_CVDI.Skh_CVDI;
                        }
                        isCVDI.NgayBH_CVDI = Tb_CVDI.NgayBH_CVDI;
                        isCVDI.TrichYeu_CVDI = Tb_CVDI.TrichYeu_CVDI;
                        isCVDI.SL_BPH = Tb_CVDI.SL_BPH;
                        isCVDI.Noigui_CVDI = Tb_CVDI.Noigui_CVDI;
                        isCVDI.Nguoinhan_CVDI = Tb_CVDI.Nguoinhan_CVDI;
                        isCVDI.SLTrang_CVDI = Tb_CVDI.SLTrang_CVDI;
                        isCVDI.GhiChu_CVDI = Tb_CVDI.GhiChu_CVDI;
                        isCVDI.TrangThai_CVDI = Tb_CVDI.TrangThai_CVDI;
                        isCVDI.ID_LVB = Tb_CVDI.ID_LVB;
                        isCVDI.ID_NV = Tb_CVDI.ID_NV;
                        isCVDI.ID_MDMat = Tb_CVDI.ID_MDMat;
                        isCVDI.ID_MDKhan = Tb_CVDI.ID_MDKhan;
                        isCVDI.ID_SoCV = Tb_CVDI.ID_SoCV;
                        isCVDI.ID_LV = Tb_CVDI.ID_LV;
                        isCVDI.ID_BP = Tb_CVDI.ID_BP;
                        isCVDI.TrangThai_Xoa = Tb_CVDI.TrangThai_Xoa;

                        if (isCVDI.NgayBH_CVDI == Tb_CVDI.NgayBH_CVDI && File_CVDI != null)
                        {
                            DateTime desiredDate = Tb_CVDI.NgayBH_CVDI;
                            var extention = Path.Combine(File_CVDI.FileName);
                            string yearFolder = desiredDate.ToString("yyyy");
                            string yearFolderPath = Path.Combine(upload, yearFolder);

                            string monthFolder = desiredDate.ToString("MM");
                            string monthFolderPath = Path.Combine(yearFolderPath, monthFolder);

                            string dayFolder = desiredDate.ToString("dd");
                            string dayFolderPath = Path.Combine(monthFolderPath, dayFolder);

                            string decodedString = Encoding.UTF8.GetString(isCVDI.File_CVDI);


                            if (System.IO.File.Exists(decodedString))
                            {
                                System.IO.File.Delete(decodedString);
                            }

                            using (var fileTream = new FileStream(Path.Combine(dayFolderPath, extention), FileMode.Create))
                            {
                                File_CVDI.CopyTo(fileTream);
                            }
                            var filePath = Path.Combine(dayFolderPath, extention);

                            byte[] Bytes = Encoding.UTF8.GetBytes(filePath);
                            isCVDI.File_CVDI = Bytes.ToArray();

                        }
                        else if (isCVDI.NgayBH_CVDI != Tb_CVDI.NgayBH_CVDI && File_CVDI == null)
                        {
                            DateTime desiredDate = Tb_CVDI.NgayBH_CVDI;
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

                            string decodedString = Encoding.UTF8.GetString(isCVDI.File_CVDI);
                            string fileName = Path.GetFileName(decodedString);
                            string destinationPath = Path.Combine(dayFolderPath, fileName);

                            if (System.IO.File.Exists(decodedString))
                            {
                                System.IO.File.Copy(decodedString, destinationPath, true);
                                System.IO.File.Delete(decodedString);
                            }
                            byte[] Bytes = Encoding.UTF8.GetBytes(destinationPath);
                            isCVDI.File_CVDI = Bytes.ToArray();
                            isCVDI.NgayBH_CVDI = Tb_CVDI.NgayBH_CVDI;
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
        public async Task<IActionResult> ImportExcel()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile excelFile)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var lstSKH = UnitOfWork.cVDI.GetAll();
            var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var lstUser = UnitOfWork.nguoidung.GetAllWhere(i => i.Id != user.Id);
            List<Tb_Thongbao> lstThongbao = new List<Tb_Thongbao>();
            string avartar = Convert.ToBase64String(user.Hinh);
            var pathRoot = _webHostEnvironment.WebRootPath;
            var upload = Path.Combine(pathRoot, @"assets\Cong_Van_Di\");
            var listExcel = new List<Tb_CVDI>();
            if (excelFile == null || excelFile.Length <= 0)
            {
                return Json(new { success = false, notify = "Bạn chưa thêm excel." });
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
                            var lstcv = new Tb_CVDI()
                            {
                                Skh_CVDI = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                NgayBH_CVDI = Convert.ToDateTime(worksheet.Cells[row, 2].Value),
                                SLTrang_CVDI = Convert.ToInt32(worksheet.Cells[row, 3].Value),
                                SL_BPH = Convert.ToInt32(worksheet.Cells[row, 4].Value),
                                TrichYeu_CVDI = worksheet.Cells[row, 5].Value.ToString().Trim(),
                                Noigui_CVDI = worksheet.Cells[row, 6].Value.ToString().Trim(),
                                Nguoinhan_CVDI = worksheet.Cells[row, 7].Value.ToString().Trim(),
                                File_CVDI = Encoding.UTF8.GetBytes(worksheet.Cells[row, 8].Value.ToString().Trim()).ToArray(),
                                TrangThai_CVDI = Convert.ToBoolean(worksheet.Cells[row, 9].Value),
                                TrangThai_Xoa = Convert.ToBoolean(worksheet.Cells[row, 10].Value),
                                ngay = Convert.ToDateTime(worksheet.Cells[row, 11].Value),
                                ID_LVB = Convert.ToInt32(worksheet.Cells[row, 12].Value),
                                ID_MDMat = Convert.ToInt32(worksheet.Cells[row, 13].Value),
                                ID_MDKhan = Convert.ToInt32(worksheet.Cells[row, 14].Value),
                                ID_SoCV = Convert.ToInt32(worksheet.Cells[row, 15].Value),
                                ID_LV = Convert.ToInt32(worksheet.Cells[row, 16].Value),
                                ID_BP = Convert.ToInt32(worksheet.Cells[row, 17].Value),
                                ID_NV = Convert.ToInt32(worksheet.Cells[row, 18].Value),
                            };
                            if (lstSKH.Any(i => i.Skh_CVDI == lstcv.Skh_CVDI))
                            {
                                return Json(new { success = false, notify = "Số ký hiệu " + lstcv.Skh_CVDI + " đã bị trùng." });
                            }
                            var currentDate = Convert.ToDateTime(lstcv.NgayBH_CVDI);
                            string yearFolder = currentDate.ToString("yyyy");
                            string yearFolderPath = Path.Combine(upload, yearFolder);
                            string decodedString = Encoding.UTF8.GetString(lstcv.File_CVDI);
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
                                lstcv.File_CVDI = Bytes.ToArray();
                            }
                            lstThongbao.AddRange(lstUser.Select(item => new Tb_Thongbao()
                            {
                                UserID = item.Id,
                                Name = user.Hoten_NV,
                                Cvden_di = false,
                                Skh = lstcv.Skh_CVDI,
                                UserHandelID = user.Id,
                                Noidung = "Bạn có 1 công văn đi với số ký hiệu là: " + lstcv.Skh_CVDI,
                                Thoigian = DateTime.Now,
                                Hinh = avartar
                            }));
                            UnitOfWork.thongbao.AddRange(lstThongbao);
                            listExcel.Add(lstcv);
                        }
                        UnitOfWork.cVDI.AddRange(listExcel);
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
            var Category = UnitOfWork.dMCV_CV.GetById(i => i.Tb_CVDI.ID == id, include: "Tb_CVDI");
            if (Category == null)
            {
                return View(new Tb_DMCV_CV
                {
                    ID = 0,
                    ID_CVDi = id
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
                var lstcVDI = UnitOfWork.cVDI.GetAllWhere(i => lst.Contains(i.ID));
                lstcVDI.ToList().ForEach(item => item.TrangThai_Xoa = true);
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
            var data = UnitOfWork.cVDI.GetFlowRestore(i => i.TrangThai_Xoa == true, start, length, sortColumn, sortDirection, include: "Tb_LoaiVB,Tb_MDMat,Tb_MDKhan,tb_SoCV,Tb_LinhVuc,Tb_NhanVien");
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.Skh_CVDI.ToLower().Contains(searchVal)
                || i.NgayBH_CVDI.ToString().ToLower().Contains(searchVal)
                || i.Nguoinhan_CVDI.ToString().ToString().ToLower().Contains(searchVal)
                || i.SLTrang_CVDI.ToString().ToLower().Contains(searchVal)
                || i.SL_BPH.ToString().ToLower().Contains(searchVal)
                || i.TrichYeu_CVDI.ToString().ToLower().Contains(searchVal)
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
        [HttpPost]
        public async Task<IActionResult> RestoreRangeConfirmed(List<int> lst)
        {
            try
            {
                if (lst.Count() > 1)
                {
                    var lstcVDI = UnitOfWork.cVDI.GetAllWhere(i => lst.Contains(i.ID));
                    lstcVDI.ToList().ForEach(item => item.TrangThai_Xoa = false);
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
            try
            {
                var lstcVDI = UnitOfWork.cVDI.GetAllWhere(i => lst.Contains(i.ID));

                lstcVDI.Where(item => item.File_CVDI != null)
                    .Select(item => Encoding.UTF8.GetString(item.File_CVDI))
                    .Where(decodedString => System.IO.File.Exists(decodedString))
                    .ToList()
                    .ForEach(System.IO.File.Delete);
                UnitOfWork.cVDI.DeleteRange(lstcVDI);
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
            var cVDI = UnitOfWork.cVDI.GetById(i => i.ID == id);
            string decodedString = Encoding.UTF8.GetString(cVDI.File_CVDI);
            string extension = System.IO.Path.GetExtension(decodedString);
            if (System.IO.File.Exists(decodedString))
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
