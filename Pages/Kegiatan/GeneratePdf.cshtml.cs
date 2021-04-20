using System.IO;
using Itm.Misc;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

namespace DeputiTigaKemenpora.Pages.Kegiatan
{
   public class GeneratePdfModel : CustomPageModel
   {
      public GeneratePdfModel()
      {
         Title = "Generate PDF";
      }

      public IActionResult OnGet(string sourceUrl)
      {
         HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

         WebKitConverterSettings settings = new WebKitConverterSettings
         {
            WebKitPath = GeneratePdfHelper.WebKitPath,
            MediaType = MediaType.Print,
            Margin = new PdfMargins
            {
               Top = GeneratePdfHelper.MarginTopPoints,
               Bottom = GeneratePdfHelper.MarginBottomPoints,
               Left = GeneratePdfHelper.MarginLeftPoints,
               Right = GeneratePdfHelper.MarginRightPoints
            }
         };

         htmlConverter.ConverterSettings = settings;

         PdfDocument document = htmlConverter.Convert(sourceUrl);
         MemoryStream stream = new MemoryStream();

         document.Save(stream);
         stream.Position = 0;
         document.Close(true);

         return File(
            stream,
            "application/pdf",
            GeneratePdfHelper.DefaultOutputFileName);
      }
   }
}
