using System;
using Microsoft.Extensions.Configuration;
using Syncfusion.Pdf.Graphics;

namespace DeputiTigaKemenpora
{
    public static class GeneratePdfHelper
    {
        public static string WebKitPath { get; set; }

        public static string DefaultOutputFileName { get; set; }

        public static int MarginTopMm { get; set; }

        public static int MarginBottomMm { get; set; }

        public static int MarginLeftMm { get; set; }

        public static int MarginRightMm { get; set; }

        public static float MarginTopPoints => ConvertMmToPoint(MarginTopMm);

        public static float MarginBottomPoints => ConvertMmToPoint(MarginBottomMm);

        public static float MarginLeftPoints => ConvertMmToPoint(MarginLeftMm);

        public static float MarginRightPoints => ConvertMmToPoint(MarginRightMm);

        public static float ConvertMmToPoint(int mm)
        {
            return converter.ConvertUnits(
                mm,
                PdfGraphicsUnit.Millimeter,
                PdfGraphicsUnit.Point);
        }

        public static int ReadMarginFromConfig(
            IConfigurationSection section,
            string subSectionName)
        {
            string value = section
                .GetSection(subSectionName)
                .Value;

            return String.IsNullOrEmpty(value) ? 0 : Convert.ToInt32(value);
        }

        private static readonly PdfUnitConverter converter = new PdfUnitConverter();
    }
}