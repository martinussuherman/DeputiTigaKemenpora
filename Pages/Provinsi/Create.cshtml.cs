using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeputiTigaKemenpora.Pages.Provinsi
{
   [Authorize]
   public class CreateModel : CustomPageModel, IProvinsiEditModel
   {
      public CreateModel(ApplicationDbContext context)
      {
         _context = context;
         Title = "Tambah Provinsi";
         PageTitle = "Provinsi";
      }

      [BindProperty]
      public ViewModels.Provinsi Provinsi { get; set; }

      public IActionResult OnGet()
      {
         return Page();
      }

      public async Task<IActionResult> OnPostAsync()
      {
         if (!ModelState.IsValid)
         {
            return Page();
         }

         _context.Provinsi.Add(Provinsi.Models);
         await _context.SaveChangesAsync();

         return RedirectToPage("./Index");
      }

      private readonly ApplicationDbContext _context;
   }
}
