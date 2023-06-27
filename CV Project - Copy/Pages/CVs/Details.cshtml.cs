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
    public class DetailsModel : PageModel
    {
        private readonly CV_Project.Data.CVContext _context;

        public DetailsModel(CV_Project.Data.CVContext context)
        {
            _context = context;
        }

      public CV CV { get; set; } = default!;  

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CV == null)
            {
                return NotFound();
            }

            var cv = await _context.CV.Include(s=>s.HasSkills).ThenInclude(s=>s.Skill).AsNoTracking().FirstOrDefaultAsync(m => m.CVID == id);
            if (cv == null)
            {
                return NotFound();
            }
            else 
            {
                CV = cv;
            }
            return Page();
        }
    }
}
