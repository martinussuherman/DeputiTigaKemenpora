namespace DeputiTigaKemenpora.ViewModels
{
   public class FileUploadData
   {
      public string FilePath { get; set; } = string.Empty;
      public string InputName { get; set; }

      public bool FilePathExists => !string.IsNullOrEmpty(FilePath);
   }
}
