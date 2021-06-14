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
               JumlahKegiatan = r.Count()
            })
            .ToListAsync();

         return Ok(list);
      }

      [HttpGet(nameof(PesertaBerdasarkanPenanggungJawab))]
      [ProducesResponseType(StatusCodes.Status200OK)]
      public async Task<IActionResult> PesertaBerdasarkanPenanggungJawab([FromQuery] int tahun)
      {
         var list = await _context.Kegiatan
            .KegiatanByTahun(tahun)
            .Include(e => e.PenanggungJawab)
            .Where(e => e.PenanggungJawabId != null)
            .GroupBy(e => new
            {
               e.PenanggungJawabId,
               e.PenanggungJawab.Nama
            })
            .Select(r => new
            {
               Id = r.Key.PenanggungJawabId,
               Nama = r.Key.Nama,
               JumlahKegiatan = r.Count(),
               JumlahPeserta = r.Sum(e => e.JumlahPeserta)
            })
            .ToListAsync();

         return Ok(list);
      }

      [HttpGet(nameof(PesertaBerdasarkanTahun))]
      [ProducesResponseType(StatusCodes.Status200OK)]
      public async Task<IActionResult> PesertaBerdasarkanTahun([FromQuery] int kodeProvinsi)
      {
         var list = await _context.Kegiatan
            .KegiatanByProvinsi(kodeProvinsi)
            .Where(e => e.KabupatenKotaId != null)
            .GroupBy(e => new
            {
               e.TanggalMulai.Year
            })
            .Select(r => new
            {
               Tahun = r.Key.Year,
               JumlahKegiatan = r.Count(),
               JumlahPeserta = r.Sum(e => e.JumlahPeserta)
            })
            .ToListAsync();

         return Ok(list);
      }

      private readonly ApplicationDbContext _context;
   }
}
