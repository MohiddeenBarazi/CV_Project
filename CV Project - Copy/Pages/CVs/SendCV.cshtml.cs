using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CV_Project.Data;
using CV_Project.Models;

namespace CV_Project.Pages.CVs
{
    public class SendCVModel : PageModel
    {
        [BindProperty]
        public CVVM CVVM { get; set; } = default!;
        public CV newCV = new CV();
        public static int i = 1;
        public Array GenderItems = Enum.GetValues(typeof(Gender));
        public List<Skill> Skills { get; set; }
        private readonly CVContext _context;
        public IEnumerable<SelectListItem> Items { get; set; } = new List<SelectListItem>
        {
                new SelectListItem("American","American"),
                new SelectListItem("Europian","Europian"),
                new SelectListItem("Middle Eastern","Middle Eastern"),
                new SelectListItem("Asian","Asian")
        };
        public SendCVModel(CVContext context)
        {
            _context = context;
            Skills = _context.Skills.ToList();
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid) return Page();
          if((CVVM.x + CVVM.y)!= CVVM.sum) return Page();
            newCV.Firstname = CVVM.FirstName;
            newCV.Lastname = CVVM.LastName;
            newCV.DateOfBirth = CVVM.DateOfBirth;
            newCV.Nationality = CVVM.Nationality;
            newCV.Gender = CVVM.Gender.ToString();
            newCV.Email = CVVM.Email;
            newCV.HasSkills = new List<HasSkills>();
            foreach (var id in CVVM.Skills)
            {
                var HS = new HasSkills { SkillID = id, CVID=newCV.CVID };
                newCV.HasSkills.Add(HS);
            }
            int grade = 10 * newCV.HasSkills.Count();
            if (CVVM.Gender.ToString() == "Female") grade += 10;
            else grade += 5;
            newCV.Grade = grade;
            if (CVVM.photo == null || CVVM.photo.Length == 0)
            {
                return Content("File not selected");
            }
            string name = CVVM.FirstName + "_" + CVVM.LastName + "_" + i + ".jpg";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", name);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await CVVM.photo.CopyToAsync(stream);
                stream.Close();
                i++;
            }
            newCV.Image = "\\Images\\" + name;

            _context.Add(newCV);
            _context.SaveChanges();

            return RedirectToPage("/CVs/BrowseCV");

        }
    }
}
