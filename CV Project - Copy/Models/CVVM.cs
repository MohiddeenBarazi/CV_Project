using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace CV_Project.Models
{
    public enum Gender
    {
        Male, Female
    }
    public class CVVM
    {
        [Required(ErrorMessage ="First Name is Required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Last Name is Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Date Of Birth is Required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Nationality is Required")]
        public string Nationality { get; set; }
        [Required(ErrorMessage ="Required")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage ="Pick atleast 1 Skill")]
        public List<int> Skills { get; set; }
        [EmailAddress]
        [Required(ErrorMessage ="Enter Email Address")]
        public string Email { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Confirm Email Address Required")]
        [Compare("Email", ErrorMessage = "Emails don't match")]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }
        [Required]
        [Display(Name = "Photo")]
        public IFormFile photo { get; set; }
        public int Grade { get; set; }
        [Required(ErrorMessage ="First Value is Required")]
        [Range(1,20,ErrorMessage ="The value is between 1 and 20")]
        public int x { get; set; }
        [Required(ErrorMessage = "Second Value is Required")]
        [Range(20, 50, ErrorMessage = "The value is between 20 and 50")]
        public int y { get; set; }
        [Required(ErrorMessage ="Third Value is required")]
        public int sum { get; set; }
    }
}
