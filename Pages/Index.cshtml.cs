using System;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DeputiTigaKemenpora.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(
            ILogger<IndexModel> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            selectListUtilities = new SelectListUtilities(context);
        }

        public int Tahun { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Tahun = DateTime.Today.Year;
            ViewData["Tahun"] = await selectListUtilities.TahunSummaryKegiatan();

            return Page();
        }

        private readonly SelectListUtilities selectListUtilities;
        private readonly ILogger<IndexModel> _logger;
    }
}