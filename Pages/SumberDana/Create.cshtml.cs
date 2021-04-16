using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeputiTigaKemenpora.Pages.SumberDana
{
   [Authorize]
   public class CreateModel : CustomPageModel, ISumberDanaEditModel
   {
      public CreateModel(ApplicationDbContext context)
      {
         _context = context;
         Title = "Tambah Sumber Dana";
         PageTitle = "Sumber Dana";
      }

      [BindProperty]
      public ViewModels.SumberDana SumberDana { get; set; }

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

         _context.SumberDana.Add(SumberDana.Models);
         await _context.SaveChangesAsync();

         return RedirectToPage("./Index");
      }

      private readonly ApplicationDbContext _context;
   }
}
