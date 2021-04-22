using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using DeputiTigaKemenpora.Identity;
using DeputiTigaKemenpora.ViewModels;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using P.Pager;

namespace DeputiTigaKemenpora.Pages.Kegiatan
{
   public class KegiatanIndexModel : CustomPageModel, IKegiatanIndexModel
   {
      public KegiatanIndexModel(
          IAuthorizationService authorizationService,
          ApplicationDbContext context)
      {
         _authorizationService = authorizationService;
         _context = context;
         Title = "Daftar Kegiatan";
         PageTitle = "Kegiatan";
      }

      public IPager<ViewModels.Kegiatan> Kegiatan { get; set; }

      public bool IsUserCanDelete { get; set; }

      public async Task RetrieveData(int page, int provinsiId = -1)
      {
         IIncludableQueryable<Models.Kegiatan, Models.Provinsi> query = _context.Kegiatan
            .Include(e => e.PenanggungJawabNavigation)
            .Include(e => e.SumberDanaNavigation)
            .Include(e => e.KabupatenKota.Provinsi);

         if (provinsiId != -1)
         {
            query = (IIncludableQueryable<Models.Kegiatan, Models.Provinsi>)query
               .Where(e => e.KabupatenKota.ProvinsiId == provinsiId);
         }

         List<Models.Kegiatan> list = await query
            .OrderBy(e => e.TanggalMulai)
            .ThenBy(e => e.KabupatenKota.Nama)
            .ThenBy(e => e.Nama)
            .AsNoTracking()
            .ToListAsync();

         Kegiatan = list
            .ViewModelCopy<ViewModels.Kegiatan, Models.Kegiatan>(page);
         AuthorizationResult result = _authorizationService.AuthorizeAsync(
            User,
            Permissions.Kegiatan.Delete).Result;
         IsUserCanDelete = result.Succeeded;
      }

      private readonly IAuthorizationService _authorizationService;
      private readonly ApplicationDbContext _context;
   }
}
