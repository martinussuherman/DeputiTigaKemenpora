using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeputiTigaKemenpora.Pages.PenanggungJawab
{
   [Authorize]
   public class CreateModel : CustomPageModel, IPenanggungJawabEditModel
   {
      public CreateModel(ApplicationDbContext context)
      {
         _context = context;
         Title = "Tambah Penanggung Jawab";
         PageTitle = "Penanggung Jawab";
      }

      [BindProperty]
      public ViewModels.PenanggungJawab PenanggungJawab { get; set; }

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

         _context.PenanggungJawab.Add(PenanggungJawab.Models);
         await _context.SaveChangesAsync();

         return RedirectToPage("./Index");
      }

      private readonly ApplicationDbContext _context;
   }
}
