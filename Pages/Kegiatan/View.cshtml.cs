using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Itm.Misc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeputiTigaKemenpora.Pages.Kegiatan
{
   public class ViewModel : CustomPageModel
   {
      public ViewModel(ApplicationDbContext context)
      {
         _context = context;
         Title = "Lihat Kegiatan";
         PageTitle = "Kegiatan";
      }

      [BindProperty]
      public ViewModels.Kegiatan Kegiatan { get; set; } = new ViewModels.Kegiatan();

      public async Task<IActionResult> OnGetAsync(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         Kegiatan.Models = await _context.Kegiatan
            .Include(e => e.PenanggungJawabNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

         if (Kegiatan.Models == null)
         {
            return NotFound();
         }

         return Page();
      }

      private readonly ApplicationDbContext _context;
   }
}
