using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CV_Project.Data;
using CV_Project.Models;

namespace CV_Project.Pages.CVs
{
    public class EditModel : PageModel
    {
        private CVContext _context;
        private readonly IWebHostEnvironment _environment;
        public Array GenderItems = Enum.GetValues(typeof(Gender));
        public List<Skill> SkillItems { get; set; }
        public IEnumerable<SelectListItem> NationalityItems { get; set; } = new List<SelectListItem>
        {
                new SelectListItem("American","american"),
                new SelectListItem("Europian","europrian"),
                new SelectListItem("Middle Eastern","me"),
                new SelectListItem("Asian","asian")
        };

        public EditModel(CVContext context, IWebHostEnvironment environment)
        {
            _context = context;
            SkillItems = _context.Skills.ToList();
            _environment = environment;
        }

        [BindProperty]
        public CVVM CVVM { get; set; } = default!;
        public CV CV { get; set; }
        public string imagePath { get; set; }
        public List<Skill> checkedSkills { get; set; }=new List<Skill>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CV == null)
            {
                return NotFound();
            }

            CV = _context.CV.Include(s => s.HasSkills)
                            .ThenInclude(h => h.Skill)
                            .FirstOrDefault(m => m.CVID == id);

            if (CV == null) return NotFound();

            CVVM = new CVVM()
            {
                FirstName = CV.Firstname,
                LastName = CV.Lastname,
                DateOfBirth = CV.DateOfBirth,
                Nationality = CV.Nationality,
                Email = CV.Email,
                ConfirmEmail = CV.Email,
                Grade = CV.Grade

            };
            if (CV.Gender.Equals(Gender.Female)) CVVM.Gender = Gender.Female;
            else CVVM.Gender = Gender.Male;
            //query the database to find all skills in a cv and put them in a list
            var skills = await _context.HasSkills.Where(s=>s.CVID == id).Include(s=>s.Skill).ToListAsync();
            foreach (var skill in skills)
            {
                checkedSkills.Add(skill.Skill);
            }
            imagePath = CV.Image;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            CV = await _context.CV.Include(s => s.HasSkills)
                                  .ThenInclude(s => s.Skill)
                                  .FirstOrDefaultAsync(m => m.CVID == id);

            CV.Firstname = CVVM.FirstName;
            CV.Lastname= CVVM.LastName;
            CV.DateOfBirth = CVVM.DateOfBirth;
            CV.Email = CVVM.Email;
            CV.Grade = CVVM.Grade;
            CV.Gender = CVVM.Gender.ToString();
            CV.Nationality = CVVM.Nationality;

            CV.HasSkills.Clear();
            foreach(var Skillid in CVVM.Skills)
            {
                var hasskil = new HasSkills { SkillID = Skillid, CVID=CV.CVID };
                CV.HasSkills.Add(hasskil);
            }
            int grade = 10 * CV.HasSkills.Count();
            if (CV.Gender.Equals("Male")) grade += 10;
            else grade += 5;
            CV.Grade = grade;
            var path = _environment.WebRootPath + CV.Image;
            System.IO.File.Delete(path);

            string name = CVVM.FirstName + "_" + CVVM.LastName + "_" + SendCVModel.i + ".jpg";
            var newpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", name);
            using (FileStream stream = new FileStream(newpath,FileMode.Create))
            {
                await CVVM.photo.CopyToAsync(stream);
                stream.Close();
                SendCVModel.i++;
            }
            CV.Image = "\\Images\\" + name;

            _context.Attach(CV).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToPage("./BrowseCV");
        }
    }
}
