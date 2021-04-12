using System.Collections.Generic;

namespace DeputiTigaKemenpora.Models
{
    public partial class PenanggungJawab
    {
        public PenanggungJawab()
        {
            Kegiatan = new HashSet<Kegiatan>();
        }

        public ushort Id { get; set; }
        public string Nama { get; set; }

        public virtual ICollection<Kegiatan> Kegiatan { get; set; }
    }
}
