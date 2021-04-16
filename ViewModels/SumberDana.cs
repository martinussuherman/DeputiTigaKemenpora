using System.ComponentModel.DataAnnotations;

namespace DeputiTigaKemenpora.ViewModels
{
   public class SumberDana : IModels<Models.SumberDana>
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

      public Models.SumberDana Models { get; set; } = new Models.SumberDana();
   }
}
