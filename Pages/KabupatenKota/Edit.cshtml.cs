using System.Linq;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Itm.Misc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DeputiTigaKemenpora.Pages.KabupatenKota
{
    public class EditModel : CustomPageModel
    {
        public EditModel(ApplicationDbContext context)
        {
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
            Title = "Ubah Kabupaten/Kota";
            PageTitle = "Kabupaten/Kota";
        }

        [BindProperty]
        public ViewModels.KabupatenKota KabupatenKota { get; set; } = new ViewModels.KabupatenKota();

        public SelectList Provinsi { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            KabupatenKota.Models = await _context.KabupatenKota
                .Include(k => k.Provinsi)
                .FirstOrDefaultAsync(m => m.Kode == id);

            if (KabupatenKota.Models == null)
            {
                return NotFound();
            }

            Provinsi = await selectListUtilities.Provinsi();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(KabupatenKota.Models).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KabupatenKotaExists(KabupatenKota.Kode))
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

        private bool KabupatenKotaExists(int id)
        {
            return _context.KabupatenKota.Any(e => e.Kode == id);
        }

        private readonly SelectListUtilities selectListUtilities;
        private readonly ApplicationDbContext _context;
    }
}