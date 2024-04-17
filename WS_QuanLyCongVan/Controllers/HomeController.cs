using BusinessLayer.Handle;
using BusinessLayer.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace WS_QuanLyCongVan.Controllers
{
    public class HomeController : Controller
    {
        public IUnitOfWork UnitOfWork;

        public HomeController(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> getCVDEN()
        {
            var cntCVDEN = UnitOfWork.cVDEN.GetAll().Count();
            return Json(new { data = cntCVDEN });
        }
        [HttpGet]
        public async Task<IActionResult> getCVDI()
        {
            var cntCVDI = UnitOfWork.cVDI.GetAll().Count();
            return Json(new { data = cntCVDI });
        }
        [HttpGet]
        public async Task<IActionResult> getCvdiOfCvden()
        {
            var cntCVDICheck = UnitOfWork.cVDI.GetAllWhere(i => i.TrangThai_CVDI == true).Count();
            var cntCVDENCheck = UnitOfWork.cVDEN.GetAllWhere(i => i.TrangThai_CVDI == true).Count();
            var sumCheck = cntCVDENCheck + cntCVDICheck;
            var cntCVDINoCheck = UnitOfWork.cVDI.GetAllWhere(i => i.TrangThai_CVDI == false).Count();
            var cntCVDENNoCheck = UnitOfWork.cVDEN.GetAllWhere(i => i.TrangThai_CVDI == false).Count();
            var sumNoCheck = cntCVDINoCheck + cntCVDENNoCheck;
            return Json(new { dataCheck = sumCheck, dataNoCheck = sumNoCheck });
        }
        [HttpGet]
        public async Task<IActionResult> getEmployess()
        {
            var cntEmloyess = UnitOfWork.nhanVien.GetAll().Count();
            return Json(new { data = cntEmloyess });
        }
        [HttpGet]
        public async Task<IActionResult> getAccount()
        {
            var cntAccount = UnitOfWork.nguoidung.GetAll().Count();
            return Json(new { data = cntAccount });
        }
        [HttpGet]
        public async Task<IActionResult> getPart()
        {
            var lstPart = UnitOfWork.boPhan.GetAll();
            return Json(new { data = lstPart });
        }

        [HttpGet]
        public async Task<IActionResult> Statistical(int cv = 0, int day = 0, int month = 0, int year = 0, int part = 0)
        {
            var combinedCountByDate = new List<Statistical>();
            int currentYear = DateTime.Now.Year;
            var cvden = UnitOfWork.cVDEN.GetAllWhere(i => i.TrangThai_Xoa == false);
            var cvdi = UnitOfWork.cVDI.GetAllWhere(i => i.TrangThai_Xoa == false);
            if (cv == 1)
            {
                if(part == 0)
                {
                    if (day == 0 && month == 0 && year == 0)
                    {


                        var cvdenCountByDate = cvden
                            .Where(i => i.NgayBH_CVDEN.Year == currentYear)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == true && i.NgayBH_CVDEN.Year == currentYear)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountNoCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == false && i.NgayBH_CVDEN.Year == currentYear)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });

                        return Json(new
                        {
                            data = cvdenCountByDate.OrderBy(i => i.Date),
                            ckecked = cvdenCountCkecked.OrderBy(i => i.Date),
                            noCkecked = cvdenCountNoCkecked.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else if (day == 0 && month == 0 && year != 0)
                    {


                        var cvdenCountByDate = cvden
                            .Where(i => i.NgayBH_CVDEN.Year == year)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == true && i.NgayBH_CVDEN.Year == currentYear)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountNoCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == false && i.NgayBH_CVDEN.Year == currentYear)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });

                        return Json(new
                        {
                            data = cvdenCountByDate.OrderBy(i => i.Date),
                            ckecked = cvdenCountCkecked.OrderBy(i => i.Date),
                            noCkecked = cvdenCountNoCkecked.OrderBy(i => i.Date),

                            success = true
                        });
                    }
                    else if (day != 0 && month != 0 && year != 0)
                    {
                        var cvdenCountByDate = cvden
                            .Where(i => i.NgayBH_CVDEN.Day == day && i.NgayBH_CVDEN.Month == month && i.NgayBH_CVDEN.Year == year)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == true && i.NgayBH_CVDEN.Year == currentYear)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountNoCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == false && i.NgayBH_CVDEN.Year == currentYear)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });

                        return Json(new
                        {
                            data = cvdenCountByDate.OrderBy(i => i.Date),
                            ckecked = cvdenCountCkecked.OrderBy(i => i.Date),
                            noCkecked = cvdenCountNoCkecked.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else if (day == 0 && month != 0 && year != 0)
                    {
                        var cvdenCountByDate = cvden
                            .Where(i => i.NgayBH_CVDEN.Month == month && i.NgayBH_CVDEN.Year == year)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == true && i.NgayBH_CVDEN.Year == currentYear)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountNoCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == false && i.NgayBH_CVDEN.Year == currentYear)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });

                        return Json(new
                        {
                            data = cvdenCountByDate.OrderBy(i => i.Date),
                            ckecked = cvdenCountCkecked.OrderBy(i => i.Date),
                            noCkecked = cvdenCountNoCkecked.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else
                    {
                        return Json(new { success = false });
                    }
                }
                else
                {
                    if (day == 0 && month == 0 && year == 0)
                    {


                        var cvdenCountByDate = cvden
                            .Where(i => i.NgayBH_CVDEN.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == true && i.NgayBH_CVDEN.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountNoCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == false && i.NgayBH_CVDEN.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });

                        return Json(new
                        {
                            data = cvdenCountByDate.OrderBy(i => i.Date),
                            ckecked = cvdenCountCkecked.OrderBy(i => i.Date),
                            noCkecked = cvdenCountNoCkecked.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else if (day == 0 && month == 0 && year != 0)
                    {


                        var cvdenCountByDate = cvden
                            .Where(i => i.NgayBH_CVDEN.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == true && i.NgayBH_CVDEN.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountNoCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == false && i.NgayBH_CVDEN.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });

                        return Json(new
                        {
                            data = cvdenCountByDate.OrderBy(i => i.Date),
                            ckecked = cvdenCountCkecked.OrderBy(i => i.Date),
                            noCkecked = cvdenCountNoCkecked.OrderBy(i => i.Date),

                            success = true
                        });
                    }
                    else if (day != 0 && month != 0 && year != 0)
                    {
                        var cvdenCountByDate = cvden
                            .Where(i => i.NgayBH_CVDEN.Day == day && i.NgayBH_CVDEN.Month == month && i.NgayBH_CVDEN.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == true && i.NgayBH_CVDEN.Month == month && i.NgayBH_CVDEN.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountNoCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == false && i.NgayBH_CVDEN.Month == month && i.NgayBH_CVDEN.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });

                        return Json(new
                        {
                            data = cvdenCountByDate.OrderBy(i => i.Date),
                            ckecked = cvdenCountCkecked.OrderBy(i => i.Date),
                            noCkecked = cvdenCountNoCkecked.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else if (day == 0 && month != 0 && year != 0)
                    {
                        var cvdenCountByDate = cvden
                            .Where(i => i.NgayBH_CVDEN.Month == month && i.NgayBH_CVDEN.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == true && i.NgayBH_CVDEN.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountNoCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == false && i.NgayBH_CVDEN.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });

                        return Json(new
                        {
                            data = cvdenCountByDate.OrderBy(i => i.Date),
                            ckecked = cvdenCountCkecked.OrderBy(i => i.Date),
                            noCkecked = cvdenCountNoCkecked.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else
                    {
                        return Json(new { success = false });
                    }
                }
                
            }
            else if (cv == 0)
            {
                if (part == 0)
                {
                    if (day == 0 && month == 0 && year == 0)
                    {
                        var cvdenCountByDate = cvden
                            .Where(i => i.NgayBH_CVDEN.Year == currentYear)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var cvdiCountByDate = cvdi
                            .Where(i => i.NgayBH_CVDI.Year == currentYear)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var combinedCountBy = cvdenCountByDate
                            .Concat(cvdiCountByDate)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        var cvdenCountByDateTrue = cvden
                            .Where(i => i.NgayBH_CVDEN.Year == currentYear && i.TrangThai_CVDI == true )
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var cvdiCountByDateTrue = cvdi
                            .Where(i => i.NgayBH_CVDI.Year == currentYear && i.TrangThai_CVDI == true )
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var cvdenCountByDateFalse = cvden
                            .Where(i => i.NgayBH_CVDEN.Year == currentYear && i.TrangThai_CVDI == false )
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var cvdiCountByDateFalse = cvdi
                            .Where(i => i.NgayBH_CVDI.Year == currentYear && i.TrangThai_CVDI == false )
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var combinedCountByTrue = cvdenCountByDateTrue
                            .Concat(cvdiCountByDateTrue)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        var combinedCountByFalse = cvdenCountByDateFalse
                            .Concat(cvdiCountByDateFalse)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        return Json(new
                        {
                            data = combinedCountBy,
                            ckecked = combinedCountByTrue.OrderBy(i => i.Date),
                            noCkecked = combinedCountByFalse.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else if (day != 0 && month != 0 && year != 0)
                    {
                        var cvdenCountByDate = cvden
                            .Where(i => i.NgayBH_CVDEN.Day == day && i.NgayBH_CVDEN.Month == month && i.NgayBH_CVDEN.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var cvdiCountByDate = cvdi
                            .Where(i => i.NgayBH_CVDI.Day == day && i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var combinedCountBy = cvdenCountByDate
                            .Concat(cvdiCountByDate)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        var cvdenCountByDateTrue = cvden
                            .Where(i => i.NgayBH_CVDEN.Day == day && i.NgayBH_CVDEN.Month == month && i.NgayBH_CVDEN.Year == year && i.TrangThai_CVDI == true && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var cvdiCountByDateTrue = cvdi
                            .Where(i => i.NgayBH_CVDI.Day == day && i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == year && i.TrangThai_CVDI == true && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var cvdenCountByDateFalse = cvden
                            .Where(i => i.NgayBH_CVDEN.Day == day && i.NgayBH_CVDEN.Month == month && i.NgayBH_CVDEN.Year == year && i.TrangThai_CVDI == false && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var cvdiCountByDateFalse = cvdi
                            .Where(i => i.NgayBH_CVDI.Day == day && i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == year && i.TrangThai_CVDI == false && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var combinedCountByTrue = cvdenCountByDateTrue
                            .Concat(cvdiCountByDateTrue)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        var combinedCountByFalse = cvdenCountByDateFalse
                            .Concat(cvdiCountByDateFalse)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        return Json(new
                        {
                            data = combinedCountBy,
                            ckecked = combinedCountByTrue.OrderBy(i => i.Date),
                            noCkecked = combinedCountByFalse.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else if (day == 0 && month != 0 && year != 0)
                    {
                        var cvdenCountByDate = cvden
                            .Where(i => i.NgayBH_CVDEN.Month == month && i.NgayBH_CVDEN.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var cvdiCountByDate = cvdi
                            .Where(i => i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var combinedCountBy = cvdenCountByDate
                            .Concat(cvdiCountByDate)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        ///
                        var cvdenCountByDateTrue = cvden
                            .Where(i => i.NgayBH_CVDEN.Month == month && i.NgayBH_CVDEN.Year == year && i.TrangThai_CVDI == true && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var cvdiCountByDateTrue = cvdi
                            .Where(i => i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == year && i.TrangThai_CVDI == true && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var cvdenCountByDateFalse = cvden
                            .Where(i => i.NgayBH_CVDEN.Month == month && i.NgayBH_CVDEN.Year == year && i.TrangThai_CVDI == false && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var cvdiCountByDateFalse = cvdi
                            .Where(i => i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == year && i.TrangThai_CVDI == false && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var combinedCountByTrue = cvdenCountByDateTrue
                            .Concat(cvdiCountByDateTrue)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        var combinedCountByFalse = cvdenCountByDateFalse
                            .Concat(cvdiCountByDateFalse)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        return Json(new
                        {
                            data = combinedCountBy,
                            ckecked = combinedCountByTrue.OrderBy(i => i.Date),
                            noCkecked = combinedCountByFalse.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else if (day == 0 && month == 0 && year != 0)
                    {
                        var cvdenCountByDate = cvden
                            .Where(i => i.NgayBH_CVDEN.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var cvdiCountByDate = cvdi
                            .Where(i => i.NgayBH_CVDI.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var combinedCountBy = cvdenCountByDate
                            .Concat(cvdiCountByDate)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();
                        ///
                        var cvdenCountByDateTrue = cvden
                           .Where(i => i.NgayBH_CVDEN.Year == year && i.TrangThai_CVDI == true && i.ID_BP == part)
                           .GroupBy(x => x.NgayBH_CVDEN.Date)
                           .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var cvdiCountByDateTrue = cvdi
                            .Where(i => i.NgayBH_CVDI.Year == year && i.TrangThai_CVDI == true && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var cvdenCountByDateFalse = cvden
                            .Where(i => i.NgayBH_CVDEN.Year == year && i.TrangThai_CVDI == false && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var cvdiCountByDateFalse = cvdi
                            .Where(i => i.NgayBH_CVDI.Year == year && i.TrangThai_CVDI == false&& i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var combinedCountByTrue = cvdenCountByDateTrue
                            .Concat(cvdiCountByDateTrue)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        var combinedCountByFalse = cvdenCountByDateFalse
                            .Concat(cvdiCountByDateFalse)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        return Json(new
                        {
                            data = combinedCountBy,
                            ckecked = combinedCountByTrue.OrderBy(i => i.Date),
                            noCkecked = combinedCountByFalse.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else
                    {
                        return Json(new { success = false });
                    }
                }
                else
                {
                    if (day == 0 && month == 0 && year == 0)
                    {
                        var cvdenCountByDate = cvden
                            .Where(i => i.NgayBH_CVDEN.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var cvdiCountByDate = cvdi
                            .Where(i => i.NgayBH_CVDI.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var combinedCountBy = cvdenCountByDate
                            .Concat(cvdiCountByDate)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        var cvdenCountByDateTrue = cvden
                            .Where(i => i.NgayBH_CVDEN.Year == currentYear && i.TrangThai_CVDI == true && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var cvdiCountByDateTrue = cvdi
                            .Where(i => i.NgayBH_CVDI.Year == currentYear && i.TrangThai_CVDI == true && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var cvdenCountByDateFalse = cvden
                            .Where(i => i.NgayBH_CVDEN.Year == currentYear && i.TrangThai_CVDI == false && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var cvdiCountByDateFalse = cvdi
                            .Where(i => i.NgayBH_CVDI.Year == currentYear && i.TrangThai_CVDI == false && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var combinedCountByTrue = cvdenCountByDateTrue
                            .Concat(cvdiCountByDateTrue)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        var combinedCountByFalse = cvdenCountByDateFalse
                            .Concat(cvdiCountByDateFalse)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        return Json(new
                        {
                            data = combinedCountBy,
                            ckecked = combinedCountByTrue.OrderBy(i => i.Date),
                            noCkecked = combinedCountByFalse.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else if (day != 0 && month != 0 && year != 0)
                    {
                        var cvdenCountByDate = cvden
                            .Where(i => i.NgayBH_CVDEN.Day == day && i.NgayBH_CVDEN.Month == month && i.NgayBH_CVDEN.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var cvdiCountByDate = cvdi
                            .Where(i => i.NgayBH_CVDI.Day == day && i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var combinedCountBy = cvdenCountByDate
                            .Concat(cvdiCountByDate)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        var cvdenCountByDateTrue = cvden
                            .Where(i => i.NgayBH_CVDEN.Day == day && i.NgayBH_CVDEN.Month == month && i.NgayBH_CVDEN.Year == year && i.TrangThai_CVDI == true && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var cvdiCountByDateTrue = cvdi
                            .Where(i => i.NgayBH_CVDI.Day == day && i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == year && i.TrangThai_CVDI == true && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var cvdenCountByDateFalse = cvden
                            .Where(i => i.NgayBH_CVDEN.Day == day && i.NgayBH_CVDEN.Month == month && i.NgayBH_CVDEN.Year == year && i.TrangThai_CVDI == false && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var cvdiCountByDateFalse = cvdi
                            .Where(i => i.NgayBH_CVDI.Day == day && i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == year && i.TrangThai_CVDI == false && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var combinedCountByTrue = cvdenCountByDateTrue
                            .Concat(cvdiCountByDateTrue)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        var combinedCountByFalse = cvdenCountByDateFalse
                            .Concat(cvdiCountByDateFalse)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        return Json(new
                        {
                            data = combinedCountBy,
                            ckecked = combinedCountByTrue.OrderBy(i => i.Date),
                            noCkecked = combinedCountByFalse.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else if (day == 0 && month != 0 && year != 0)
                    {
                        var cvdenCountByDate = cvden
                            .Where(i => i.NgayBH_CVDEN.Month == month && i.NgayBH_CVDEN.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var cvdiCountByDate = cvdi
                            .Where(i => i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var combinedCountBy = cvdenCountByDate
                            .Concat(cvdiCountByDate)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        ///
                        var cvdenCountByDateTrue = cvden
                            .Where(i => i.NgayBH_CVDEN.Month == month && i.NgayBH_CVDEN.Year == year && i.TrangThai_CVDI == true && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var cvdiCountByDateTrue = cvdi
                            .Where(i => i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == year && i.TrangThai_CVDI == true && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var cvdenCountByDateFalse = cvden
                            .Where(i => i.NgayBH_CVDEN.Month == month && i.NgayBH_CVDEN.Year == year && i.TrangThai_CVDI == false && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var cvdiCountByDateFalse = cvdi
                            .Where(i => i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == year && i.TrangThai_CVDI == false && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("dd/MM/yyyy"), Count = group.Count() });

                        var combinedCountByTrue = cvdenCountByDateTrue
                            .Concat(cvdiCountByDateTrue)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        var combinedCountByFalse = cvdenCountByDateFalse
                            .Concat(cvdiCountByDateFalse)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        return Json(new
                        {
                            data = combinedCountBy,
                            ckecked = combinedCountByTrue.OrderBy(i => i.Date),
                            noCkecked = combinedCountByFalse.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else if (day == 0 && month == 0 && year != 0)
                    {
                        var cvdenCountByDate = cvden
                            .Where(i => i.NgayBH_CVDEN.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var cvdiCountByDate = cvdi
                            .Where(i => i.NgayBH_CVDI.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var combinedCountBy = cvdenCountByDate
                            .Concat(cvdiCountByDate)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();
                        ///
                        var cvdenCountByDateTrue = cvden
                           .Where(i => i.NgayBH_CVDEN.Year == year && i.TrangThai_CVDI == true && i.ID_BP == part)
                           .GroupBy(x => x.NgayBH_CVDEN.Date)
                           .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var cvdiCountByDateTrue = cvdi
                            .Where(i => i.NgayBH_CVDI.Year == year && i.TrangThai_CVDI == true && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var cvdenCountByDateFalse = cvden
                            .Where(i => i.NgayBH_CVDEN.Year == year && i.TrangThai_CVDI == false && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var cvdiCountByDateFalse = cvdi
                            .Where(i => i.NgayBH_CVDI.Year == year && i.TrangThai_CVDI == false && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.Date)
                            .Select(group => new { Date = group.Key.ToString("yyyy"), Count = group.Count() });

                        var combinedCountByTrue = cvdenCountByDateTrue
                            .Concat(cvdiCountByDateTrue)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        var combinedCountByFalse = cvdenCountByDateFalse
                            .Concat(cvdiCountByDateFalse)
                            .GroupBy(x => x.Date)
                            .Select(group => new { Date = group.Key.ToString(), Count = group.Sum(x => x.Count) })
                            .OrderBy(x => x.Date)
                            .ToList();

                        return Json(new
                        {
                            data = combinedCountBy,
                            ckecked = combinedCountByTrue.OrderBy(i => i.Date),
                            noCkecked = combinedCountByFalse.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else
                    {
                        return Json(new { success = false });
                    }
                }
                
            }
            else
            {
                if(part == 0)
                {
                    if (day == 0 && month == 0 && year == 0)
                    {

                        var cvdenCountByDate = cvdi
                            .Where(i => i.NgayBH_CVDI.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountCkecked = cvdi
                            .Where(i => i.TrangThai_CVDI == true && i.NgayBH_CVDI.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountNoCkecked = cvdi
                            .Where(i => i.TrangThai_CVDI == false && i.NgayBH_CVDI.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });

                        return Json(new
                        {
                            data = cvdenCountByDate.OrderBy(i => i.Date),
                            ckecked = cvdenCountCkecked.OrderBy(i => i.Date),
                            noCkecked = cvdenCountNoCkecked.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else if (day == 0 && month == 0 && year != 0)
                    {

                        var cvdenCountByDate = cvdi
                            .Where(i => i.NgayBH_CVDI.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountCkecked = cvdi
                            .Where(i => i.TrangThai_CVDI == true && i.NgayBH_CVDI.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountNoCkecked = cvdi
                            .Where(i => i.TrangThai_CVDI == false && i.NgayBH_CVDI.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });

                        return Json(new
                        {
                            data = cvdenCountByDate.OrderBy(i => i.Date),
                            ckecked = cvdenCountCkecked.OrderBy(i => i.Date),
                            noCkecked = cvdenCountNoCkecked.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else if (day != 0 && month != 0 && year != 0)
                    {
                        var cvdenCountByDate = cvdi
                            .Where(i => i.NgayBH_CVDI.Day == day && i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountCkecked = cvdi
                            .Where(i => i.TrangThai_CVDI == true && i.NgayBH_CVDI.Day == day && i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountNoCkecked = cvdi
                            .Where(i => i.TrangThai_CVDI == false && i.NgayBH_CVDI.Day == day && i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });

                        return Json(new
                        {
                            data = cvdenCountByDate.OrderBy(i => i.Date),
                            ckecked = cvdenCountCkecked.OrderBy(i => i.Date),
                            noCkecked = cvdenCountNoCkecked.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else if (day == 0 && month != 0 && year != 0)
                    {
                        var cvdenCountByDate = cvdi
                            .Where(i => i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == true && i.NgayBH_CVDEN.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountNoCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == false && i.NgayBH_CVDEN.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });

                        return Json(new
                        {
                            data = cvdenCountByDate.OrderBy(i => i.Date),
                            ckecked = cvdenCountCkecked.OrderBy(i => i.Date),
                            noCkecked = cvdenCountNoCkecked.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else
                    {
                        return Json(new { success = false });
                    }
                }
                else
                {
                    if (day == 0 && month == 0 && year == 0)
                    {

                        var cvdenCountByDate = cvdi
                            .Where(i => i.NgayBH_CVDI.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountCkecked = cvdi
                            .Where(i => i.TrangThai_CVDI == true && i.NgayBH_CVDI.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountNoCkecked = cvdi
                            .Where(i => i.TrangThai_CVDI == false && i.NgayBH_CVDI.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });

                        return Json(new
                        {
                            data = cvdenCountByDate.OrderBy(i => i.Date),
                            ckecked = cvdenCountCkecked.OrderBy(i => i.Date),
                            noCkecked = cvdenCountNoCkecked.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else if (day == 0 && month == 0 && year != 0)
                    {

                        var cvdenCountByDate = cvdi
                            .Where(i => i.NgayBH_CVDI.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountCkecked = cvdi
                            .Where(i => i.TrangThai_CVDI == true && i.NgayBH_CVDI.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountNoCkecked = cvdi
                            .Where(i => i.TrangThai_CVDI == false && i.NgayBH_CVDI.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });

                        return Json(new
                        {
                            data = cvdenCountByDate.OrderBy(i => i.Date),
                            ckecked = cvdenCountCkecked.OrderBy(i => i.Date),
                            noCkecked = cvdenCountNoCkecked.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else if (day != 0 && month != 0 && year != 0)
                    {
                        var cvdenCountByDate = cvdi
                            .Where(i => i.NgayBH_CVDI.Day == day && i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountCkecked = cvdi
                            .Where(i => i.TrangThai_CVDI == true && i.NgayBH_CVDI.Day == day && i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountNoCkecked = cvdi
                            .Where(i => i.TrangThai_CVDI == false && i.NgayBH_CVDI.Day == day && i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });

                        return Json(new
                        {
                            data = cvdenCountByDate.OrderBy(i => i.Date),
                            ckecked = cvdenCountCkecked.OrderBy(i => i.Date),
                            noCkecked = cvdenCountNoCkecked.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else if (day == 0 && month != 0 && year != 0)
                    {
                        var cvdenCountByDate = cvdi
                            .Where(i => i.NgayBH_CVDI.Month == month && i.NgayBH_CVDI.Year == year && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDI.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == true && i.NgayBH_CVDEN.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });
                        var cvdenCountNoCkecked = cvden
                            .Where(i => i.TrangThai_CVDI == false && i.NgayBH_CVDEN.Year == currentYear && i.ID_BP == part)
                            .GroupBy(x => x.NgayBH_CVDEN.ToString("dd/MM/yyyy"))
                            .Select(group => new { Date = group.Key, Count = group.Count() });

                        return Json(new
                        {
                            data = cvdenCountByDate.OrderBy(i => i.Date),
                            ckecked = cvdenCountCkecked.OrderBy(i => i.Date),
                            noCkecked = cvdenCountNoCkecked.OrderBy(i => i.Date),
                            success = true
                        });
                    }
                    else
                    {
                        return Json(new { success = false });
                    }
                }
             return Json(new { success = false });
            }
        }

    }
}