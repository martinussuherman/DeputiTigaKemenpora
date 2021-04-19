using System.Collections.Generic;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using DeputiTigaKemenpora.ViewModels;
using Itm.Misc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P.Pager;

namespace DeputiTigaKemenpora.Pages.PenanggungJawab
{
   public class IndexModel : CustomPageModel
   {
      public IndexModel(ApplicationDbContext context)
      {
         _context = context;
         Title = "Daftar Penanggung Jawab";
         PageTitle = "Penanggung Jawab";
      }

      public IPager<ViewModels.PenanggungJawab> PenanggungJawab { get; set; }

      public async Task OnGetAsync([FromQuery] int page = 1)
      {
         List<Models.PenanggungJawab> list = await _context.PenanggungJawab
            .AsNoTracking()
            .ToListAsync();
         PenanggungJawab = list
            .ViewModelCopy<ViewModels.PenanggungJawab, Models.PenanggungJawab>(page);
      }

      private readonly ApplicationDbContext _context;
   }
}
