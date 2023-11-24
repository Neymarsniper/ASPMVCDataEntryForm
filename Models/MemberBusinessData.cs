using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberDataEntryForm.Models
{
    public class MemberBusinessData
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string BusinessName { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string BusinessDetail { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string BusinessAddress { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string BusinessCity { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(3)]
        public string BusinessPostalCode { get; set; }
        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        public string BusinessEmail { get; set; }
        [Required]
        public int MemNo { get; set; }
        [ForeignKey("MemNo")]
        public virtual MemberData MemberData { get; set; }
    }
}
