using System.Collections.Generic;

namespace DeputiTigaKemenpora.Pages.Kegiatan
{
   public interface IKegiatanEditModel
   {
      ViewModels.Kegiatan Kegiatan { get; set; }

      List<Models.KabupatenKota> KabupatenKotaList { get; set; }

      List<Models.PenanggungJawab> PenanggungJawabList { get; set; }

      List<Models.SumberDana> SumberDanaList { get; set; }
   }
}
