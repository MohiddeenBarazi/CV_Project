using CV_Project.Models;

namespace CV_Project.Data
{
    public class DbInitializer
    {
        public static void Intialize(CVContext context)
        {
            if (context.Skills.Any()) return;
            var skills = new Skill[] {
                new Skill{SkillName="C#"},
                new Skill{SkillName="C"},
                new Skill{SkillName="Java"},
                new Skill{SkillName="Python"},
                new Skill{SkillName="Assembly"},
                new Skill{SkillName="Flutter"}
        };
        context.Skills.AddRange(skills);
        context.SaveChanges();
        }
}
}
