using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberDataEntryForm.Models
{
    public class UserData
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter the First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter the Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Mandatory!!!")]
        [RegularExpression("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please confirm your Email")]
        [Compare("Email", ErrorMessage = "Email and Confirm Email must be same!!")]
        public string EmailConfirmed { get; set; }
        
        [Required(ErrorMessage = "Password is Mandatory!!!")]
        [RegularExpression("(?=^.{8,}$)((?=.*\\d)|(?=.*\\W+))(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Range must be 8 and 15!, must include : Uppercase, Lowercase, Number, Special Symbols...")] // this is Strong Password code
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm password must be same!!")]
        public string PasswordConfirmed { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        public string City { get; set; }

        [Required(ErrorMessage = "Enter the Valid Mobile Number!!!")]
        public string MobileNo { get; set; }

        [Required]
        public string userType { get; set; }

        //[Required]
        //[ForeignKey("RoleId")]
        //public int useTypeId { get; set; }

        //[Required]
        //[ForeignKey("RoleId")]
        //public UserType useType { get; set; }
    }
}
