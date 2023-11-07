using System.ComponentModel.DataAnnotations;

namespace MemberDataEntryForm.Models
{
    public class MemberFamilyData
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public int MemNo { get; set; }
        [Required]
        public string SpouseName { get; set; }
        [Required]
        public string SpouseOccupation { get; set; }
        public string SpouseMobileNo { get; set; }
        public string ChildName { get; set; }
        [Required]
        public string FatherName { get; set; }
        [Required]
        public string FatherOccupation { get; set; }
        public string FatherMobileNo { get; set; }
        [Required]
        public string MotherName { get; set; }
        [Required]
        public string MotherOccupation { get; set; }
        public string MotherMobileNo { get; set; }
    }
}
