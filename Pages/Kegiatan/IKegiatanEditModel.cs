using Microsoft.AspNetCore.Mvc.Rendering;

namespace DeputiTigaKemenpora.Pages.Kegiatan
{
   public interface IKegiatanEditModel
   {
      ViewModels.Kegiatan Kegiatan { get; set; }

      SelectList PenanggungJawab { get; set; }

      SelectList KabupatenKota { get; set; }

      SelectList SumberDana { get; set; }
   }
}
