using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberDataEntryForm.Models
{
    public class MemberImageModel
    {
        [Required]
        public IFormFile Photo { get; set; }
    }
}
