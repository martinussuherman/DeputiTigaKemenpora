using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Itm.Misc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeputiTigaKemenpora.Pages.KabupatenKota
{
   public class EditModel : CustomPageModel, IKabupatenKotaEditModel
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

      public List<Models.Provinsi> ProvinsiList { get; set; }

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

         ProvinsiList = await selectListUtilities.ProvinsiList();
         return Page();
      }

      public async Task<IActionResult> OnPostAsync()
      {
         if (!ModelState.IsValid)
         {
            ProvinsiList = await selectListUtilities.ProvinsiList();
            return Page();
         }

         _context.Attach(KabupatenKota.Models).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!KabupatenKotaExists(KabupatenKota.Id))
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
         return _context.KabupatenKota.Any(e => e.Id == id);
      }

      private readonly SelectListUtilities selectListUtilities;
      private readonly ApplicationDbContext _context;
   }
}
