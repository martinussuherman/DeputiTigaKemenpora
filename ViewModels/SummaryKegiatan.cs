using System.Globalization;

namespace DeputiTigaKemenpora.ViewModels
{
    public class SummaryKegiatan
    {
        public string NamaPenanggungJawab { get; set; }

        public ushort IdPenanggungJawab { get; set; }

        public int JumlahKegiatan { get; set; }

        public string JumlahKegiatanView
        {
            get => JumlahKegiatan.ToString("##,#", info);
        }

        public long TotalPeserta { get; set; }

        public string TotalPesertaView
        {
            get => TotalPeserta.ToString("##,#", info);
        }

        private static readonly CultureInfo info = CultureInfo.CreateSpecificCulture("id-ID");
    }
}