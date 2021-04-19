using System.ComponentModel.DataAnnotations;

namespace DeputiTigaKemenpora.ViewModels
{
   public class Provinsi : IModels<Models.Provinsi>
   {
      public byte Id
      {
         get => Models.Id;
         set => Models.Id = value;
      }

      [Required(ErrorMessage = "Nama harus diisi.")]
      public string Nama
      {
         get => Models.Nama;
         set => Models.Nama = value;
      }

      [Required(ErrorMessage = "Lintang harus diisi.")]
      public decimal Lat
      {
         get => Models.Lat;
         set => Models.Lat = value;
      }

      [Required(ErrorMessage = "Bujur harus diisi.")]
      public decimal Long
      {
         get => Models.Long;
         set => Models.Long = value;
      }

      public Models.Provinsi Models { get; set; } = new Models.Provinsi();
   }
}
