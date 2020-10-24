using System.Linq;
using DeputiTigaKemenpora.Data;
using DeputiTigaKemenpora.Identity;
using DeputiTigaKemenpora.ViewModels;
using Itm.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using P.Pager;

namespace DeputiTigaKemenpora.Pages.Kegiatan
{
    public class IndexModel : CustomPageModel
    {
        public IndexModel(
            IAuthorizationService authorizationService,
            ILogger<IndexModel> logger,
            ApplicationDbContext context)
        {
            _authorizationService = authorizationService;
            _logger = logger;
            _context = context;
            Title = "Daftar Kegiatan";
            PageTitle = "Kegiatan";
        }

        public IPager<ViewModels.Kegiatan> Kegiatan { get; set; }

        public bool IsUserCanDelete { get; set; }

        public void OnGet([FromQuery] int page = 1)
        {
            Kegiatan = _context.Kegiatan
                .Include(e => e.PenanggungJawabNavigation)
                .OrderBy(e => e.PenanggungJawab)
                .ThenBy(e => e.Nama)
                .ThenBy(e => e.Tempat)
                .AsNoTracking()
                .ToPagerList(page, PagerUrlHelper.ItemPerPage)
                .ViewModelPagerCopy<ViewModels.Kegiatan, Models.Kegiatan>(page);

            AuthorizationResult result = _authorizationService.AuthorizeAsync(
                User,
                Permissions.Kegiatan.Delete).Result;
            IsUserCanDelete = result.Succeeded;
        }

        private readonly IAuthorizationService _authorizationService;
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
    }
}