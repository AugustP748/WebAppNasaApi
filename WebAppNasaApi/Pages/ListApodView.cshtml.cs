using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppNasaApi.Context;
using WebAppNasaApi.Models;

namespace WebAppNasaApi.Pages
{
    public class ListApodViewModel : PageModel
    {
        private readonly WebAppNasaApi.Context.ApplicationDbContext _context;

        public ListApodViewModel(WebAppNasaApi.Context.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Apod> Apod { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ApodDB != null)
            {
                Apod = await _context.ApodDB.ToListAsync();
            }
        }
    }
}
