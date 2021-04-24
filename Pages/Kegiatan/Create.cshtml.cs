using System.Collections.Generic;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using DeputiTigaKemenpora.Identity;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeputiTigaKemenpora.Pages.Kegiatan
{
   [Authorize(Permissions.Kegiatan.Create)]
   public class CreateModel : CustomPageModel, IKegiatanEditModel
   {
      public CreateModel(ApplicationDbContext context)
      {
         _context = context;
         selectListUtilities = new SelectListUtilities(context);
         Title = "Tambah Kegiatan";
         PageTitle = "Kegiatan";
      }

      [BindProperty]
      public ViewModels.Kegiatan Kegiatan { get; set; } = new ViewModels.Kegiatan();

      public List<Models.KabupatenKota> KabupatenKotaList { get; set; }

      public List<Models.PenanggungJawab> PenanggungJawabList { get; set; }

      public List<Models.SumberDana> SumberDanaList { get; set; }

      public async Task<IActionResult> OnGetAsync()
      {
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

         _context.Kegiatan.Add(Kegiatan.Models);
         await _context.SaveChangesAsync();
         return RedirectToPage("./Index");
      }

      private readonly ApplicationDbContext _context;
      private readonly SelectListUtilities selectListUtilities;
   }
}
