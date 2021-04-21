using P.Pager;

namespace DeputiTigaKemenpora.Pages.Kegiatan
{
   public interface IKegiatanIndexModel
   {
      IPager<ViewModels.Kegiatan> Kegiatan { get; set; }

      bool IsUserCanDelete { get; set; }
   }
}
