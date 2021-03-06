using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using DeputiTigaKemenpora.Identity;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeputiTigaKemenpora.Pages.Kegiatan
{
   [Authorize(Permissions.Kegiatan.Delete)]
   public class DeleteModel : CustomPageModel
   {
      public DeleteModel(ApplicationDbContext context)
      {
         _context = context;
         Title = "Hapus Kegiatan";
         PageTitle = "Kegiatan";
      }

      [BindProperty]
      public ViewModels.Kegiatan Kegiatan { get; set; } = new ViewModels.Kegiatan();

      public async Task<IActionResult> OnGetAsync(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         Kegiatan.Models = await _context.Kegiatan
            .Include(e => e.PenanggungJawab)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

         if (Kegiatan.Models == null)
         {
            return NotFound();
         }

         return Page();
      }

      public async Task<IActionResult> OnPostAsync(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         Kegiatan.Models = await _context.Kegiatan.FindAsync((uint)id);

         if (Kegiatan.Models != null)
         {
            _context.Kegiatan.Remove(Kegiatan.Models);
            await _context.SaveChangesAsync();
         }

         return RedirectToPage("./Index");
      }

      private readonly ApplicationDbContext _context;
   }
}
