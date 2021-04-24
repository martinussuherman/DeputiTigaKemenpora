using System.Collections.Generic;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeputiTigaKemenpora.Pages.KabupatenKota
{
   [Authorize]
   public class CreateModel : CustomPageModel, IKabupatenKotaEditModel
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

      public List<Models.Provinsi> ProvinsiList { get; set; }

      public async Task<IActionResult> OnGetAsync()
      {
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

         _context.KabupatenKota.Add(KabupatenKota.Models);
         await _context.SaveChangesAsync();

         return RedirectToPage("./Index");
      }

      private readonly SelectListUtilities selectListUtilities;
      private readonly ApplicationDbContext _context;
   }
}
