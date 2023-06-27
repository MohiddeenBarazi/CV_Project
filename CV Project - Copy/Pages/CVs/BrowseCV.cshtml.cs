using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CV_Project.Data;
using CV_Project.Models;

namespace CV_Project.Pages.CVs
{
    public class BrowseCVModel : PageModel
    {
        private readonly CV_Project.Data.CVContext _context;

        public BrowseCVModel(CV_Project.Data.CVContext context)
        {
            _context = context;
        }

        public IList<CV> CV { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.CV != null)
            {
                CV = await _context.CV.ToListAsync();
            }
        }
    }
}
