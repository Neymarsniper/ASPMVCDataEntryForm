using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MemberDataEntryForm.Models
{
    public class MembersFamilyData
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(2)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string Relation { get; set; }
        [Required]
        [MaxLength(200)]
        [MinLength(5)]
        public string HomeAddress { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(10)]
        public string Mobile { get; set; }
        [MaxLength(20)]
        [MinLength(3)]
        public string ChildName { get; set; }
        [Required]
        public int MemNo { get; set; }
        [ForeignKey("MemNo")]
        public virtual MemberData MemberData { get; set; }
    }
}
