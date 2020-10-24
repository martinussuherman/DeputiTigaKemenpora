using System.ComponentModel.DataAnnotations;

namespace DeputiTigaKemenpora.ViewModels
{
    public class PenanggungJawab : IModels<Models.PenanggungJawab>
    {
        public ushort Id
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

        public Models.PenanggungJawab Models { get; set; } = new Models.PenanggungJawab();
    }
}