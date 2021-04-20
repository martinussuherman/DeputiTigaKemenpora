using Microsoft.AspNetCore.Mvc.Rendering;

namespace DeputiTigaKemenpora.Pages.KabupatenKota
{
   public interface IKabupatenKotaEditModel
   {
      ViewModels.KabupatenKota KabupatenKota { get; set; }

      SelectList Provinsi { get; set; }
   }
}
