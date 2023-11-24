using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberDataEntryForm.Models
{
    public class MemberImageModel
    {
        //these properties below are for storing MemberData values :
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is must!!!")]
        [MaxLength(20)]
        [MinLength(3)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "D.O.B is Necessary!!!")]
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "Please provide the Addess!!!")]
        [MaxLength(100)]
        [MinLength(3)]
        public string ResAddress { get; set; } = null!;

        [Required(ErrorMessage = "Enter the Valid Phone number!!!")]
        [MaxLength(10)]
        [MinLength(10)]
        public string ResPhone { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string OfficeNo { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string Profession { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string OfficeAddress { get; set; } = null!;

        [Required(ErrorMessage = "Enter the Valid Cell Number!!!")]
        [MaxLength(10)]
        [MinLength(10)]
        public string MobileNo { get; set; } = null!;

        [MaxLength(10)]
        [MinLength(10)]
        public string? AlternateMobileNo { get; set; }

        [Required(ErrorMessage = "Email is Mandatory!!!")]
        [RegularExpression("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$")]
        public string Email { get; set; } = null!;

        [Required]
        public DateTime DateofMarriage { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string NameofSpouse { get; set; } = null!;

        [Required]
        public DateTime SpouseDob { get; set; }

        [MaxLength(20)]
        [MinLength(3)]
        public string? ChildName { get; set; }

        [Required]
        public IFormFile Photo { get; set; }


        //this below is the MemberFamilyData properties :
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
        [MaxLength(100)]
        [MinLength(5)]
        public string HomeAddress { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(10)]
        public string Mobile { get; set; }
        [MaxLength(20)]
        [MinLength(3)]
        public string? childName { get; set; }


        //these properties below are for storing MemberBusinessData values : 
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


        //these below properties are for storing MemberAddressData values : 
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
    }
}
