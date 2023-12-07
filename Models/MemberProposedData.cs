using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberDataEntryForm.Models
{
    public class MemberProposedData
    {

        [Key]
        public int MemProId { get; set; }
        //MemberData Model properties : 
        public string? Name { get; set; }

        public DateTime Dob { get; set; }

        public string? ResAddress { get; set; }

        public string? ResPhone { get; set; } 

        public string? OfficeNo { get; set; } 

        public string? Profession { get; set; }

        public string? OfficeAddress { get; set; } 

        public string? MobileNo { get; set; }

        public string? AlternateMobileNo { get; set; }

        public string? Email { get; set; }

        public DateTime DateofMarriage { get; set; }

        public string? NameofSpouse { get; set; }

        public DateTime SpouseDob { get; set; }

        public string? ChildName { get; set; }





        //MemberAddress Model properties : 
        public string? Address { get; set; }

        public string? Country { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public string? PostalCode { get; set; }

        public string? AdditonalInfo { get; set; }

        public string? AddressType { get; set; }





        //MemberFamily Model properties : 
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Relation { get; set; }

        public string? HomeAddress { get; set; }

        public string? Mobile { get; set; }

        public string? FamilyChildName { get; set; }





        //MemberBusiness Model properties : 
        public string? BusinessName { get; set; }

        public string? BusinessDetail { get; set; }

        public string? BusinessAddress { get; set; }

        public string? BusinessCity { get; set; }

        public string? BusinessPostalCode { get; set; }

        public string? BusinessEmail { get; set; }

        //this below property is storing the Current Admin or FrontDesk Id which is accessing the Data...
        public int? AuthId { get; set; }

        //this below properties is the Foreign key refrenced to MemberData Models...
        public int? MemId { get; set; }
        [ForeignKey("MemId")]
        public virtual MemberData MemberData { get; set; }

        public int? MemBusinessId { get; set; }
        [ForeignKey("MemBusinessId")]
        public virtual MemberBusinessData MemberBusinessData { get; set; }

        public int? MemFamilyId { get; set; }
        [ForeignKey("MemFamilyId")]
        public virtual MembersFamilyData MembersFamilyData { get; set; }

        public int? MemAddressId { get; set; }
        [ForeignKey("MemAddressId")]
        public virtual MemberAddressData MemberAddressData { get; set; }
    }
}
