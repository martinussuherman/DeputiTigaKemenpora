using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task OnGetAsync([FromQuery] int page = 1)
        {
            List<Models.Provinsi> list = await _context.Provinsi
                .AsNoTracking()
                .ToListAsync();
            Provinsi = list
                .ViewModelCopy<ViewModels.Provinsi, Models.Provinsi>(page);
        }

        private readonly ApplicationDbContext _context;
    }
}