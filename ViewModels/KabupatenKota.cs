using System.ComponentModel.DataAnnotations;

namespace DeputiTigaKemenpora.ViewModels
{
    public class KabupatenKota : IModels<Models.KabupatenKota>
    {
        public int Kode
        {
            get => Models.Kode;
            set => Models.Kode = value;
        }

        [Required(ErrorMessage = "Nama harus diisi.")]
        public string Nama
        {
            get => Models.Nama;
            set => Models.Nama = value;
        }

        [Required(ErrorMessage = "Mohon pilih Provinsi.")]
        public sbyte Provinsi
        {
            get => Models.KodeProvinsi;
            set => Models.KodeProvinsi = value;
        }

        public string NamaProvinsi
        {
            get => Models?.Provinsi?.Nama ?? string.Empty;
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

        public Models.KabupatenKota Models { get; set; } = new Models.KabupatenKota();
    }
}