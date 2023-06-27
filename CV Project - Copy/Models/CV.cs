using System.ComponentModel.DataAnnotations;

namespace CV_Project.Models
{
    public class CV
    {
        public int CVID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public int Grade { get; set; }
        public string Image { get; set; }
        [Display(Name = "Skills")]
        public ICollection<HasSkills> HasSkills { get; set; }

    }  
}
