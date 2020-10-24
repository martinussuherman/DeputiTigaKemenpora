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
    public class EditModel : CustomPageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EditModel> _logger;
        private readonly IdentityDbContext _identityContext;
        private readonly SelectListUtilities selectListUtilities;

        public EditModel(
            UserManager<ApplicationUser> userManager,
            ILogger<EditModel> logger,
            ApplicationDbContext context,
            IdentityDbContext identityContext)
        {
            _userManager = userManager;
            _logger = logger;
            _identityContext = identityContext;
            selectListUtilities = new SelectListUtilities(context);
            Title = "Ubah User";
            PageTitle = "User";
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        {
            public int Id { get; set; }

            public string UserName { get; set; }

            [Required(ErrorMessage = "Role harus diisi.")]
            public string UserRole { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);

            Input.Id = user.Id;
            Input.UserName = user.UserName;
            Input.UserRole = roles[0];
            ViewData["UserRole"] = await selectListUtilities.UserRoles(_identityContext);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ApplicationUser user = new ApplicationUser();

            if (ModelState.IsValid)
            {
                user = await _userManager.FindByIdAsync(Input.Id.ToString());
            }

            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result = await _userManager.RemoveFromRolesAsync(
                user,
                await _userManager.GetRolesAsync(user));

            if (!LogSuccessAndError(result, "Remove old roles."))
            {
                return NotFound();
            }

            result = await _userManager.AddToRoleAsync(user, Input.UserRole);

            if (!LogSuccessAndError(result, "Changed user role."))
            {
                return NotFound();
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