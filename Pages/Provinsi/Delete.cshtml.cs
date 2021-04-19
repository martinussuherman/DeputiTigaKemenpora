using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeputiTigaKemenpora.Pages.Provinsi
{
   [Authorize]
   public class DeleteModel : CustomPageModel
   {
      public DeleteModel(ApplicationDbContext context)
      {
         _context = context;
         Title = "Hapus Provinsi";
         PageTitle = "Provinsi";
      }

      [BindProperty]
      public ViewModels.Provinsi Provinsi { get; set; } = new ViewModels.Provinsi();

      public async Task<IActionResult> OnGetAsync(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         Provinsi.Models = await _context.Provinsi
            .FirstOrDefaultAsync(m => m.Id == id);

         if (Provinsi.Models == null)
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

         Provinsi.Models = await _context.Provinsi.FindAsync(id);

         if (Provinsi.Models != null)
         {
            _context.Provinsi.Remove(Provinsi.Models);
            await _context.SaveChangesAsync();
         }

         return RedirectToPage("./Index");
      }

      private readonly ApplicationDbContext _context;
   }
}
