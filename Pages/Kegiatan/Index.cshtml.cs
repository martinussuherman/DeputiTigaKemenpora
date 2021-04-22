using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeputiTigaKemenpora.Pages.Kegiatan
{
   public class IndexModel : KegiatanIndexModel
   {
      public IndexModel(
         IAuthorizationService authorizationService,
         ApplicationDbContext context)
         : base(authorizationService, context)
      {
      }

      public async Task OnGetAsync([FromQuery] int page = 1)
      {
         await RetrieveData(page);
      }
   }
}
