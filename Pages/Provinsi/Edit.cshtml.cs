using System.Linq;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeputiTigaKemenpora.Pages.Provinsi
{
   [Authorize]
   public class EditModel : CustomPageModel, IProvinsiEditModel
   {
      public EditModel(ApplicationDbContext context)
      {
         _context = context;
         Title = "Ubah Provinsi";
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
            .FirstOrDefaultAsync(m => m.Kode == id);

         if (Provinsi.Models == null)
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

         _context.Attach(Provinsi.Models).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!Exists(Provinsi.Kode))
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
         return _context.Provinsi.Any(e => e.Kode == id);
      }

      private readonly ApplicationDbContext _context;
   }
}
