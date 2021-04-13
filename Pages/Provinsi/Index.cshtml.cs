using DeputiTigaKemenpora.Data;
using DeputiTigaKemenpora.ViewModels;
using Itm.Misc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P.Pager;

namespace DeputiTigaKemenpora.Pages.Provinsi
{
    public class IndexModel : CustomPageModel
    {
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
            Title = "Daftar Provinsi";
            PageTitle = "Provinsi";
        }

        public IPager<ViewModels.Provinsi> Provinsi { get; set; }

        public void OnGet([FromQuery] int page = 1)
        {
            Provinsi = _context.Provinsi
                .AsNoTracking()
                .ToPagerList(page, PagerUrlHelper.ItemPerPage)
                .ViewModelPagerCopy<ViewModels.Provinsi, Models.Provinsi>(page);
        }

        private readonly ApplicationDbContext _context;
    }
}