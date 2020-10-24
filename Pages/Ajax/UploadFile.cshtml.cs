using System;
using System.IO;
using System.Threading.Tasks;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeputiTigaKemenpora.Pages.Ajax
{
    [Authorize]
    public class UploadFileModel : CustomPageModel
    {
        public UploadFileModel(
            ILogger<UploadFileModel> logger,
            IWebHostEnvironment environment)
        {
            _logger = logger;
            hostingEnvironment = environment;
            Title = "Upload File";
        }

        [BindProperty]
        public string NamaBagian { get; set; }

        [BindProperty]
        public IFormFile UploadFile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (await CopyUploadedFile())
            {
                string path = $"/upload/{NamaBagian}/{UploadFile.FileName}";
                return new JsonResult(path);
            }

            return NotFound();
        }

        private async Task<bool> CopyUploadedFile()
        {
            if (UploadFile == null ||
                String.IsNullOrEmpty(UploadFile.FileName) ||
                UploadFile.Length == 0)
            {
                return false;
            }

            string filePath = Path.Combine(
                hostingEnvironment.WebRootPath,
                "upload",
                NamaBagian,
                UploadFile.FileName);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await UploadFile.CopyToAsync(stream);
            }

            return true;
        }

        private readonly ILogger<UploadFileModel> _logger;
        private readonly IWebHostEnvironment hostingEnvironment;
    }
}