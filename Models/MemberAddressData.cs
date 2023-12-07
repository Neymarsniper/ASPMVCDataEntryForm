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
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [MaxLength(30)]
        public string Country { get; set; }

        [Required]
        [MaxLength(50)]
        public string State { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(6)]
        public string PostalCode { get; set; }

        [MaxLength(300)]
        public string AdditonalInfo { get; set; }

        [Required]
        public string AddressType { get; set; }

        public int? AuthId { get; set; }

        [Required]
        public int MemNo { get; set; }

        [ForeignKey("MemNo")]
        public MemberData MemberData { get; set; }
    }
}
