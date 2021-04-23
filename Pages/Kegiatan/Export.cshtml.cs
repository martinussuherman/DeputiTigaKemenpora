using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Itm.Misc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.XlsIO;

namespace DeputiTigaKemenpora.Pages.Kegiatan
{
   public class ExportModel : CustomPageModel
   {
      public ExportModel(ApplicationDbContext context)
      {
         _context = context;
         Title = "Ekspor ke Excel";
      }

      public List<Models.Kegiatan> Kegiatan { get; set; }

      public async Task<IActionResult> OnGetAsync()
      {
         List<Models.Kegiatan> tempList = await _context.Kegiatan
            .Include(e => e.PenanggungJawab)
            .OrderBy(e => e.PenanggungJawab)
            .ThenBy(e => e.Nama)
            .ThenBy(e => e.Tempat)
            .AsNoTracking()
            .ToListAsync();

         List<ViewModels.Kegiatan> result = tempList
            .ConvertAll(x => new ViewModels.Kegiatan { Models = x });

         using ExcelEngine excelEngine = new ExcelEngine();
         IApplication application = excelEngine.Excel;

         application.DefaultVersion = ExcelVersion.Xlsx;

         IWorkbook workbook = application.Workbooks.Create(1);
         IWorksheet worksheet = workbook.Worksheets[0];

         worksheet.IsGridLinesVisible = false;

         FillWorksheetHeader(worksheet);

         for (int index = 0; index < result.Count; index++)
         {
            FillWorksheetDetail(worksheet, index + 1, result[index]);
         }

         worksheet.UsedRange.AutofitColumns();
         worksheet.UsedRange.AutofitRows();

         MemoryStream stream = new MemoryStream();

         workbook.SaveAs(stream);
         stream.Position = 0;

         return new FileStreamResult(stream, "application/excel")
         {
            FileDownloadName = "Output.xlsx"
         };
      }

      private void FillWorksheetHeader(IWorksheet worksheet)
      {
         worksheet.Range["A1"].Text = "Daftar Kegiatan";
         worksheet.Range["A1"].CellStyle.Font.Bold = true;
         worksheet.Range["A1"].CellStyle.Font.Size = 16;
         worksheet.Range["A1:T1"].Merge();
         worksheet.Range["A1:T1"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

         worksheet.Range["A2"].Text = "No";
         worksheet.Range["B2"].Text = "Penanggung Jawab";
         worksheet.Range["C2"].Text = "Nama Kegiatan";
         worksheet.Range["D2"].Text = "Tempat";
         worksheet.Range["E2"].Text = "Tanggal Mulai";
         worksheet.Range["F2"].Text = "Tanggal Selesai";
         worksheet.Range["G2"].Text = "Nama Pejabat Pembuka";
         worksheet.Range["H2"].Text = "Jumlah Peserta";
         worksheet.Range["I2"].Text = "Kendala";
         worksheet.Range["J2"].Text = "Link Berita 1";
         worksheet.Range["K2"].Text = "Link Berita 2";
         worksheet.Range["L2"].Text = "Link Berita 3";
         worksheet.Range["M2"].Text = "Foto Kegiatan 1";
         worksheet.Range["N2"].Text = "Foto Kegiatan 2";
         worksheet.Range["O2"].Text = "Foto Kegiatan 3";
         worksheet.Range["P2"].Text = "Foto Kegiatan 4";
         worksheet.Range["Q2"].Text = "Foto Kegiatan 5";
         worksheet.Range["R2"].Text = "File Pendukung 1";
         worksheet.Range["S2"].Text = "File Pendukung 2";
         worksheet.Range["T2"].Text = "File Pendukung 3";

         worksheet.Range["A2:T2"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
         worksheet.Range["A2:T2"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
         worksheet.Range["A2:T2"].CellStyle.Font.Bold = true;
      }

      private void FillWorksheetDetail(
         IWorksheet worksheet,
         int index,
         ViewModels.Kegiatan item)
      {
         worksheet.Range[index + 2, 1].Number = index;
         worksheet.Range[index + 2, 2].Text = item.NamaPenanggungJawab;
         worksheet.Range[index + 2, 3].Text = item.Nama;
         worksheet.Range[index + 2, 4].Text = item.Tempat;
         worksheet.Range[index + 2, 5].Value = item.TanggalMulaiView;
         worksheet.Range[index + 2, 6].Value = item.TanggalSelesaiView;
         worksheet.Range[index + 2, 7].Text = item.NamaPejabatPembuka;
         worksheet.Range[index + 2, 8].Number = item.JumlahPeserta;
         worksheet.Range[index + 2, 9].Text = item.KendalaView;
         CreateHyperlink(worksheet.Range[index + 2, 10], item.LinkBerita1);
         CreateHyperlink(worksheet.Range[index + 2, 11], item.LinkBerita2);
         CreateHyperlink(worksheet.Range[index + 2, 12], item.LinkBerita3);
         CreateHyperlink(
            worksheet.Range[index + 2, 13],
            LocalToAbsoluteUri(item.FotoKegiatan1));
         CreateHyperlink(
            worksheet.Range[index + 2, 14],
            LocalToAbsoluteUri(item.FotoKegiatan2));
         CreateHyperlink(
            worksheet.Range[index + 2, 15],
            LocalToAbsoluteUri(item.FotoKegiatan3));
         CreateHyperlink(
            worksheet.Range[index + 2, 16],
            LocalToAbsoluteUri(item.FotoKegiatan4));
         CreateHyperlink(
            worksheet.Range[index + 2, 17],
            LocalToAbsoluteUri(item.FotoKegiatan5));
         CreateHyperlink(
            worksheet.Range[index + 2, 18],
            LocalToAbsoluteUri(item.FilePendukung1));
         CreateHyperlink(
            worksheet.Range[index + 2, 19],
            LocalToAbsoluteUri(item.FilePendukung2));
         CreateHyperlink(
            worksheet.Range[index + 2, 20],
            LocalToAbsoluteUri(item.FilePendukung3));
      }

      private void CreateHyperlink(IRange range, string url)
      {
         if (string.IsNullOrEmpty(url))
         {
            return;
         }

         IWorksheet worksheet = range.Worksheet;
         IHyperLink hyperlink = worksheet.HyperLinks.Add(range);
         hyperlink.Type = ExcelHyperLinkType.Url;
         hyperlink.Address = url;
      }

      private string LocalToAbsoluteUri(string localUri)
      {
         if (string.IsNullOrEmpty(localUri))
         {
            return string.Empty;
         }

         PathString localPath = new PathString(localUri);
         HttpRequest request = HttpContext.Request;

         return string.Concat(
            request.Scheme,
            "://",
            request.Host.ToUriComponent(),
            request.PathBase.ToUriComponent(),
            localPath.ToUriComponent());
      }

      // worksheet.Range["E6"].Value = "12/31/2018";
      // worksheet.Range["D7:E7"].CellStyle.Color = Color.FromArgb(42, 118, 189);
      // worksheet.Range["A3:E23"].CellStyle.Font.FontName = "Arial";
      // worksheet.Range["D16:E22"].NumberFormat = "$.00";
      // application.EnableIncrementalFormula = true;
      // worksheet.Range["E16:E20"].Formula = "=C16*D16";
      // worksheet.Range["E23"].Formula = "=SUM(E16:E22)";
      // worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
      // worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

      private readonly ApplicationDbContext _context;
   }
}
