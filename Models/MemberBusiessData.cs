using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberDataEntryForm.Models
{
    public class MemberBusiessData
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int MemNo { get; set; }
        [Required]
        public string BusinessName { get; set; }
        [Required]
        public string BusinessDetail { get; set; }
        [Required]
        public string BusinessAddress { get; set; }
        [Required]
        public string BusinessCity { get; set; }
        [Required]
        public string BusinessPostalCode { get; set; }
        [Required]
        public string BusinessEmail { get; set; }
        //public string Occupation { get; set; }
        //public string CompanyName { get; set; }
        //public string CompanyAddress { get; set; }

    }
}
