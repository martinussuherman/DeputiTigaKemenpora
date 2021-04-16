using System.Collections.Generic;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using DeputiTigaKemenpora.ViewModels;
using Itm.Misc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P.Pager;

namespace DeputiTigaKemenpora.Pages.SumberDana
{
   public class IndexModel : CustomPageModel
   {
      public IndexModel(ApplicationDbContext context)
      {
         _context = context;
         Title = "Daftar Sumber Dana";
         PageTitle = "Sumber Dana";
      }

      public IPager<ViewModels.SumberDana> SumberDana { get; set; }

      public async Task OnGetAsync([FromQuery] int page = 1)
      {
         List<Models.SumberDana> list = await _context.SumberDana
            .AsNoTracking()
            .ToListAsync();
         SumberDana = list
            .ViewModelCopy<ViewModels.SumberDana, Models.SumberDana>(page);
      }

      private readonly ApplicationDbContext _context;
   }
}
