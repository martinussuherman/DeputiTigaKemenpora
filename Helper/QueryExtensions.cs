using System.Linq;
using DeputiTigaKemenpora.Models;

namespace DeputiTigaKemenpora.Helper
{
   public static class QueryExtensions
   {
      public static IQueryable<Kegiatan> KegiatanByTahun(
          this IQueryable<Kegiatan> query,
          int tahun)
      {
         return tahun == 0 ? query : query.Where(e => e.TanggalMulai.Year == tahun);
      }
   }
}
