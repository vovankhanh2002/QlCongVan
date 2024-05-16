// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DataLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WS_QuanLyCongVan.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<Tb_Nguoidung> _userManager;
        private readonly SignInManager<Tb_Nguoidung> _signInManager;

        public IndexModel(
            UserManager<Tb_Nguoidung> userManager,
            SignInManager<Tb_Nguoidung> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            ///
            [Required]
            [StringLength(100, ErrorMessage = "{0} phải dài ít nhất là {2} và tối đa {1} ký tự.", MinimumLength = 6)]
            [Display(Name = "Họ tên")]
            public string Hoten_NV { get; set; }

            [Required(ErrorMessage = "Bạn cần nhập đầy đủ địa chỉ.")]
            public string? DiaChi_NV { get; set; }

            [Required(ErrorMessage = "Bạn cần nhập đầy đủ thông tin.")]
            [Display(Name = "Ngày sinh")]
            public DateTime NgaySinh_NV { get; set; }

            [Phone]
            [Display(Name = "Số điện thoại")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Hình đại diện")]
            public byte[] Hinh { get; set; }
        }

        private async Task LoadAsync(Tb_Nguoidung user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Hoten_NV = user.Hoten_NV,
                DiaChi_NV = user.DiaChi_NV,
                NgaySinh_NV = user.NgaySinh_NV,
                Hinh = user.Hinh,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var hoten_nv = user.Hoten_NV;
            var diachi_nv = user.DiaChi_NV;
            var ngaysinh_nv = user.NgaySinh_NV;

            if(Input.Hoten_NV != hoten_nv)
            {
                user.Hoten_NV = Input.Hoten_NV;
                await _userManager.UpdateAsync(user);
            }
            if (Input.DiaChi_NV != diachi_nv)
            {
                user.DiaChi_NV = Input.DiaChi_NV;
                await _userManager.UpdateAsync(user);
            }
            if (Input.NgaySinh_NV != ngaysinh_nv)
            {
                user.NgaySinh_NV = Input.NgaySinh_NV;
                await _userManager.UpdateAsync(user);
            }
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Lỗi không mong muốn khi cố gắng đặt số điện thoại.";
                    return RedirectToPage();
                }
            }
            if (Request.Form.Files.Count() > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();
                using (var datastream = new MemoryStream())
                {
                    await file.CopyToAsync(datastream);
                    user.Hinh = datastream.ToArray();
                }
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Hồ sơ của bạn đã được cập nhật";
            return RedirectToPage();
        }
    }
}
