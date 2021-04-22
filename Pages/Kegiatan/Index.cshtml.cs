using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using DeputiTigaKemenpora.Identity;
using DeputiTigaKemenpora.ViewModels;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P.Pager;

namespace DeputiTigaKemenpora.Pages.Kegiatan
{
   public class IndexModel : CustomPageModel, IKegiatanIndexModel
   {
      public IndexModel(
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

      public async Task OnGetAsync([FromQuery] int page = 1)
      {
         List<Models.Kegiatan> list = await _context.Kegiatan
            .Include(e => e.PenanggungJawabNavigation)
            .OrderBy(e => e.PenanggungJawab)
            .ThenBy(e => e.Nama)
            .ThenBy(e => e.Tempat)
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
