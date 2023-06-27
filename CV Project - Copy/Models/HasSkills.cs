namespace CV_Project.Models
{
    public class HasSkills
    {
        public int Id { get; set; }
        public int CVID { get; set; }
        public int SkillID { get; set; }
        public Skill Skill { get; set; }
        public CV CV { get; set; }

    }
}
