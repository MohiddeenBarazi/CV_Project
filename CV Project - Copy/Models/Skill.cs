namespace CV_Project.Models
{
    public class Skill
    {
        public int SkillID { get; set; }
        public string SkillName { get; set; }
        public ICollection<HasSkills> HasSkills { get; set; }
    }
}
