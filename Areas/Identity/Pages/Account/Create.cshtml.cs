using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using DeputiTigaKemenpora.Identity;
using Itm.Identity;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeputiTigaKemenpora.Areas.Identity.Pages.Account
{
    [Authorize(Permissions.Users.All)]
    public class CreateModel : CustomPageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CreateModel> _logger;
        private readonly IdentityDbContext _identityContext;
        private readonly SelectListUtilities selectListUtilities;

        public CreateModel(
            UserManager<ApplicationUser> userManager,
            ILogger<CreateModel> logger,
            ApplicationDbContext context,
            IdentityDbContext identityContext)
        {
            _userManager = userManager;
            _logger = logger;
            _identityContext = identityContext;
            selectListUtilities = new SelectListUtilities(context);
            Title = "Tambah User";
            PageTitle = "User";
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "User harus diisi.")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Password harus diisi.")]
            [StringLength(100, ErrorMessage = "{0} minimal {2} dan maksimal {1} karakter.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Password and konfirmasi password tidak sama.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Role harus diisi.")]
            public string UserRole { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["UserRole"] = await selectListUtilities.UserRoles(_identityContext);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = Input.UserName,
                    Email = String.Empty
                };

                IdentityResult result = await _userManager.CreateAsync(user, Input.Password);

                if (!LogSuccessAndError(result, "User created a new account with password."))
                {
                    return await OnGetAsync();
                }

                result = await _userManager.AddToRoleAsync(user, Input.UserRole);

                if (!LogSuccessAndError(result, "User added to role."))
                {
                    return await OnGetAsync();
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LogSuccessAndError(IdentityResult result, string logMessage)
        {
            if (result.Succeeded)
            {
                _logger.LogInformation(logMessage);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return result.Succeeded;
        }
    }
}