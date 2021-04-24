using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using DeputiTigaKemenpora.Models;
using Itm.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DeputiTigaKemenpora
{
   public class SelectListUtilities
   {
      public SelectListUtilities(ApplicationDbContext context)
      {
         _context = context;
      }

      public async Task<List<PenanggungJawab>> PenanggungJawabList()
      {
         return await _context.PenanggungJawab
            .Where(p => p.Id > 0)
            .OrderBy(p => p.Nama)
            .AsNoTracking()
            .ToListAsync();
      }

      public async Task<List<SumberDana>> SumberDanaList()
      {
         return await _context.SumberDana
            .Where(p => p.Id > 0)
            .OrderBy(p => p.Nama)
            .AsNoTracking()
            .ToListAsync();
      }

      public async Task<List<Provinsi>> ProvinsiList()
      {
         return await _context.Provinsi
            .Where(p => p.Id > 0)
            .OrderBy(p => p.Nama)
            .AsNoTracking()
            .ToListAsync();
      }

      public async Task<List<KabupatenKota>> KabupatenKotaList()
      {
         return await _context.KabupatenKota
            .Where(p => p.Id > 0)
            .OrderBy(p => p.Nama)
            .AsNoTracking()
            .ToListAsync();
      }

      public async Task<SelectList> UserRoles(IdentityDbContext context)
      {
         List<ApplicationRole> list = await context.Roles
            .OrderBy(e => e.Name)
            .AsNoTracking()
            .ToListAsync();

         return new SelectList(list, "Name", "Name");
      }

      public async Task<SelectList> TahunSummaryKegiatan()
      {
         List<int> list = await _context.Kegiatan
            .OrderByDescending(e => e.TanggalMulai.Year)
            .AsNoTracking()
            .Select(e => e.TanggalMulai.Year)
            .Distinct()
            .ToListAsync();

         return new SelectList(list);
      }

      private readonly ApplicationDbContext _context;
   }
}
