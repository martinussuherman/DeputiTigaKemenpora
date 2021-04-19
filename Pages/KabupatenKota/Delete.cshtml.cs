using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Itm.Misc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeputiTigaKemenpora.Pages.KabupatenKota
{
   public class DeleteModel : CustomPageModel
   {
      public DeleteModel(ApplicationDbContext context)
      {
         _context = context;
         Title = "Hapus Kabupaten/Kota";
         PageTitle = "Kabupaten/Kota";
      }

      [BindProperty]
      public ViewModels.KabupatenKota KabupatenKota { get; set; } = new ViewModels.KabupatenKota();

      public async Task<IActionResult> OnGetAsync(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         KabupatenKota.Models = await _context.KabupatenKota
            .Include(k => k.Provinsi)
            .FirstOrDefaultAsync(m => m.Id == id);

         if (KabupatenKota.Models == null)
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

         KabupatenKota.Models = await _context.KabupatenKota.FindAsync(id);

         if (KabupatenKota.Models != null)
         {
            _context.KabupatenKota.Remove(KabupatenKota.Models);
            await _context.SaveChangesAsync();
         }

         return RedirectToPage("./Index");
      }

      private readonly ApplicationDbContext _context;
   }
}
