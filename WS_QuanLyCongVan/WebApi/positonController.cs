using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WS_QuanLyCongVan.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class positonController : ControllerBase
    {
        public IUnitOfWork UnitOfWork;

        public positonController(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Tb_ChucVu>> Get()
        {

            return Ok(UnitOfWork.chucVu.GetAll());
        }
    }
}
