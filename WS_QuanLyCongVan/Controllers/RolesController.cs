using DataLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WS_QuanLyCongVan.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> getAll()
        {
            var draw = Request.Form["draw"];
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            var searchVal = Request.Form["search[value]"].ToString().ToLower();
            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortDirection = Request.Form["order[0][dir]"];
            var data = await _roleManager.Roles.ToListAsync();
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

        [HttpGet]
        public async Task<IActionResult> AddOfEdit(string id)
        {
            return View(new Roles());
        }
        [HttpPost]
        public async Task<IActionResult> AddOfEdit(Roles roles)
        {
            var rolesExits = await _roleManager.RoleExistsAsync(roles.Name);
            if (rolesExits)
            {
                ModelState.AddModelError("Name", "Quyền đã tồn tại");
                return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "AddOfEdit", roles) });
            }
            await _roleManager.CreateAsync(new IdentityRole(roles.Name));
            return Json(new { success = true, data = roles, notify = "Bạn đã thêm thành công." });
        }
    }
}
