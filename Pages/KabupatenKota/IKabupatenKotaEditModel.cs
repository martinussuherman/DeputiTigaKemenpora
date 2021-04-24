using System.Collections.Generic;

namespace DeputiTigaKemenpora.Pages.KabupatenKota
{
   public interface IKabupatenKotaEditModel
   {
      ViewModels.KabupatenKota KabupatenKota { get; set; }

      List<Models.Provinsi> ProvinsiList { get; set; }
   }
}
