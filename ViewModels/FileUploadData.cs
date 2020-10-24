using System;

namespace DeputiTigaKemenpora.ViewModels
{
    public class FileUploadData
    {
        public string FilePath { get; set; } = String.Empty;
        public string InputName { get; set; }

        public bool FilePathExists
        {
            get => !String.IsNullOrEmpty(FilePath);
        }
    }
}