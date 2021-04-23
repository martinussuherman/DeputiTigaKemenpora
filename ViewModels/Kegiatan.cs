using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;

namespace DeputiTigaKemenpora.ViewModels
{
   public class Kegiatan : IModels<Models.Kegiatan>
   {
      public Kegiatan()
      {
         for (int index = 1; index <= _fotoKegiatanUpload.Length; index++)
         {
            _fotoKegiatanUpload[index - 1] = new FileUploadData
            {
               InputName = $"Kegiatan.FotoKegiatan{index}"
            };
         }

         for (int index = 1; index <= _filePendukungUpload.Length; index++)
         {
            _filePendukungUpload[index - 1] = new FileUploadData
            {
               InputName = $"Kegiatan.FilePendukung{index}"
            };
         }
      }

      public uint Id
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

      [Required(ErrorMessage = "Mohon pilih Penanggung Jawab.")]
      public ushort? IdPenanggungJawab
      {
         get => Models.PenanggungJawabId;
         set => Models.PenanggungJawabId = value;
      }

      public string NamaPenanggungJawab
      {
         get => Models?.PenanggungJawab?.Nama ?? string.Empty;
      }

      [Required(ErrorMessage = "Mohon pilih Kabupaten/Kota.")]
      public ushort? IdKabupatenKota
      {
         get => Models.KabupatenKotaId;
         set => Models.KabupatenKotaId = value;
      }

      public string NamaKabupatenKota
      {
         get => Models?.KabupatenKota?.Nama ?? string.Empty;
      }

      [Required(ErrorMessage = "Mohon pilih Sumber Dana.")]
      public byte? IdSumberDana
      {
         get => Models.SumberDanaId;
         set => Models.SumberDanaId = value;
      }

      public string NamaSumberDana
      {
         get => Models?.SumberDana?.Nama ?? string.Empty;
      }

      [Required(ErrorMessage = "Tempat harus diisi.")]
      public string Tempat
      {
         get => Models.Tempat;
         set => Models.Tempat = value;
      }

      [Required(ErrorMessage = "Tanggal Mulai harus diisi.")]
      public DateTime TanggalMulai
      {
         get => Models.TanggalMulai;
         set => Models.TanggalMulai = value;
      }

      public string TanggalMulaiDisplay
      {
         get => Models.TanggalMulai.ToString("yyyy-MM-dd");
      }

      public string TanggalMulaiView
      {
         get => Models.TanggalMulai.ToString("D", info);
      }

      [Required(ErrorMessage = "Tanggal Selesai harus diisi.")]
      public DateTime TanggalSelesai
      {
         get => Models.TanggalSelesai;
         set => Models.TanggalSelesai = value;
      }

      public string TanggalSelesaiDisplay
      {
         get => Models.TanggalSelesai.ToString("yyyy-MM-dd");
      }

      public string TanggalSelesaiView
      {
         get => Models.TanggalSelesai.ToString("D", info);
      }

      [Required(ErrorMessage = "Pejabat Pembuka harus diisi.")]
      public string NamaPejabatPembuka
      {
         get => Models.NamaPejabatPembuka;
         set => Models.NamaPejabatPembuka = value;
      }

      [Required(ErrorMessage = "Jumlah Peserta harus diisi.")]
      public uint JumlahPeserta
      {
         get => Models.JumlahPeserta;
         set => Models.JumlahPeserta = value;
      }

      public string JumlahPesertaView
      {
         get => JumlahPeserta.ToString("##,#", info);
      }

      public bool AdaKendala
      {
         get => Models.AdaKendala == 1;
         set
         {
            if (!value)
            {
               Kendala = string.Empty;
            }

            Models.AdaKendala = (sbyte)(value ? 1 : 0);
         }
      }

      public string Kendala
      {
         get => Models.Kendala;
         set => Models.Kendala = value ?? string.Empty;
      }

      public string KendalaView
      {
         get => AdaKendala ? Kendala : "-";
      }

      public string LinkBerita1
      {
         get => Models.LinkBerita1;
         set => Models.LinkBerita1 = value ?? string.Empty;
      }

      public string LinkBerita2
      {
         get => Models.LinkBerita2;
         set => Models.LinkBerita2 = value ?? string.Empty;
      }

      public string LinkBerita3
      {
         get => Models.LinkBerita3;
         set => Models.LinkBerita3 = value ?? string.Empty;
      }

      public string FotoKegiatan1
      {
         get => Models.FotoKegiatan1;
         set => Models.FotoKegiatan1 = value ?? string.Empty;
      }

      public string FotoKegiatan2
      {
         get => Models.FotoKegiatan2;
         set => Models.FotoKegiatan2 = value ?? string.Empty;
      }

      public string FotoKegiatan3
      {
         get => Models.FotoKegiatan3;
         set => Models.FotoKegiatan3 = value ?? string.Empty;
      }

      public string FotoKegiatan4
      {
         get => Models.FotoKegiatan4;
         set => Models.FotoKegiatan4 = value ?? string.Empty;
      }

      public string FotoKegiatan5
      {
         get => Models.FotoKegiatan5;
         set => Models.FotoKegiatan5 = value ?? string.Empty;
      }

      public string FilePendukung1
      {
         get => Models.FilePendukung1;
         set => Models.FilePendukung1 = value ?? string.Empty;
      }

      public string FilePendukung1View
      {
         get => Path.GetFileName(FilePendukung1);
      }

      public string FilePendukung2
      {
         get => Models.FilePendukung2;
         set => Models.FilePendukung2 = value ?? string.Empty;
      }

      public string FilePendukung2View
      {
         get => Path.GetFileName(FilePendukung2);
      }

      public string FilePendukung3
      {
         get => Models.FilePendukung3;
         set => Models.FilePendukung3 = value ?? string.Empty;
      }

      public string FilePendukung3View
      {
         get => Path.GetFileName(FilePendukung3);
      }

      public FileUploadData FotoKegiatan1Upload
      {
         get
         {
            _fotoKegiatanUpload[0].FilePath = FotoKegiatan1;
            return _fotoKegiatanUpload[0];
         }
      }

      public FileUploadData FotoKegiatan2Upload
      {
         get
         {
            _fotoKegiatanUpload[1].FilePath = FotoKegiatan2;
            return _fotoKegiatanUpload[1];
         }
      }

      public FileUploadData FotoKegiatan3Upload
      {
         get
         {
            _fotoKegiatanUpload[2].FilePath = FotoKegiatan3;
            return _fotoKegiatanUpload[2];
         }
      }

      public FileUploadData FotoKegiatan4Upload
      {
         get
         {
            _fotoKegiatanUpload[3].FilePath = FotoKegiatan4;
            return _fotoKegiatanUpload[3];
         }
      }

      public FileUploadData FotoKegiatan5Upload
      {
         get
         {
            _fotoKegiatanUpload[4].FilePath = FotoKegiatan5;
            return _fotoKegiatanUpload[4];
         }
      }

      public FileUploadData FilePendukung1Upload
      {
         get
         {
            _filePendukungUpload[0].FilePath = FilePendukung1;
            return _filePendukungUpload[0];
         }
      }

      public FileUploadData FilePendukung2Upload
      {
         get
         {
            _filePendukungUpload[1].FilePath = FilePendukung2;
            return _filePendukungUpload[1];
         }
      }

      public FileUploadData FilePendukung3Upload
      {
         get
         {
            _filePendukungUpload[2].FilePath = FilePendukung3;
            return _filePendukungUpload[2];
         }
      }

      public Models.Kegiatan Models { get; set; } = new Models.Kegiatan();

      private readonly FileUploadData[] _fotoKegiatanUpload = new FileUploadData[5];

      private readonly FileUploadData[] _filePendukungUpload = new FileUploadData[3];

      private static readonly CultureInfo info = CultureInfo.CreateSpecificCulture("id-ID");
   }
}
