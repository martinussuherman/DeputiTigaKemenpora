using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace DeputiTigaKemenpora
{
    public static class UploadFolderCreator
    {
        public static void CreateUploadFolders(this IWebHostEnvironment environment)
        {
            string[] namaBagian = { "", "foto-kegiatan", "file-pendukung" };

            foreach (string nama in namaBagian)
            {
                string path = Path.Combine(
                    environment.WebRootPath,
                    "upload",
                    nama);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }
    }
}