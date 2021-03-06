using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using DeputiTigaKemenpora.ViewModels;
using Itm.Misc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeputiTigaKemenpora.Pages.Ajax
{
   public class FilterSummaryModel : CustomPageModel
   {
      public FilterSummaryModel(ApplicationDbContext context)
      {
         _context = context;
         Title = "Filter Summary";
      }

      public async Task<JsonResult> OnGetAsync(int tahun)
      {
         IQueryable<Models.Kegiatan> query = _context.Kegiatan;

         if (tahun != 0)
         {
            query = query
               .Where(e => e.TanggalMulai.Year == tahun);
         }

         List<SummaryKegiatan> sumInfo = await query
            .GroupBy(e => new
            {
               e.Nama,
               e.SumberDanaId
            })
            .Select(s => new SummaryKegiatan
            {
               Nama = s.Key.Nama,
               IdSumberDana = s.Key.SumberDanaId,
               JumlahKegiatan = s.Count(),
               TotalPeserta = s.Sum(e => e.JumlahPeserta)
            })
            .AsNoTracking()
            .ToListAsync();

         List<Models.SumberDana> listSumberDana = await _context.SumberDana
            .OrderBy(e => e.Nama)
            .AsNoTracking()
            .ToListAsync();

         foreach (SummaryKegiatan item in sumInfo)
         {
            item.NamaSumberDana = listSumberDana
               .FirstOrDefault(e => e.Id == item.IdSumberDana)?.Nama;
         }

         SummaryView result = new SummaryView
         {
            Summary = sumInfo
               .OrderBy(e => e.Nama)
               .ThenBy(e => e.NamaSumberDana),
            GrandTotalKegiatan = sumInfo.Sum(e => e.JumlahKegiatan),
            GrandTotalPeserta = sumInfo.Sum(e => e.TotalPeserta)
         };

         return new JsonResult(result);
      }

      public class SummaryView
      {
         public IOrderedEnumerable<SummaryKegiatan> Summary { get; set; }

         public int GrandTotalKegiatan { get; set; }

         public long GrandTotalPeserta { get; set; }

         public string GrandTotalKegiatanView
         {
            get => GrandTotalKegiatan.ToString("##,#", info);
         }

         public string GrandTotalPesertaView
         {
            get => GrandTotalPeserta.ToString("##,#", info);
         }

         private static readonly CultureInfo info = CultureInfo.CreateSpecificCulture("id-ID");
      }

      private readonly ApplicationDbContext _context;
   }
}
