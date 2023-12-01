using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberDataEntryForm.Models
{
    public class UserType
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }

        //public ICollection<UserData> userData { get; set; }
    }
}
