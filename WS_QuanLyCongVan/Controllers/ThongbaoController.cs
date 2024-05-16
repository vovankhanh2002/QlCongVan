using BusinessLayer.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WS_QuanLyCongVan.Controllers
{
    public class ThongbaoController : Controller
    {
        public IUnitOfWork UnitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ThongbaoController(IUnitOfWork UnitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.UnitOfWork = UnitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult>  Index()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var lstThongbao = UnitOfWork.thongbao.GetAllWhere(i => i.UserID == userId).Reverse();
            return Json(new {data = lstThongbao});
        }
        public async Task<IActionResult> Edit(int id)
        {
            var ThongbaoId = UnitOfWork.thongbao.GetById(i => i.ID == id);
            ThongbaoId.Trangthai = true;
            UnitOfWork.thongbao.Update(ThongbaoId);
            UnitOfWork.Save();
            return Json(new { status = ThongbaoId.Trangthai,statusCV = ThongbaoId.Cvden_di });
        }
        public async Task<IActionResult> Delete(int id)
        {
            var ThongbaoId = UnitOfWork.thongbao.GetById(i => i.ID == id);
            UnitOfWork.thongbao.Delete(ThongbaoId);
            UnitOfWork.Save();
            return Json(new { status = true });
        }
    }
}
