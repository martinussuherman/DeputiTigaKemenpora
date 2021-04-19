using System.Collections.Generic;
using System.Linq;
using DeputiTigaKemenpora.Data;
using DeputiTigaKemenpora.ViewModels;
using Itm.Misc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P.Pager;

namespace DeputiTigaKemenpora.Pages.KabupatenKota
{
   public class IndexModel : CustomPageModel
   {
      public IndexModel(ApplicationDbContext context)
      {
         _context = context;
         Title = "Daftar Kabupaten/Kota";
         PageTitle = "Kabupaten/Kota";
      }

      public IPager<ViewModels.KabupatenKota> KabupatenKota { get; set; }

      public async System.Threading.Tasks.Task OnGetAsync([FromQuery] int page)
      {
         List<Models.KabupatenKota> list = await _context.KabupatenKota
             .Include(k => k.Provinsi)
             .OrderBy(k => k.Provinsi.Nama)
             .ThenBy(k => k.Nama)
             .AsNoTracking()
             .ToListAsync();
         KabupatenKota = list
             .ViewModelCopy<ViewModels.KabupatenKota, Models.KabupatenKota>(page);
      }

      private readonly ApplicationDbContext _context;
   }
}
