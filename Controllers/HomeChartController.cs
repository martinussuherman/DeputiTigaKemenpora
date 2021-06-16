using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using DeputiTigaKemenpora.Helper;
using DeputiTigaKemenpora.ViewModels;
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
      public async Task<List<ChartData>> SummaryPendanaan([FromQuery] int tahun)
      {
         List<ChartData> list = await _context.Kegiatan
            .KegiatanByTahun(tahun)
            .Include(e => e.SumberDana)
            .Where(e => e.SumberDanaId != null)
            .GroupBy(e => new
            {
               e.SumberDanaId,
               e.SumberDana.Nama
            })
            .Select(r => new ChartData
            {
               Id = (int)r.Key.SumberDanaId,
               Nama = r.Key.Nama,
               JumlahKegiatan = r.Count()
            })
            .ToListAsync();

         return list;
      }

      [HttpGet(nameof(PesertaBerdasarkanPenanggungJawab))]
      [ProducesResponseType(StatusCodes.Status200OK)]
      public async Task<List<ChartData>> PesertaBerdasarkanPenanggungJawab([FromQuery] int tahun)
      {
         List<ChartData> list = await _context.Kegiatan
            .KegiatanByTahun(tahun)
            .Include(e => e.PenanggungJawab)
            .Where(e => e.PenanggungJawabId != null)
            .GroupBy(e => new
            {
               e.PenanggungJawabId,
               e.PenanggungJawab.Nama
            })
            .Select(r => new ChartData
            {
               Id = (int)r.Key.PenanggungJawabId,
               Nama = r.Key.Nama,
               JumlahKegiatan = r.Count(),
               JumlahPeserta = r.Sum(e => e.JumlahPeserta)
            })
            .ToListAsync();

         return list;
      }

      [HttpGet(nameof(PesertaBerdasarkanTahun))]
      [ProducesResponseType(StatusCodes.Status200OK)]
      public async Task<List<ChartData>> PesertaBerdasarkanTahun([FromQuery] int kodeProvinsi)
      {
         List<ChartData> list = await _context.Kegiatan
            .KegiatanByProvinsi(kodeProvinsi)
            .Where(e => e.KabupatenKotaId != null)
            .GroupBy(e => new
            {
               e.TanggalMulai.Year
            })
            .Select(r => new ChartData
            {
               Id = r.Key.Year,
               Nama = r.Key.Year.ToString(),
               JumlahKegiatan = r.Count(),
               JumlahPeserta = r.Sum(e => e.JumlahPeserta)
            })
            .ToListAsync();

         return list;
      }

      [HttpGet(nameof(PesertaBerdasarkanProvinsi))]
      [ProducesResponseType(StatusCodes.Status200OK)]
      public async Task<List<ChartData>> PesertaBerdasarkanProvinsi()
      {
         List<ChartData> list = await _context.Kegiatan
            .Where(e => e.KabupatenKotaId != null)
            .GroupBy(e => new
            {
               e.KabupatenKota.ProvinsiId,
               e.KabupatenKota.Provinsi.Nama,
            })
            .Select(r => new ChartData
            {
               Id = (int)r.Key.ProvinsiId,
               Nama = r.Key.Nama,
               JumlahKegiatan = r.Count(),
               JumlahPeserta = r.Sum(e => e.JumlahPeserta)
            })
            .ToListAsync();

         return list;
      }

      private readonly ApplicationDbContext _context;
   }
}
