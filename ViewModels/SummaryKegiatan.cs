using System.Globalization;

namespace DeputiTigaKemenpora.ViewModels
{
   public class SummaryKegiatan
   {
      public string Nama { get; set; }

      public byte? IdSumberDana { get; set; }

      public string NamaSumberDana
      {
         get
         {
            return _namaSumberDana;
         }
         set
         {
            if (value == null)
            {
               value = string.Empty;
            }

            _namaSumberDana = value;
         }
      }

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

      private string _namaSumberDana;
      private static readonly CultureInfo info = CultureInfo.CreateSpecificCulture("id-ID");
   }
}
