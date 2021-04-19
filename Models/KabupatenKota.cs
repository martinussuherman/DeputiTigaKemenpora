using System.Collections.Generic;

namespace DeputiTigaKemenpora.Models
{
   public partial class KabupatenKota
   {
      public KabupatenKota()
      {
         Kegiatan = new HashSet<Kegiatan>();
      }

      public ushort Id { get; set; }
      public string Nama { get; set; }
      public byte? ProvinsiId { get; set; }
      public decimal Lat { get; set; }
      public decimal Long { get; set; }

      public virtual Provinsi Provinsi { get; set; }
      public virtual ICollection<Kegiatan> Kegiatan { get; set; }
   }
}
