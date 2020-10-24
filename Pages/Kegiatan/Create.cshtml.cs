using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using DeputiTigaKemenpora.Identity;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeputiTigaKemenpora.Pages.Kegiatan
{
    [Authorize(Permissions.Kegiatan.Create)]
    public class CreateModel : CustomPageModel
    {
        public CreateModel(
            ILogger<CreateModel> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
            Title = "Tambah Kegiatan";
            PageTitle = "Kegiatan";
        }

        [BindProperty]
        public ViewModels.Kegiatan Kegiatan { get; set; } = new ViewModels.Kegiatan();

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["PenanggungJawab"] = await selectListUtilities.PenanggungJawab();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Kegiatan.Add(Kegiatan.Models);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateModel> _logger;
        private readonly SelectListUtilities selectListUtilities;
    }
}