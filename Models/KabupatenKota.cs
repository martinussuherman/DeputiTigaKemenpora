using System.Collections.Generic;

namespace DeputiTigaKemenpora.Models
{
    public partial class KabupatenKota
    {
        public KabupatenKota()
        {
            Kegiatan = new HashSet<Kegiatan>();
        }

        public int Kode { get; set; }
        public string Nama { get; set; }
        public sbyte KodeProvinsi { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }

        public virtual Provinsi Provinsi { get; set; }
        public virtual ICollection<Kegiatan> Kegiatan { get; set; }
    }
}
