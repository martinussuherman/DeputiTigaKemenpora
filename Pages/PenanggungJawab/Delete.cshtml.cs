using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeputiTigaKemenpora.Pages.PenanggungJawab
{
   [Authorize]
   public class DeleteModel : CustomPageModel
   {
      public DeleteModel(ApplicationDbContext context)
      {
         _context = context;
         Title = "Hapus Penanggung Jawab";
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

      public async Task<IActionResult> OnPostAsync(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         PenanggungJawab.Models = await _context.PenanggungJawab.FindAsync((ushort)id);

         if (PenanggungJawab.Models != null)
         {
            _context.PenanggungJawab.Remove(PenanggungJawab.Models);
            await _context.SaveChangesAsync();
         }

         return RedirectToPage("./Index");
      }

      private readonly ApplicationDbContext _context;
   }
}
