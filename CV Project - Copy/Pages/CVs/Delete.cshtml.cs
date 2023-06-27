﻿using System;
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
    public class DeleteModel : PageModel
    {
        private readonly CVContext _context;
        private readonly IWebHostEnvironment _environment;

        public DeleteModel(CVContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
      public CV CV { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CV == null)
            {
                return NotFound();
            }

            var cv = await _context.CV.Include(s => s.HasSkills).ThenInclude(s => s.Skill).AsNoTracking().FirstOrDefaultAsync(m => m.CVID == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.CV == null)
            {
                return NotFound();
            }
            var cv = await _context.CV.FindAsync(id);

            if (cv != null)
            {
                CV = cv;
                _context.CV.Remove(CV);
                var path = _environment.WebRootPath + CV.Image;
                System.IO.File.Delete(path);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./BrowseCV");
        }
    }
}
