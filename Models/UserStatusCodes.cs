using System.ComponentModel.DataAnnotations;

namespace MemberDataEntryForm.Models
{
    public class UserStatusCodes
    {
        [Key]
        public int StatusCode { get; set; }
        [Required]
        public string StatusMessage { get; set; }
    }
}
