using System;
using System.Collections.Generic;

namespace DeputiTigaKemenpora.Models
{
    public partial class Kegiatan
    {
        public uint Id { get; set; }
        public ushort? PenanggungJawab { get; set; }
        public string Nama { get; set; }
        public string Tempat { get; set; }
        public DateTime TanggalMulai { get; set; }
        public DateTime TanggalSelesai { get; set; }
        public string NamaPejabatPembuka { get; set; }
        public uint JumlahPeserta { get; set; }
        public sbyte AdaKendala { get; set; }
        public string Kendala { get; set; }
        public string LinkBerita1 { get; set; }
        public string LinkBerita2 { get; set; }
        public string LinkBerita3 { get; set; }
        public string FotoKegiatan1 { get; set; }
        public string FotoKegiatan2 { get; set; }
        public string FotoKegiatan3 { get; set; }
        public string FotoKegiatan4 { get; set; }
        public string FotoKegiatan5 { get; set; }
        public string FilePendukung1 { get; set; }
        public string FilePendukung2 { get; set; }
        public string FilePendukung3 { get; set; }

        public virtual PenanggungJawab PenanggungJawabNavigation { get; set; }
    }
}
