using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeputiTigaKemenpora.Pages.Kegiatan
{
   public class ByProvinsiModel : KegiatanIndexModel
   {
      public ByProvinsiModel(
         IAuthorizationService authorizationService,
         ApplicationDbContext context)
         : base(authorizationService, context)
      {
      }

      public async Task OnGetAsync([FromQuery] byte? prov, [FromQuery] int page = 1)
      {
         if (prov == null)
         {
            await RetrieveData(page);
            return;
         }

         await RetrieveData(prov.Value, page);
      }
   }
}
