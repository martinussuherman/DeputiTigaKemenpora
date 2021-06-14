using System.Linq;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using DeputiTigaKemenpora.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeputiTigaKemenpora.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class HomeChartController : ControllerBase
   {
      public HomeChartController(ApplicationDbContext context)
      {
         _context = context;
      }

      [HttpGet(nameof(SummaryPendanaan))]
      [ProducesResponseType(StatusCodes.Status200OK)]
      public async Task<IActionResult> SummaryPendanaan([FromQuery] int tahun)
      {
         var list = await _context.Kegiatan
            .KegiatanByTahun(tahun)
            .Include(e => e.SumberDana)
            .Where(e => e.SumberDanaId != null)
            .GroupBy(e => new
            {
               e.SumberDanaId,
               e.SumberDana.Nama
            })
            .Select(r => new
            {
               Id = r.Key.SumberDanaId,
               Nama = r.Key.Nama,
               Jumlah = r.Count()
            })
            .ToListAsync();

         return Ok(list);
      }

      private readonly ApplicationDbContext _context;
   }
}
