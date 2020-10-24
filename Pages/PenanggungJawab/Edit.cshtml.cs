using System;
using System.Linq;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DeputiTigaKemenpora.Pages.PenanggungJawab
{
    [Authorize]
    public class EditModel : CustomPageModel
    {
        public EditModel(
            ILogger<EditModel> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            Title = "Ubah Penanggung Jawab";
            PageTitle = "Penanggung Jawab";
        }

        [BindProperty]
        public ViewModels.PenanggungJawab PenanggungJawab { get; set; } = new ViewModels.PenanggungJawab();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PenanggungJawab.Models = await _context.PenanggungJawab
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (PenanggungJawab.Models == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PenanggungJawab.Models).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(PenanggungJawab.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool Exists(int id)
        {
            return _context.PenanggungJawab.Any(e => e.Id == id);
        }

        private readonly ILogger<EditModel> _logger;
        private readonly ApplicationDbContext _context;
    }
}