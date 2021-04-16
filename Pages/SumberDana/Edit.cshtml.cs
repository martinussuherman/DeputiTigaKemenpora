using System.Linq;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeputiTigaKemenpora.Pages.SumberDana
{
   [Authorize]
   public class EditModel : CustomPageModel, ISumberDanaEditModel
   {
      public EditModel(ApplicationDbContext context)
      {
         _context = context;
         Title = "Ubah Sumber Dana";
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

      public async Task<IActionResult> OnPostAsync()
      {
         if (!ModelState.IsValid)
         {
            return Page();
         }

         _context.Attach(SumberDana.Models).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!Exists(SumberDana.Id))
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
         return _context.SumberDana.Any(e => e.Id == id);
      }

      private readonly ApplicationDbContext _context;
   }
}
