using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using DeputiTigaKemenpora.Identity;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeputiTigaKemenpora.Pages.Kegiatan
{
   [Authorize(Permissions.Kegiatan.Edit)]
   public class EditModel : CustomPageModel, IKegiatanEditModel
   {
      public EditModel(ApplicationDbContext context)
      {
         _context = context;
         selectListUtilities = new SelectListUtilities(context);
         Title = "Ubah Kegiatan";
         PageTitle = "Kegiatan";
      }

      [BindProperty]
      public ViewModels.Kegiatan Kegiatan { get; set; } = new ViewModels.Kegiatan();

      public List<Models.KabupatenKota> KabupatenKotaList { get; set; }

      public List<Models.PenanggungJawab> PenanggungJawabList { get; set; }

      public List<Models.SumberDana> SumberDanaList { get; set; }

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

         KabupatenKotaList = await selectListUtilities.KabupatenKotaList();
         PenanggungJawabList = await selectListUtilities.PenanggungJawabList();
         SumberDanaList = await selectListUtilities.SumberDanaList();

         return Page();
      }

      public async Task<IActionResult> OnPostAsync()
      {
         if (!ModelState.IsValid)
         {
            KabupatenKotaList = await selectListUtilities.KabupatenKotaList();
            PenanggungJawabList = await selectListUtilities.PenanggungJawabList();
            SumberDanaList = await selectListUtilities.SumberDanaList();
            return Page();
         }

         _context.Attach(Kegiatan.Models).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!Exists(Kegiatan.Id))
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

      private bool Exists(uint id)
      {
         return _context.Kegiatan.Any(e => e.Id == id);
      }

      private readonly SelectListUtilities selectListUtilities;
      private readonly ApplicationDbContext _context;
   }
}
