using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using DeputiTigaKemenpora.Identity;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DeputiTigaKemenpora.Pages.Kegiatan
{
   [Authorize(Permissions.Kegiatan.Create)]
   public class CreateModel : CustomPageModel
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

      public SelectList PenanggungJawab { get; set; }

      public SelectList KabupatenKota { get; set; }

      public SelectList SumberDana { get; set; }

      public async Task<IActionResult> OnGetAsync()
      {
         PenanggungJawab = await selectListUtilities.PenanggungJawab();
         KabupatenKota = await selectListUtilities.KabupatenKota();
         SumberDana = await selectListUtilities.SumberDana();
         return Page();
      }

      public async Task<IActionResult> OnPostAsync()
      {
         if (!ModelState.IsValid)
         {
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
