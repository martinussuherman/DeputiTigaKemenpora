using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DeputiTigaKemenpora.Pages.KabupatenKota
{
   [Authorize]
   public class CreateModel : CustomPageModel
   {
      public CreateModel(ApplicationDbContext context)
      {
         _context = context;
         selectListUtilities = new SelectListUtilities(context);
         Title = "Tambah Kabupaten/Kota";
         PageTitle = "Kabupaten/Kota";
      }

      [BindProperty]
      public ViewModels.KabupatenKota KabupatenKota { get; set; }

      public SelectList Provinsi { get; set; }

      public async Task<IActionResult> OnGetAsync()
      {
         Provinsi = await selectListUtilities.Provinsi();
         return Page();
      }

      public async Task<IActionResult> OnPostAsync()
      {
         if (!ModelState.IsValid)
         {
            Provinsi = await selectListUtilities.Provinsi();
            return Page();
         }

         _context.KabupatenKota.Add(KabupatenKota.Models);
         await _context.SaveChangesAsync();

         return RedirectToPage("./Index");
      }

      private readonly SelectListUtilities selectListUtilities;
      private readonly ApplicationDbContext _context;
   }
}
