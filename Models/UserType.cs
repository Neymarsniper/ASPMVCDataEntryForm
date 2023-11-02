using System.ComponentModel.DataAnnotations;

namespace MemberDataEntryForm.Models
{
    public class UserType
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }

        //public string Admin { get; set; }
        //public string FrontDesk { get; set; }
        //public string OfficeClerk { get; set; }
        //public string Receptionist { get; set; }
    }
}
