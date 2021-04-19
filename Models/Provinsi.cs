using System.Collections.Generic;

namespace DeputiTigaKemenpora.Models
{
   public partial class Provinsi
   {
      public Provinsi()
      {
         KabupatenKota = new HashSet<KabupatenKota>();
      }

      public byte Id { get; set; }
      public string Nama { get; set; }
      public decimal Lat { get; set; }
      public decimal Long { get; set; }

      public virtual ICollection<KabupatenKota> KabupatenKota { get; set; }
   }
}
