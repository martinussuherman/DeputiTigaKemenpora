using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeputiTigaKemenpora.Pages.SumberDana
{
   [Authorize]
   public class DeleteModel : CustomPageModel
   {
      public DeleteModel(ApplicationDbContext context)
      {
         _context = context;
         Title = "Hapus Sumber Dana";
         PageTitle = "Sumber Dana";
      }

      [BindProperty]
      public ViewModels.SumberDana SumberDana { get; set; } = new ViewModels.SumberDana();

      public async Task<IActionResult> OnGetAsync(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         SumberDana.Models = await _context.SumberDana
            .FirstOrDefaultAsync(m => m.Id == id);

         if (SumberDana.Models == null)
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

         SumberDana.Models = await _context.SumberDana.FindAsync(id);

         if (SumberDana.Models != null)
         {
            _context.SumberDana.Remove(SumberDana.Models);
            await _context.SaveChangesAsync();
         }

         return RedirectToPage("./Index");
      }

      private readonly ApplicationDbContext _context;
   }
}
