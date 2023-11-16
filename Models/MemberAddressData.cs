using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberDataEntryForm.Models
{
    public class MemberAddressData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        [MaxLength(150)]
        public string City { get; set; }

        [Required]
        public int PostalCode { get; set; }

        public String AdditonalInfo { get; set; }

        [Required]
        public string AddressType { get; set; }

        [Required]
        public int MemNo { get; set; }

        [ForeignKey("MemNo")]
        public MemberData MemberData { get; set; }
    }
}
