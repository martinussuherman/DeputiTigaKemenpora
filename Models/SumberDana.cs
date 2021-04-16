using System.Collections.Generic;

namespace DeputiTigaKemenpora.Models
{
   public partial class SumberDana
   {
      public SumberDana()
      {
         Kegiatan = new HashSet<Kegiatan>();
      }

      public byte Id { get; set; }
      public string Nama { get; set; }

      public virtual ICollection<Kegiatan> Kegiatan { get; set; }
   }
}
