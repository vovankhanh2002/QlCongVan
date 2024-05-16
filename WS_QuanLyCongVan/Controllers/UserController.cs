using DataLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;

namespace WS_QuanLyCongVan.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<Tb_Nguoidung> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;

        public UserController(UserManager<Tb_Nguoidung> userManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment webHostEnvironment, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
        }
        public IActionResult Index()
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
            var data = _userManager.Users.Select(user => new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Hoten_NV = user.Hoten_NV,
                DiaChi_NV = user.DiaChi_NV,
                Email = user.Email,
                SDT_NV = user.SDT_NV,
                NgaySinh_NV = user.NgaySinh_NV,
                Hinh = user.Hinh,
                Roles = _userManager.GetRolesAsync(user).Result
            }).ToList();
            if (!string.IsNullOrEmpty(searchVal))
                data = data.Where(i => i.UserName.ToLower().Contains(searchVal)
                || i.Hoten_NV.ToLower().Contains(searchVal)
                || i.DiaChi_NV.ToLower().Contains(searchVal)
                || i.Email.ToLower().Contains(searchVal)
                || i.Email.ToLower().Contains(searchVal)
                || i.SDT_NV.ToString().ToLower().Contains(searchVal)
                || i.NgaySinh_NV.ToString().ToLower().Contains(searchVal)
                || i.Roles.ToList().ToString().ToLower().Contains(searchVal)
                ).ToList();
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
        public async Task<IActionResult> Add()
        {
            var roles = await _roleManager.Roles.Select(r => new RoleViewModel { RoleId = r.Id, RoleName = r.Name }).ToListAsync();

            var viewModel = new AddUserViewModel
            {
                Roles = roles
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "Add", model) });

            if (!model.Roles.Any(r => r.IsSelect))
            {
                ModelState.AddModelError("Roles", "Please select at least one role");
                return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "Add", model) });
            }

            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "Email is already exists");
                return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "Add", model) });
            }

            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError("UserName", "Username is already exists");
                return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "Add", model) });
            }
            
            var user = new Tb_Nguoidung
            {
                UserName = model.UserName,
                Email = model.Email,
                Hoten_NV = model.Hoten_NV,
                DiaChi_NV = model.DiaChi_NV,
                SDT_NV = model.SDT_NV,
                NgaySinh_NV = model.NgaySinh_NV,

            };
            string webRootPath = _webHostEnvironment.WebRootPath;
            string imagePath = Path.Combine(webRootPath, "assets/css/images/avatar/avatar-male.jpg");
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            user.Hinh = imageBytes.ToArray();
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Roles", error.Description);
                }

                return View(model);
            }

            await _userManager.AddToRolesAsync(user, model.Roles.Where(r => r.IsSelect).Select(r => r.RoleName));
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = user.Id, code = code },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            return Json(new { success = true, notify = "Bạn đã thêm thành công." });
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var viewModel = new UpdateViewModel
            {
                Id = id,
                UserName = user.UserName,
                Email = user.Email,
                Hoten_NV = user.Hoten_NV,
                DiaChi_NV = user.DiaChi_NV,
                SDT_NV = user.SDT_NV,
                NgaySinh_NV = user.NgaySinh_NV
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "Edit", model) });

            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
                return NotFound();

            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);

            if (userWithSameEmail != null && userWithSameEmail.Id != model.Id)
            {
                ModelState.AddModelError("Email", "This email is already assiged to another user");
                return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "Edit", model) });

            }

            var userWithSameUserName = await _userManager.FindByNameAsync(model.UserName);

            if (userWithSameUserName != null && userWithSameUserName.Id != model.Id)
            {
                ModelState.AddModelError("UserName", "This username is already assiged to another user");
                return Json(new { success = false, html = Helper.RenderRazorViewToString(this, "Edit", model) });

            }

            user.Hoten_NV = model.Hoten_NV;
            user.DiaChi_NV = model.DiaChi_NV;
            user.SDT_NV = model.SDT_NV;
            user.NgaySinh_NV = model.NgaySinh_NV;
            user.UserName = model.UserName;
            user.Email = model.Email;

            await _userManager.UpdateAsync(user);

            return Json(new { success = true, notify = "Bạn đã cập nhật thành công." });

        }

        [HttpPost]
        public async Task<IActionResult> DeleteRange(List<string> lst)
        {
            if (lst != null)
            {
                try
                {

                    foreach (var item in lst)
                    {
                        var user = await _userManager.FindByIdAsync(item);
                        var lstchucVu = await _userManager.DeleteAsync(user);
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { isValue = false, notify = "Bạn đã xóa không thành công" });
                }
            }
            return Json(new { isValue = true, notify = "Bạn đã xóa thành công" });
        }

        public async Task<IActionResult> AddUserRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();

            var viewModel = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.Select(role => new RoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSelect = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddUserRoles(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if (userRoles.Any(r => r == role.RoleName) && !role.IsSelect)
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);

                if (!userRoles.Any(r => r == role.RoleName) && role.IsSelect)
                    await _userManager.AddToRoleAsync(user, role.RoleName);
            }

            return Json(new { success = true, notify = "Bạn đã cấp quyền thành công." });
        }
    }
}
